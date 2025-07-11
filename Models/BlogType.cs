using BlogPostSimpleApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("BlogType")]  // Set table name to BlogType
public class BlogType
{
    [Key]  
    public int BlogTypeId { get; set; } 

    [Required]  
    public int Status { get; set; }  
     
    [Required]  
    [MaxLength(20)]  
    public string Name { get; set; }

    [MaxLength(400)]  
    public string Description { get; set; }
    public List<Blog> Blogs { get; set; }
}