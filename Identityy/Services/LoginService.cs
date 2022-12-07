using FluentResults;
using Microsoft.AspNetCore.Identity;
using Identityy.Data.Dtos;

namespace Identityy.Models;

public class LoginService
{
    private SignInManager<IdentityUser<int>> _signInManager;
    private TokenService _tokenService;

	public LoginService(
        SignInManager<IdentityUser<int>> signInManager,
        TokenService tokenService)
	{
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public Result LogarUsuario(LogarUsuarioDto logarUsuarioDto)
    {
        var resultadoIdentity = _signInManager
            .PasswordSignInAsync(logarUsuarioDto.Username, logarUsuarioDto.Password, false, false);

        if (resultadoIdentity.Result.Succeeded)
        {
            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedUserName == logarUsuarioDto.Username.ToUpper());
            var token = _tokenService.CreateToken(identityUser);

            return Result.Ok().WithSuccess(token.Value);
        }

        return Result.Fail("Falha ao logar usuario");
    }

    public Result DeslogarUsuario()
    {
        var resultadoIdentity = _signInManager.SignOutAsync();

        if (resultadoIdentity.IsCompletedSuccessfully)
        {
            return Result.Ok();
        }

        return Result.Fail("Falha ao deslogar usuario");
    }
}
