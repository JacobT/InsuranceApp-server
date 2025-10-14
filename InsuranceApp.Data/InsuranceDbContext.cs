using InsuranceApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data;

/// <summary>
/// The Entity Framework Core database context for the Insurance application.  
/// Inherits from <see cref="IdentityDbContext"/> to integrate ASP.NET Core Identity 
/// for authentication and authorization.
/// </summary>
public class InsuranceDbContext : IdentityDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the EF Core model for the application, including relationships 
    /// between entities and cascading delete behaviors.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Insurance>()
            .HasOne(insurance => insurance.Customer)
            .WithMany(customer => customer.Insurances)
            .HasForeignKey(insurance => insurance.InsuredId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<InsuranceClaim>()
            .HasOne(c => c.Insurance)
            .WithMany(i => i.Claims)
            .HasForeignKey(c => c.InsuranceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RefreshTokenEntity>()
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
