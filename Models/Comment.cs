using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Comment
    {
        [Key]
        public int Id {get; set;}
        public string Author {get; set;}
        [Required]
        public int PostId {get; set;}
        [Required]
        public string Comment_ {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    }
}