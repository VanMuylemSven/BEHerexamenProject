using DataGent.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataGent.Web.ViewModels
{
    public class CommentaarCreate_VM
    {
        public Stad Stad { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "You need to enter a comment of valid length")]
        [MinLength(5, ErrorMessage = "You need to enter a comment of valid length")]
        public Commentaar Commentaar { get; set; }
    }
}
