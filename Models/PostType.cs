
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostSimpleApp.Models
{
    [Table("PostType")]
    public class PostType
    {
        [Key]
        public int PostTypeId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }


        public List<Post> Posts { get; set; }
    }
}