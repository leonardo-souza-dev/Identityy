using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identityy.Models;

public class TokenService
{
    private SignInManager<IdentityUser<int>> _signInManager;

	public TokenService(SignInManager<IdentityUser<int>> signInManager)
	{
        _signInManager = signInManager;
	}

    public Token CreateToken(IdentityUser<int> usuario)
    {
        Claim[] direitosUsuario = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id.ToString())
        };

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("f06h54f06h5f40h65f4g0h65fg4h0"));

        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: direitosUsuario,
            signingCredentials: credenciais,
            expires: DateTime.UtcNow.AddHours(1));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token(tokenString);
    }
}
