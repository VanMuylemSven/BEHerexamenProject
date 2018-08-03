using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataGent.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        public string Discriminator { get; set; }
    }
}
