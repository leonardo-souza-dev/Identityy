using Identityy.Swagger;
using System.ComponentModel.DataAnnotations;

namespace Identityy.Data.Dtos;

public class LogarUsuarioDto
{
    [Required]
    [SwaggerSchemaExample("leonardo")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [SwaggerSchemaExample("Senha!123")]
    public string Password { get; set; }
}
