using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostSimpleApp.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Navigation: One User can have many Posts
        public List<Post> Posts { get; set; }
    }
}