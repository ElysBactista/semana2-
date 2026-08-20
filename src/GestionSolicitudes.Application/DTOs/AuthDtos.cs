using System.ComponentModel.DataAnnotations;

namespace GestionSolicitudes.Application.DTOs;

/// <summary>
/// DTO para la petición de inicio de sesión
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "El correo electrónico es requerido.")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida.")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// DTO para el registro de nuevos usuarios (Residentes o Administrativos)
/// </summary>
public class RegistroDto
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Password { get; set; } = string.Empty;

    public string Rol { get; set; } = "Residente"; // Rol asignado por defecto
}

/// <summary>
/// Respuesta devuelta por la API tras login o registro
/// </summary>
public class RespuestaAuthDto
{
    public bool Exito { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
}