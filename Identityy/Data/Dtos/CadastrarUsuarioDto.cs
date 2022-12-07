using Identityy.Swagger;
using System.ComponentModel.DataAnnotations;

namespace Identityy.Data.Dtos;

public class CadastrarUsuarioDto
{
    [Required]
    [SwaggerSchemaExample("username")]
    public string Username { get; set; }

    [Required]
    [SwaggerSchemaExample("username@email.com")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [SwaggerSchemaExample("Password!123")]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    [SwaggerSchemaExample("Password!123")]
    public string RePassword { get; set; }
}
