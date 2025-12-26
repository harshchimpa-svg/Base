using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Persistence.Extension;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using WebApi.CustomMiddleware.NLog;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // --- Add Controllers ---
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // --- Add SignalR ---
        builder.Services.AddSignalR();

        // --- NLog Logging ---
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        // --- Set DB Connection String in Environment ---
        Environment.SetEnvironmentVariable(
            "DB_CONNECTION_STRING",
            builder.Configuration.GetConnectionString("DefaultConnection")
        );

        // --- Add Services Layers ---
        builder.Services.AddPersistenceLayer(builder.Configuration, builder.Environment);
        builder.Services.ApplicationLayer(); 
        builder.Services.AddInfrastructure();

        builder.Services.AddHttpContextAccessor();

        // --- Swagger ---
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Web API",
                Version = "v1"
            });

            c.CustomOperationIds(apiDesc =>
                apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null
            );

            // JWT Auth
            var security = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer {your token}'",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", security);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { security, Array.Empty<string>() }
            });
        });

        // --- Rate Limiter ---
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx =>
                RateLimitPartition.GetFixedWindowLimiter(
                    ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 60,
                        Window = TimeSpan.FromMinutes(1)
                    }
                ));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsync("Too many requests! Try again after 1 minute");
            };
        });

        // --- CORS ---
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", policy =>
            {
                policy.AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(_ => true)
                      .AllowCredentials();
            });
        });

        // --- File Upload Limit ---
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 30 * 1024 * 1024;
        });

        var app = builder.Build();

        // --- Middlewares ---
        app.UseHttpsRedirection();
        app.UseHsts();

        app.UseStaticFiles();

        app.UseCors("AllowOrigin");
        app.UseRateLimiter();

        app.UseMiddleware<NLogMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        // --- Swagger UI ---
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./swagger/v1/swagger.json", "Web Api v1");
                options.RoutePrefix = "";
            });
        }

        app.MapControllers();

        app.Run();
    }
}
