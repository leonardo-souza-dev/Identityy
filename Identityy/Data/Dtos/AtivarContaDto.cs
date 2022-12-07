using Identityy.Swagger;
using System.ComponentModel.DataAnnotations;

namespace Identityy.Data.Dtos;

public class AtivarContaDto
{
    [Required]
    public string CodigoAtivacao { get; set; }

    [Required]
    public int UsuarioId { get; set; }
}
