using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Post
    {
        [Key]
        public int Id {get; set;}
        public string? Author {get; set;}
        [Required]
        public string Title {get; set;}
        [Required]
        public string Content {get; set;}
        [Required]
        public string Category {get; set;}
        [Required]
        public string FilePath {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        internal bool Contains()
        {
            throw new NotImplementedException();
        }
    }
}