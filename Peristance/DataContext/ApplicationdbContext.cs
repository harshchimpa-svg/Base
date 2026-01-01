using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Domain.Entities.ApplicationRoles;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Catagoryes;
using Domain.Entities.Documents;
using Domain.Entities.Organizations;
using Domain.Entities.OTPs;
using Domain.Entities.PaymentHeates;
using Domain.Entities.Transicstions;
using Domain.Entities.TranstionDocuments;
using Domain.Entities.Users.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence.DataContext;

public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    private readonly IReadOnlyCollection<int> _currentOrgIds;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentOrganizationProvider currentOrganizationProvider) : base(options)
    {
        _currentOrgIds = currentOrganizationProvider.OrganizationIds.ToList();
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OTP> OTPs { get; set; }

    //public DbSet<Template> Templates { get; set; }
    //public DbSet<TemplateBody> TemplateBodies { get; set; }
    //public DbSet<TemplateType> TemplateTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<PaymentHead> PaymentHeads { get; set; }
    public DbSet<Transicstion> Transicstions { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<TranstionDocument> TranstionDocument { get; set; }
    public DbSet<Catgory> Catgory { get; set; }

     
    public IReadOnlyCollection<int> CurrentOrgIds => _currentOrgIds;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());




        modelBuilder.Entity<Role>()
            .HasIndex(r => r.NormalizedName)
            .IsUnique(false);

        modelBuilder.Entity<UserRole>(builder =>
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        });


        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            var parameter = Expression.Parameter(clrType, "e");
            Expression? filterExpression = null;

            // Skip organization filter for the Organization entity itself
            var isOrganizationEntity = clrType == typeof(Organization); // or your actual Organization class
            var orgIdProp = clrType.GetProperty("OrganizationId");

            // 1️⃣ IsDeleted filter
            var isDeletedProp = clrType.GetProperty("IsDeleted");
            if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
            {
                var isDeletedProperty = Expression.Property(parameter, isDeletedProp);
                var isDeletedFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                filterExpression = isDeletedFalse;
            }

            // 2️⃣ OrganizationId filter (skip Organization table itself)
            if (!isOrganizationEntity && orgIdProp != null && orgIdProp.PropertyType == typeof(int))
            {
                var idsExpression = Expression.Property(Expression.Constant(this), nameof(CurrentOrgIds));

                var organizationIdProperty = Expression.Property(parameter, orgIdProp);

                var containsMethod = typeof(Enumerable)
                    .GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(int));

                var containsExpression = Expression.Call(containsMethod, idsExpression, organizationIdProperty);

                filterExpression = filterExpression == null
                    ? containsExpression
                    : Expression.AndAlso(filterExpression, containsExpression);
            }

            // Apply filter if exists
            if (filterExpression != null)
            {
                var lambda = Expression.Lambda(filterExpression, parameter);
                modelBuilder.Entity(clrType).HasQueryFilter(lambda);
            }
        }

    }
}
