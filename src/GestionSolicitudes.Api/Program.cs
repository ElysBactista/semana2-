using System.Text;
using GestionSolicitudes.Application.Interfaces;
using GestionSolicitudes.Infrastructure.Persistence;
using GestionSolicitudes.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1. Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Base de Datos (Entity Framework Core con SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Inyección de Dependencias de Servicios de Negocio
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// 4. CORS: Permite peticiones desde el cliente Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 5. Configuración de ASP.NET Core Identity (Reglas de contraseña)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 6. Configuración de Autenticación con Token JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecretaYLargaParaFirmarLosTokensJWT2026!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "GestionSolicitudesApi";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();

// 7. Pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS debe ir ANTES de Authentication y Authorization
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// --- Sembrado automático de Roles y Usuarios de Prueba al arrancar ---
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // 1. Crear Roles si no existen
    string[] roles = ["Administrador", "Residente"];
    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    // 2. Crear Admin si no existe
    var admin = await userManager.FindByEmailAsync("admin@migracion.gob.do");
    if (admin == null)
    {
        admin = new IdentityUser { UserName = "admin@migracion.gob.do", Email = "admin@migracion.gob.do", EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin123*");
        await userManager.AddToRoleAsync(admin, "Administrador");
    }

    // 3. Crear Residente si no existe
    var residente = await userManager.FindByEmailAsync("residente@correo.com");
    if (residente == null)
    {
        residente = new IdentityUser { UserName = "residente@correo.com", Email = "residente@correo.com", EmailConfirmed = true };
        await userManager.CreateAsync(residente, "Residente123*");
        await userManager.AddToRoleAsync(residente, "Residente");
    }
}

app.Run();