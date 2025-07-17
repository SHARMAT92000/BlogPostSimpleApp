using BlogPostSimpleApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostApplication.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        // One Status can be assigned to many Blogs
        public List<Blog> Blogs { get; set; }
    }
}
