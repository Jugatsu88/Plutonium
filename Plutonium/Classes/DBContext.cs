using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plutonium.Models;

namespace Plutonium.Classes
{
    public class DBContext : DbContext
    {

        public DbSet<Link> Links { get; set; }
        public DbSet<Button> Buttons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PlutoniumDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Link>().ToTable("Links", "dbo");
            modelBuilder.Entity<Button>().ToTable("Buttons", "dbo");
            modelBuilder.Entity<MenuItem>().ToTable("MenuItems", "dbo");
            modelBuilder.Entity<Process>().ToTable("Processes", "dbo");
            modelBuilder.Entity<CRUDLookup>().ToTable("tblLookup", "dbo");

            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(e => e.Id);
                //entity.HasIndex(e => e.Title).IsUnique();
                //entity.Property(e => e.DateTimeAdd).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}