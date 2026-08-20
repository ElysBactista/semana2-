using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionSolicitudes.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GestionSolicitudes.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Define la ruta base como: api/auth
public class AuthController(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : ControllerBase
{
    // Manejador de usuarios de ASP.NET Identity (crear, buscar, validar contraseñas)
    private readonly UserManager<IdentityUser> _userManager = userManager;

    // Manejador de roles de Identity (crear, validar roles)
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    // Permite leer las claves de configuración de appsettings.json (JWT Key, Issuer, Audience)
    private readonly IConfiguration _configuration = configuration;

    // POST: api/auth/login
    // Función: Valida correo y contraseña, extrae el rol y devuelve el token JWT con los datos de sesión.
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        // 1. Busca si el correo existe en la base de datos
        var usuario = await _userManager.FindByEmailAsync(dto.Email);
        if (usuario == null)
        {
            return Unauthorized(new RespuestaAuthDto
            {
                Exito = false,
                Mensaje = "Correo o contraseña incorrectos."
            });
        }

        // 2. Comprueba si el hash de la contraseña coincide
        var passwordValido = await _userManager.CheckPasswordAsync(usuario, dto.Password);
        if (!passwordValido)
        {
            return Unauthorized(new RespuestaAuthDto
            {
                Exito = false,
                Mensaje = "Correo o contraseña incorrectos."
            });
        }

        // 3. Obtiene el rol asignado al usuario
        var roles = await _userManager.GetRolesAsync(usuario);
        var rolPrincipal = roles.FirstOrDefault() ?? "Solicitante";

        // 4. Genera el token JWT
        var token = GenerarTokenJwt(usuario, rolPrincipal);

        // 5. Retorna la respuesta con éxito y datos del usuario
        return Ok(new RespuestaAuthDto
        {
            Exito = true,
            Token = token,
            Rol = rolPrincipal,
            NombreCompleto = usuario.UserName ?? usuario.Email ?? string.Empty,
            Correo = usuario.Email ?? string.Empty,
            Mensaje = "Inicio de sesión exitoso."
        });
    }

    // POST: api/auth/registro
    // Función: Registra una nueva cuenta de usuario y le asigna su rol.
    [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] RegistroDto dto)
    {
        // 1. Valida que el correo no esté registrado previamente
        var usuarioExiste = await _userManager.FindByEmailAsync(dto.Email);
        if (usuarioExiste != null)
        {
            return BadRequest(new RespuestaAuthDto
            {
                Exito = false,
                Mensaje = "El correo ya está registrado."
            });
        }

        // 2. Crea el objeto IdentityUser
        var nuevoUsuario = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };

        // 3. Guarda el usuario en la BD con su contraseña hasheada
        var resultado = await _userManager.CreateAsync(nuevoUsuario, dto.Password);
        if (!resultado.Succeeded)
        {
            var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
            return BadRequest(new RespuestaAuthDto
            {
                Exito = false,
                Mensaje = errores
            });
        }

        // 4. Asigna el rol especificado al usuario creado
        if (!string.IsNullOrWhiteSpace(dto.Rol))
        {
            if (!await _roleManager.RoleExistsAsync(dto.Rol))
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Rol));
            }
            await _userManager.AddToRoleAsync(nuevoUsuario, dto.Rol);
        }

        return Ok(new RespuestaAuthDto
        {
            Exito = true,
            Mensaje = "Usuario creado exitosamente."
        });
    }

    // Función privada: Construye y firma el token JWT con los claims del usuario
    private string GenerarTokenJwt(IdentityUser usuario, string rol)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "ClaveSuperSecretaYLargaParaFirmarLosTokensJWT2026!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Claims: Datos embebidos dentro del token
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id),
            new(ClaimTypes.Email, usuario.Email ?? string.Empty),
            new(ClaimTypes.Name, usuario.UserName ?? string.Empty),
            new(ClaimTypes.Role, rol)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}