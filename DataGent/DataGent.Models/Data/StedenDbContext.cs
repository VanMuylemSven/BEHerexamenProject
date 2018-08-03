using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/*Je kan kiezen waar je deze tabellen onderbrengt : ofwel voeg je deze Identitytabellen toe aan de applicatie database of je kan een 
 * afzonderlijke (herbruikbare) database configureren. We kiezen om alle tabellen in dezelfde database onder te brengen en 
 * passen de Context class aan zodat ze erft van IdentityDbContext in plaats van te erven van DbContext. 
 * 
 * using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
 * using Microsoft.EntityFrameworkCore; 
 * public class ConventionsContext : IdentityDbContext<IdentityUser>*/

namespace DataGent.Models.Data
{
    public class StedenDbContext : IdentityDbContext<IdentityUser>
    {
        public StedenDbContext(DbContextOptions<StedenDbContext> options): base(options)
        {

        }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        //public DbSet<GebruikerCommentaar> GebruikerCommentaar { get; set; }
        public DbSet<Commentaar> Commentaar { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            /*
            // Voor Veel op Veel relaties, alles bij de HasKey steken
            builder.Entity<GebruikerCommentaar>().HasKey(gc => new
            {
                gc.GebruikerID,
                gc.CommentaarID
            });
            */
        }
    }
}
