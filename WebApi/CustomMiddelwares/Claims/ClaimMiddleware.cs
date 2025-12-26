using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;

namespace WebApi.CustomMiddlewares.Claims;

public class ClaimMiddleware
{
    private readonly RequestDelegate _next;

    public ClaimMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext _dbContext)
    {


        await _next(context);
    }
}
