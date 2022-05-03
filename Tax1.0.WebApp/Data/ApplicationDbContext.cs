using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tax1._0.WebApp.Models;

namespace Tax1._0.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Salary> Salary { get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>()
            .HasIndex(p => new { p.Email, p.Month }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
