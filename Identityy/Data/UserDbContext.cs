using Identityy.Models;
using Microsoft.EntityFrameworkCore;
using Identityy.Data.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Identityy.Data;

public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
	public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
	{
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Identityy");
    }
}
