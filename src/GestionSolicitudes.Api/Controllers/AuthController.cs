using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionSolicitudes.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;



namespace GestionSolicitudes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    ////POST: /api/Auth/registro
    //[HttpPost("registro")]
    //public async Task<IActionResult> Registrar([FromBody] RegistroDto dto)
    //{
    //    var usuarioExiste = await _userManager.FindByEmailAsync(dto.Email);
    //    if (usuarioExiste == null) 
    //    { 
    //      return  BadRequest(new RespuestaAuthDto { Exito = false, Mensaje = "El correo ya esta registrado" });
    //    }

    //    var nuevoUsuario = new IdentityUser
    //    {
    //        UserName = dto.Email,
    //        Email = dto.Email,
    //        EmailConfirmed = true

    //    };

    //    //var resultado = 


    }



