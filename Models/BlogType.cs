using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostApplication.Models
{
  public class BlogType
    {
        public int BlogTypeId { get; set; }    // Primary Key
        public int Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
