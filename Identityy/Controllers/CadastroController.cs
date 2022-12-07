using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Identityy.Data.Dtos;
using Identityy.Models;

namespace Identityy.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Consumes("application/json")]
public class CadastroController : Controller
{
    private readonly CadastroService _cadastroService;

    public CadastroController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpPost]
    [Route("cadastrar-usuario")]
    public IActionResult CadastrarUsuario(CadastrarUsuarioDto cadastrarUsuarioDto)
    {
        Result result = _cadastroService.CadastrarUsuario(cadastrarUsuarioDto);

        if (result.IsFailed)
        {
            return StatusCode(500);
        }

        return Ok(result.Successes);
    }

    [HttpGet]
    [Route("ativar-conta")]
    public IActionResult AtivarConta([FromQuery]AtivarContaDto ativarContaDto)
    {
        Result result = _cadastroService.AtivarConta(ativarContaDto);

        if (result.IsFailed)
        {
            return StatusCode(500);
        }

        return Ok(result.Successes);
    }
}
