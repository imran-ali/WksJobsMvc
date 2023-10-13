using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WksJobsMvc.Models;

namespace WksJobsMvc.Data
{
    public class WksDbContext : IdentityDbContext<DefaultUser>
    {
        public WksDbContext(DbContextOptions<WksDbContext> options) : base (options)
        {            
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobItemDetail> JobItemDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>().ToTable("Job");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Vessel>().ToTable("Vessel");
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<JobItemDetail>().ToTable("JobItemDetail");

            modelBuilder.Entity<Vessel>().HasIndex(p => new { p.VesselCode }).IsUnique();
            modelBuilder.Entity<Item>().HasIndex(p=> new { p.ItemCode }).IsUnique();
            modelBuilder.Entity<Job>().HasIndex(p => new { p.JobCode }).IsUnique();

        }

    }
}
