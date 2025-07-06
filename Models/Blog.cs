using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostApplication.Models
{
    public class Blog
    {
        public int BlogId { get; set; }           // Primary Key
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
    }

}
