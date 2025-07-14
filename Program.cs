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

        // STEP 1: Add new Blog
        Console.Write("Enter blog URL: ");
        var url = Console.ReadLine();
        var blog = new Blog { Url = url };
        var blogType = context.BlogTypes.FirstOrDefault();
        if (blogType == null)
        {
            blogType = new BlogType
            {
                Status = 1,
                Name = "General",
                Description = "Default blog type"
            };
            context.BlogTypes.Add(blogType);
            context.SaveChanges();
        }

        // Assign BlogType to blog
        blog.BlogTypeId = blogType.BlogTypeId;
        context.Blogs.Add(blog);
        context.SaveChanges();

        // STEP 2: Add 3 users
        var users = new List<User>
        {
            new User { UserName = "Tarun", Email = "Tarun@google.com", PhoneNumber = "7809152930" },
            new User { UserName = "Kiran", Email = "Kiran@google.com", PhoneNumber = "2345678901" },
            new User { UserName = "Mahir", Email = "Mahir@google.com", PhoneNumber = "3456789012" }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        // STEP 3: Ensure at least one PostType exists
        var postType = context.PostTypes.FirstOrDefault();
        if (postType == null)
        {
            postType = new PostType
            {
                Status = 1,
                Name = "General",
                Description = "Default post type"
            };
            context.PostTypes.Add(postType);
            context.SaveChanges();
        }

        // STEP 4: Add a Post by the first user
        var post = new Post
        {
            Title = "Hello EF Core",
            Content = "This is my first post!",
            BlogId = blog.BlogId,
            PostTypeId = postType.PostTypeId,
            UserId = users[0].UserId 
        };
        context.Posts.Add(post);
        context.SaveChanges();

        // STEP 5: Display all blogs and posts with user info
        var blogs = context.Blogs
            .Include(b => b.Posts)
            .ThenInclude(p => p.User)
            .ToList();

        foreach (var b in blogs)
        {
            Console.WriteLine($"\nBlog: {b.Url}");
            foreach (var p in b.Posts)
            {
                Console.WriteLine($"  Post: {p.Title} - {p.Content} (Author: {p.User?.UserName})");
            }
        }
    }
}