using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("BlogType")]  // Set table name to BlogType
public class BlogType
{
    [Key]  // Primary key
    public int BlogTypeId { get; set; }  // BlogTypeId as primary key

    [Required]  // Make Status required
    public int Status { get; set; }  // Assuming Status is an int

    [Required]  // Make Name required
    [MaxLength(20)]  // Maximum length of 20 characters
    public string Name { get; set; }

    [MaxLength(400)]  // Maximum length of 400 characters for Description
    public string Description { get; set; }
}