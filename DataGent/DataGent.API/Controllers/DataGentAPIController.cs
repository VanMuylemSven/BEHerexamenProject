using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataGent.Models;
using DataGent.Services;
using DataGent.Web.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataGent.API.Controllers
{
    [Route("api/[controller]")]
    public class DataGentAPIController : Controller
    {

        private readonly IDataGentService _dataGentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private static StedenIndex_VM _stedenIndex_VM;

        public DataGentAPIController(IDataGentService dataGentService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._dataGentService = dataGentService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }



        // GET: DataGent
        public ActionResult Index()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Stad> GetSteden()
        {

            //Get data from URL
            const string URL = "https://datatank.stad.gent/4/milieuennatuur/ecoplan.json";

            var data = new WebClient().DownloadString(URL);
            var stedenList = JsonConvert.DeserializeObject<List<Stad>>(data);

            //Give Id's to objects because why the hell doesn't this API use ID's?
            int teller = 0;
            foreach (var item in stedenList)
            {
                item.Id = teller;
                teller++;
            }

            return stedenList;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
