using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Identityy.Data.Dtos;

namespace Identityy.Models;

public class UsuarioProfile : Profile
{
	public UsuarioProfile()
    {
        CreateMap<CadastrarUsuarioDto, Usuario>();
        CreateMap<Usuario, IdentityUser<int>>();
    }
}
