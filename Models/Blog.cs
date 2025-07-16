using BlogPostApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostSimpleApp.Models
{
    public class Blog
    {
        public int BlogId { get; set; } // Primary Key
        public string Url { get; set; }
        public bool isPublic { get; set; }
        public List<Post> Posts { get; set; }
        public int BlogTypeId { get; set; }        
        public BlogType BlogType { get; set; }
        public int StatusId { get; set; }       // Foreign key
        public Status Status { get; set; }      // Navigation property
    }
}