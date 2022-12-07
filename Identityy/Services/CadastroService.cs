using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Identityy.Data.Dtos;
using System.Web;

namespace Identityy.Models;

public class CadastroService
{
    private IMapper _mapper;
    private UserManager<IdentityUser<int>> _userManager;
    private readonly EmailService _emailService;

    public CadastroService(
        IMapper mapper, 
        UserManager<IdentityUser<int>> userManager,
        EmailService emailService)
	{
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public Result CadastrarUsuario(CadastrarUsuarioDto cadastrarUsuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(cadastrarUsuarioDto);
        IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

        var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, cadastrarUsuarioDto.Password);

        if (resultadoIdentity.Result.Succeeded)
        {
            var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

            var encodedCode = HttpUtility.UrlEncode(code.Result);

            _emailService.EnviarEmail(
                new[] { usuarioIdentity.Email },
                "Link de Ativação",
                usuarioIdentity.Id, encodedCode);

            return Result.Ok().WithSuccess(code.Result);
        }

        return Result.Fail("Falha ao cadastrar usuario");
    }

    public Result AtivarConta(AtivarContaDto ativarContaDto)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == ativarContaDto.UsuarioId);
        var identityResult = _userManager.ConfirmEmailAsync(identityUser, ativarContaDto.CodigoAtivacao);

        if (identityResult.Result.Succeeded)
        {
            return Result.Ok();
        }

        return Result.Fail("Falha ao ativar a conta");
    }
}
