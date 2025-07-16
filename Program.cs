using BlogPostApplication.Models;
using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

class Program
{
    static void Main()
    {
        //    using var context = new AppDbContext();

        //    // STEP 1: Add new Blog
        //    Console.Write("Enter blog URL: ");
        //    var url = Console.ReadLine();
        //    var blog = new Blog { Url = url };
        //    var blogType = context.BlogTypes.FirstOrDefault();
        //    if (blogType == null)
        //    {
        //        blogType = new BlogType
        //        {
        //            Status = 1,
        //            Name = "General",
        //            Description = "Default blog type"
        //        };
        //        context.BlogTypes.Add(blogType);
        //        context.SaveChanges();
        //    }

        //    // Assign BlogType to blog
        //    blog.BlogTypeId = blogType.BlogTypeId;
        //    context.Blogs.Add(blog);
        //    context.SaveChanges();

        //    // STEP 2: Add 3 users
        //    var users = new List<User>
        //    {
        //        new User { UserName = "Tarun", Email = "Tarun@google.com", PhoneNumber = "7809152930" },
        //        new User { UserName = "Kiran", Email = "Kiran@google.com", PhoneNumber = "2345678901" },
        //        new User { UserName = "Mahir", Email = "Mahir@google.com", PhoneNumber = "3456789012" }
        //    };

        //    context.Users.AddRange(users);
        //    context.SaveChanges();

        //    // STEP 3: Ensure at least one PostType exists
        //    var postType = context.PostTypes.FirstOrDefault();
        //    if (postType == null)
        //    {
        //        postType = new PostType
        //        {
        //            Status = 1,
        //            Name = "General",
        //            Description = "Default post type"
        //        };
        //        context.PostTypes.Add(postType);
        //        context.SaveChanges();
        //    }

        //    // STEP 4: Add a Post by the first user
        //    var post = new Post
        //    {
        //        Title = "Hello EF Core",
        //        Content = "This is my first post!",
        //        BlogId = blog.BlogId,
        //        PostTypeId = postType.PostTypeId,
        //        UserId = users[0].UserId 
        //    };
        //    context.Posts.Add(post);
        //    context.SaveChanges();

        //    // STEP 5: Display all blogs and posts with user info
        //    var blogs = context.Blogs
        //        .Include(b => b.Posts)
        //        .ThenInclude(p => p.User)
        //        .ToList();

        //    foreach (var b in blogs)
        //    {
        //        Console.WriteLine($"\nBlog: {b.Url}");
        //        foreach (var p in b.Posts)
        //        {
        //            Console.WriteLine($"  Post: {p.Title} - {p.Content} (Author: {p.User?.UserName})");
        //        }
        //    }
        //}
        //{


        using var context = new AppDbContext();


        //// Clear existing data
        //context.Posts.RemoveRange(context.Posts);
        //context.Blogs.RemoveRange(context.Blogs);
        //context.BlogTypes.RemoveRange(context.BlogTypes);
        //context.PostTypes.RemoveRange(context.PostTypes);
        //context.Users.RemoveRange(context.Users);
        //context.SaveChanges();

        //// Reset identity counters
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('BlogType', RESEED, 0)"); // because table name is BlogType
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Blogs', RESEED, 0)");
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Posts', RESEED, 0)");
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PostType', RESEED, 0)"); // because table name is PostType
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");
        var statuses = new List<Status>
{
    new Status { Name = "S1", Description = "S1", StatusCode = 1 },
    new Status { Name = "S2", Description = "S2", StatusCode = 2 },
    new Status { Name = "S3", Description = "S3", StatusCode = 3 },
    new Status { Name = "S4", Description = "S4", StatusCode = 4 },
    new Status { Name = "S5", Description = "S5", StatusCode = 5 },
    new Status { Name = "S6", Description = "S6", StatusCode = 6 },
    new Status { Name = "S7", Description = "S7", StatusCode = 7 },
    new Status { Name = "S8", Description = "S8", StatusCode = 8 },
    new Status { Name = "S9", Description = "S9", StatusCode = 9 },
    new Status { Name = "S10", Description = "S10", StatusCode = 10 }

};
        var blogTypes = new List<BlogType>
{
    new BlogType { Name = "Corporate", Description = "Corporate Blog", Status = 1 },
    new BlogType { Name = "Personal", Description = "Personal Blog", Status = 1 },
    new BlogType { Name = "Private", Description = "Private Blog", Status = 1 }
};
        var blogs = new List<Blog>
{
    new Blog { Url = "https://corporate.com", BlogType = blogTypes[0], Status = statuses[0] }, // S1
    new Blog { Url = "https://personal.com", BlogType = blogTypes[1], Status = statuses[1] },  // S2
    new Blog { Url = "https://private.com", BlogType = blogTypes[2], Status = statuses[2] }    // S3
};
        context.Statuses.AddRange(statuses);
        context.BlogTypes.AddRange(blogTypes);
        context.Blogs.AddRange(blogs);
        context.SaveChanges();

        var status10 = context.Statuses.First(s => s.StatusCode == 10);

        var allBlogs = context.Blogs.ToList();

        foreach (var blog in allBlogs)
        {
            blog.StatusId = status10.StatusId;
        }
        context.SaveChanges();
    }
}