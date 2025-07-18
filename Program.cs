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
        var active = new Status { StatusCode = 1, Name = "Active", Description = "Active" };
        var inactive = new Status { StatusCode = 0, Name = "Inactive", Description = "Inactive" };
        context.Statuses.AddRange(active, inactive);
        context.SaveChanges();

        // STEP 2: Seed Users
        var users = new List<User>
        {
            new User { UserName = "Tarun", Email = "Tarun@gmail.com", PhoneNumber = "7809152930" },
            new User { UserName = "Pragun", Email = "Pragun@gmail.com", PhoneNumber = "6548903478" },
            new User { UserName = "Kiran", Email = "Kiran@gmail.com", PhoneNumber = "1238437195" },
            new User { UserName = "Tanziya", Email = "Tanziya@gmail.com", PhoneNumber = "6419486711" },
            new User { UserName = "Mahir", Email = "Mahir@gmail.com", PhoneNumber = "7009210433" },
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // STEP 3: Seed Blog Types
        var blogTypes = new List<BlogType>
        {
            new BlogType { Name = "Tech", Description = "Tech Blog", Status = active.StatusId },
            new BlogType { Name = "Food", Description = "Food Blog", Status = active.StatusId },
            new BlogType { Name = "Travel", Description = "Travel Blog", Status = inactive.StatusId } // Will be excluded
        };
        context.BlogTypes.AddRange(blogTypes);
        context.SaveChanges();

        // STEP 4: Seed Post Types
        var postTypes = new List<PostType>
        {
            new PostType { Name = "Guide", Description = "Helpful guide", Status = active.StatusId },
            new PostType { Name = "Article", Description = "Informational article", Status = active.StatusId },
            new PostType { Name = "News", Description = "Breaking news", Status = inactive.StatusId } // Will be excluded
        };
        context.PostTypes.AddRange(postTypes);
        context.SaveChanges();

        // STEP 5: Seed Blogs
        var blogs = new List<Blog>
        {
            new Blog { Url = "https://techblog.com", isPublic = true, BlogTypeId = blogTypes[0].BlogTypeId, StatusId = active.StatusId },
            new Blog { Url = "https://foodie.com", isPublic = false, BlogTypeId = blogTypes[1].BlogTypeId, StatusId = active.StatusId },
            new Blog { Url = "https://travelnow.com", isPublic = true, BlogTypeId = blogTypes[2].BlogTypeId, StatusId = active.StatusId }, // Excluded
        };
        context.Blogs.AddRange(blogs);
        context.SaveChanges();

        // STEP 6: Seed Posts
        var posts = new List<Post>
        {
            new Post { Title = "AI Future", Content = "Post 1", BlogId = blogs[0].BlogId, PostTypeId = postTypes[0].PostTypeId, UserId = users[4].UserId },
            new Post { Title = "C# Tricks", Content = "Post 2", BlogId = blogs[0].BlogId, PostTypeId = postTypes[1].PostTypeId, UserId = users[2].UserId },
            new Post { Title = "Best Recipes", Content = "Post 3", BlogId = blogs[1].BlogId, PostTypeId = postTypes[0].PostTypeId, UserId = users[2].UserId },
            new Post { Title = "Food Photography", Content = "Post 4", BlogId = blogs[1].BlogId, PostTypeId = postTypes[1].PostTypeId, UserId = users[2].UserId },
            new Post { Title = "Xamarin Dev", Content = "Post 5", BlogId = blogs[0].BlogId, PostTypeId = postTypes[1].PostTypeId, UserId = users[1].UserId },
            new Post { Title = "Pizza Review", Content = "Post 6", BlogId = blogs[1].BlogId, PostTypeId = postTypes[0].PostTypeId, UserId = users[4].UserId },
            new Post { Title = "Hidden Paris", Content = "Post 7", BlogId = blogs[2].BlogId, PostTypeId = postTypes[2].PostTypeId, UserId = users[3].UserId }, // Excluded
        };
        context.Posts.AddRange(posts);
        context.SaveChanges();

        // STEP 7: Query & Output
        var result = context.Blogs
            .Include(b => b.BlogType)
            .Include(b => b.Posts)
                .ThenInclude(p => p.User)
            .Include(b => b.Posts)
                .ThenInclude(p => p.PostType)
            .Where(b => b.Status.StatusCode == 1 && b.BlogType.Status == 1)
            .SelectMany(b => b.Posts
                .Where(p => p.PostType.Status == 1)
                .Select(p => new
                {
                    BlogUrl = b.Url,
                    b.isPublic,
                    BlogType = b.BlogType.Name,
                    UserName = p.User.UserName,
                    UserEmail = p.User.Email,
                    TotalUserPosts = context.Posts.Count(x => x.UserId == p.UserId),
                    PostType = p.PostType.Name,
                    PostTitle = p.Title
                }))
            .OrderBy(p => p.UserName)
            .ToList();

        // STEP 8: Display Output
        foreach (var r in result)
        {
            Console.WriteLine($"Blog URL: {r.BlogUrl}");
            Console.WriteLine($"Is Public: {r.isPublic}");
            Console.WriteLine($"Blog Type: {r.BlogType}");
            Console.WriteLine($"Post Title: {r.PostTitle}");
            Console.WriteLine($"User Name: {r.UserName}");
            Console.WriteLine($"Email: {r.UserEmail}");
            Console.WriteLine($"User Posts Count: {r.TotalUserPosts}");
            Console.WriteLine($"Post Type: {r.PostType}");
            Console.WriteLine(new string('-', 50));
        }
    }
}

