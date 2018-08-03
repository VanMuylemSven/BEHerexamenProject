using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataGent.Web.Data;
using DataGent.Web.Models;
using DataGent.Web.Services;
using DataGent.Models.Data;
using DataGent.Repositories;
using DataGent.Services;

namespace DataGent.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));*/
            services.AddDbContext<StedenDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<DataGent.Models.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StedenDbContext>()
                .AddDefaultTokenProviders();


            /*services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/

            //To Do: 
            //Add Repositories
            services.AddScoped<IDataGentRepoAsync, DataGentRepoAsync>();

            //Add services
            services.AddScoped<IDataGentService, DataGentService>();
            /*De nodige Identity services moeten geregistreerd en geconfigureerd worden in Startup.cs. 
             * >> in ConfigureServices: */
            /*services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<StedenDbContext>();//ToDo: StedenDbCOntext => Stedencontext ???*/
            
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //>> in Configure wordt de middleware toegepast:
            app.UseAuthentication(); //aanbrengen vóór UseMvc()!

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=DataGent}/{action=Index}/{id?}");
            });
        }
    }
}
