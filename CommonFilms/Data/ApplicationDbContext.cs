﻿using CommonFilms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonFilms.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<User>()
            .HasMany(x => x.Movies)
            .WithOne()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>()
            .HasMany(x => x.Friends)
            .WithMany()
            .UsingEntity(y => y.ToTable("UserFriends"));
    }

    public static void SeedData(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "test@test.com",
                    Password = "test",
                    IsAdmin = true,
                }
            );
            context.SaveChanges();
        }
    }
}