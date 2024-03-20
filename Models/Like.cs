using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Like
    {
        [Key]
        public int Id {get; set;}

        public int UserId { get; set; }

    }
}