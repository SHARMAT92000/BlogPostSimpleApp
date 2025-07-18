using BlogPostApplication.Models;
using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        // Clear existing data
        context.Posts.RemoveRange(context.Posts);
        context.Blogs.RemoveRange(context.Blogs);
        context.BlogTypes.RemoveRange(context.BlogTypes);
        context.PostTypes.RemoveRange(context.PostTypes);
        context.Users.RemoveRange(context.Users);
        context.Statuses.RemoveRange(context.Statuses);
        context.SaveChanges();

        // Reset identity counters
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('BlogType', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Blogs', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PostType', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Status', RESEED, 0)");

        // STEP 1: Seed Statuses
        var statuses = new List<Status>
            {
                new Status { Name = "S1", Description = "Status 1", StatusCode = 1 },
                new Status { Name = "S2", Description = "Status 2", StatusCode = 2 },
                new Status { Name = "S3", Description = "Status 3", StatusCode = 3 },
                new Status { Name = "S4", Description = "Status 4", StatusCode = 4 },
                new Status { Name = "S5", Description = "Status 5", StatusCode = 5 },
                new Status { Name = "S6", Description = "Status 6", StatusCode = 6 },
                new Status { Name = "S7", Description = "Status 7", StatusCode = 7 },
                new Status { Name = "S8", Description = "Status 8", StatusCode = 8 },
                new Status { Name = "S9", Description = "Status 9", StatusCode = 9 },
                new Status { Name = "S10", Description = "Status 10", StatusCode = 10 }
            };
        context.Statuses.AddRange(statuses);
        context.SaveChanges();

        // STEP 2: Seed BlogTypes
        var blogTypes = new List<BlogType>
            {
                new BlogType { Name = "Corporate", Description = "Corporate blog" },
                new BlogType { Name = "Personal", Description = "Personal blog" },
                new BlogType { Name = "Private", Description = "Private blog" }
            };
        context.BlogTypes.AddRange(blogTypes);
        context.SaveChanges();

        // STEP 3: Seed Blogs
        var blogs = new List<Blog>
            {
                new Blog { Url = "https://corporate.com", BlogType = blogTypes[0], Status = statuses[3], isPublic = true },
                new Blog { Url = "https://personal.com", BlogType = blogTypes[1], Status = statuses[4], isPublic = true },
                new Blog { Url = "https://private.com", BlogType = blogTypes[2], Status = statuses[5], isPublic = false }
            };
        context.Blogs.AddRange(blogs);
        context.SaveChanges();

        // STEP 4: Seed Users
        var users = new List<User>
            {
                new User { UserName = "tarun", Email = "tarun@example.com", PhoneNumber = "1234567890" },
                new User { UserName = "kamal", Email = "kamal@example.com", PhoneNumber = "0987654321" }
            };
        context.Users.AddRange(users);
        context.SaveChanges();

        // STEP 5: Seed PostTypes
        var postTypes = new List<PostType>
            {
                new PostType { Name = "General", Description = "General Post", Status = 1 },
                new PostType { Name = "News", Description = "News Post", Status = 1 },
                new PostType { Name = "Update", Description = "Update Post", Status = 1 }
            };
        context.PostTypes.AddRange(postTypes);
        context.SaveChanges();

        // STEP 6: Seed Posts
        var posts = new List<Post>
            {
                new Post { Title = "Post 1", Content = "Content 1", Blog = blogs[0], PostType = postTypes[0], User = users[0] },
                new Post { Title = "Post 2", Content = "Content 2", Blog = blogs[1], PostType = postTypes[1], User = users[1] },
                new Post { Title = "Post 3", Content = "Content 3", Blog = blogs[2], PostType = postTypes[2], User = users[0] }
            };
        context.Posts.AddRange(posts);
        context.SaveChanges();

        // Optional: Output confirmation
        Console.WriteLine("Seeding completed successfully.");

        // STEP 5: Query 1 - Blog + BlogType Name
        Console.WriteLine("\nQuery 1: Blogs with BlogType Name");
        var blogWithType = context.Blogs
            .Include(b => b.BlogType)
            .Select(b => new
            {
                b.BlogId,
                b.Url,
                BlogTypeName = b.BlogType.Name
            })
            .ToList();

        foreach (var item in blogWithType)
        {
            Console.WriteLine($"BlogId: {item.BlogId}, Url: {item.Url}, Type: {item.BlogTypeName}");
        }

        // STEP 6: Query 2 - Blog + BlogType + Number of Posts
        Console.WriteLine("\nQuery 2: Blogs + BlogType + Number of Posts");
        var blogWithCount = context.Blogs
            .Include(b => b.BlogType)
            .Include(b => b.Posts)
            .Select(b => new
            {
                b.BlogId,
                b.Url,
                BlogTypeName = b.BlogType.Name,
                PostCount = b.Posts.Count
            })
            .ToList();

        foreach (var item in blogWithCount)
        {
            Console.WriteLine($"BlogId: {item.BlogId}, Url: {item.Url}, Type: {item.BlogTypeName}, Posts: {item.PostCount}");
        }

        // STEP 7: Query 3 - Post + Blog Name
        Console.WriteLine("\nQuery 3: Posts with Blog Url");
        var postWithBlog = context.Posts
            .Include(p => p.Blog)
            .Select(p => new
            {
                p.PostId,
                p.Title,
                p.Content,
                BlogUrl = p.Blog.Url
            })
            .ToList();

        foreach (var item in postWithBlog)
        {
            Console.WriteLine($"PostId: {item.PostId}, Title: {item.Title}, Content: {item.Content}, Blog: {item.BlogUrl}");
        }
    }
}