
using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<BlogType> BlogTypes { get; set; }
    public DbSet<PostType> PostTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlogPostDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(p => p.PostType)
            .WithMany(pt => pt.Posts)
            .HasForeignKey(p => p.PostTypeId);
        });
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasOne(b => b.BlogType)
                  .WithMany(bt => bt.Blogs)
                  .HasForeignKey(b => b.BlogTypeId);
        });
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(p => p.Blog)
                  .WithMany(b => b.Posts)
                  .HasForeignKey(p => p.BlogId);
        });
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(p => p.User)
                  .WithMany(u => u.Posts)
                  .HasForeignKey(p => p.UserId);
        });
    }

}