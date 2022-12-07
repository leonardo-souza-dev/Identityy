using Identityy.Data;
using Identityy.Models;
using Identityy.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Identityy;

public static class Startup
{
    private static string _assemblyName = typeof(Program).Assembly.GetName().Name ?? "Identityy";

    public static WebApplication Init(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();
        Configure(app);

        return app;
    }

    public static void ConfigureServices(IServiceCollection services)
    { 
        // Add services to the container.
        services.AddControllers();

        services.AddSwaggerGen(s =>
        {
            s.EnableAnnotations();
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "title",
                Description = "# Swagger",
                Contact = new OpenApiContact { Name = "Identityy", Url = new Uri("https://identityy.com") },
            });

            s.SchemaFilter<SwaggerSchemaExampleFilter>();
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase(_assemblyName));
        services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt => opt.SignIn.RequireConfirmedEmail = true)
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<CadastroService, CadastroService>();
        services.AddScoped<LoginService, LoginService>();
        services.AddScoped<TokenService, TokenService>();
        services.AddScoped<EmailService, EmailService>();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
        });
    }

    public static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        //app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSwagger(s =>
        {
            s.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}",
                        Description = _assemblyName
                    }
                };
            });

        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", _assemblyName);
        });

        app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
    }
}
