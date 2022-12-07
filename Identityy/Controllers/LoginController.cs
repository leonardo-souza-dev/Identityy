using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Identityy.Data.Dtos;
using Identityy.Models;

namespace Identityy.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Consumes("application/json")]
public class LoginController : Controller
{
    private LoginService _loginService;

    public LoginController(LoginService cadastroService)
    {
        _loginService = cadastroService;
    }

    [HttpPost]
    [Route("logar-usuario")]
    public IActionResult LogarUsuario(LogarUsuarioDto logarUsuarioDto)
    {
        Result result = _loginService.LogarUsuario(logarUsuarioDto);

        if (result.IsFailed)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result.Successes);
    }

    [HttpPost]
    [Route("deslogar-usuario")]
    public IActionResult DeslogarUsuario()
    {
        Result result = _loginService.DeslogarUsuario();

        if (result.IsFailed)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result.Successes);
    }
}