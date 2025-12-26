
using Application.Interfaces.GenericRepositories;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence.DataContext;
using Persistence.Extension.Repositories;
using Persistence.Extension.Repositories.UserIdAndOrganizationIds;
using System.Security.Claims;
using System.Text;

namespace Persistence.Extension;

public static class IServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.AddDbContext(configuration, webHostEnvironment);
        services.AddRepository();
        services.AddIdentityServices(configuration);

    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
    {
        try
        {
            string connectionString = environment.IsDevelopment()
                 ? configuration.GetConnectionString("DefaultConnection")
                 : configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public static void AddRepository(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
        .AddTransient<ICurrentOrganizationProvider, CurrentOrganizationProvider>()
       .AddTransient<IUserIdAndOrganizationIdRepository, UserIdAndOrganizationIdRepository>()


        ;

    }

    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {



        // Configure application cookies
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use HTTPS
            options.Cookie.SameSite = SameSiteMode.Lax; // Adjust based on your security needs
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                RoleClaimType = ClaimTypes.Role

            };
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        })
       .AddGoogle(options =>
       {
           options.ClientId = configuration["Auth:Google:ClientID"];
           options.ClientSecret = configuration["Auth:Google:ClientSecret"];
           options.Scope.Add("email");
           options.Scope.Add("profile");
           options.SaveTokens = true;
       })
       .AddLinkedIn(options =>
       {
           options.ClientId = configuration["Auth:LinkedIn:ClientId"];
           options.ClientSecret = configuration["Auth:LinkedIn:ClientSecret"];
           options.CallbackPath = new PathString("/signin-LinkedIn"); // Make sure this matches LinkedIn's registered redirect URI
           options.Scope.Add("r_liteprofile");
           options.Scope.Add("r_emailaddress");
           options.SaveTokens = true;
       });
    }
}
