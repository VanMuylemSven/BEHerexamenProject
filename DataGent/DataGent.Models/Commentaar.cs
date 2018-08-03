using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataGent.Models
{

    public class Commentaar
    {
        [Key]
        public int CommentaarId { get; set; }
        [Required]
        public string UserId { get; set; } 
        [Required]
        public int StadId { get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage="You need to enter a comment of valid length")]
        [MinLength(5, ErrorMessage ="You need to enter a comment of valid length")]
        public string CommentaarText { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Tijdstip { get; set; }
    }
}
