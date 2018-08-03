using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataGent.API.Controllers;
using DataGent.Models;
using DataGent.Repositories;
using DataGent.Services;
using DataGent.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataGent.Web.Controllers
{
    public class DataGentController : Controller
    {
        /*admin@test.be
         * pass: Admin+1
         * */
        //private IDataGentRepoAsync _dataGentRepoAsync;
        private readonly IDataGentService _dataGentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private static StedenIndex_VM _stedenIndex_VM;

        public DataGentController(IDataGentService dataGentService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._dataGentService = dataGentService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            //this._dataGentRepoAsync = dataGentRepoAsync;
        }



        // GET: DataGent
        public ActionResult Index()
        {
            //Get steden from API
            var stedenList = new DataGentAPIController(_dataGentService, _userManager, _signInManager).GetSteden();

            //Stad stad = new Stad() {Id = 1, Naam = "Roomer", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" };
            /*List<Stad> stedenTest = new List<Stad>();
            stedenTest.Add(new Stad() { Id = 1, Naam = "Roomer", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "something", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "test", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "testie", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            */

            List<Commentaar> commentaarList = new List<Commentaar>();
            //Now check if any comments were entered from the DataBase, and fill them in in the list
            //1. Get all comments from the currently logged in user
            var stedenCommList = new List<StadCommentaar>();
            foreach (var item in stedenList.ToList())
            {
                stedenCommList.Add(new StadCommentaar()
                {
                    Stad = item,
                    Commentaar = null
                });
            }
            if (_signInManager.IsSignedIn(User))
            {
                //Get current userId
                var userId = _userManager.GetUserId(HttpContext.User);
                //Get all comments associated with that Id
                commentaarList = _dataGentService.GetCommentaarByUserId(userId).ToList();


                //Put all objects into ViewModel
                foreach (var item in commentaarList)
                {
                    //Find the corresponding comment in the StadCOmment list AND check for identical users
                    var stad = stedenCommList.Find(stadCom => stadCom.Stad.Id == item.StadId && userId == item.UserId);
                    stedenCommList[stad.Stad.Id].Commentaar = item;
                }
                
                /*foreach (var item in stedenCommList)
                {
                    //Now find a comment where that Stad.Id is the same as commentaar.stadId
                    var commentaar = commentaarList.Find(comment => comment.StadId == item.Id && comment.UserId == userId);
                    if(commentaar != null)
                    {
                        item.Commentaar = commentaar;
                    }
                    stedenCommList.Add(stadComm);
                }*/
            }

            var stedenVm = new StedenIndex_VM()
            {
                StedenList = stedenCommList
            };
            _stedenIndex_VM = stedenVm;

            return View(stedenVm);
        }

        /*
        // GET: DataGent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DataGent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataGent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */


        public ActionResult Create(int id)
        {
            //ViewBag.Naam = naam;
            CommentaarCreate_VM vm = new CommentaarCreate_VM()
            {
                Stad = _dataGentService.GetStadFromId(id),
                Commentaar = null
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CommentaarText, Tijdstip")] int id, IFormCollection collection) //Bind = protect from overposting
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Creating  object to POST
                    Commentaar commentaar = new Commentaar
                    {
                        UserId = _userManager.GetUserId(HttpContext.User),
                        StadId = id,
                        CommentaarText = collection["Commentaar"],
                        Tijdstip = DateTime.Now
                    };

                    var result = _dataGentService.PostCommentaar(commentaar);
                    /*//Send a mail to the user with the comment
                   string body = "Notification that you commented: \n"
                        + commentaar.CommentaarText
                        + " at " + commentaar.Tijdstip;
                    _dataGentService.SendMail(User.Identity.Name, "You posted a comment", body);*/

                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit([Bind("CommentaarText, Tijdstip")] int id, int stadId)
        {
            StadCommentaar stadCom = _stedenIndex_VM.StedenList.ToList()[stadId];
            ViewBag.CommentaarText = stadCom.Commentaar.CommentaarText;
            return View(stadCom);

            
        }

        // POST: DataGent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int stadId, IFormCollection collection)
        {
            try
            {
                // Creating  object to POST
                Commentaar commentaar = new Commentaar
                {
                    UserId = _userManager.GetUserId(HttpContext.User),
                    StadId = stadId,
                    CommentaarText = collection["Commentaar"],
                    Tijdstip = DateTime.Now
                };

                var result = _dataGentService.UpdateCommentaar(commentaar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataGent/Delete/5
        public ActionResult Delete(int id, int stadId)
        {
            StadCommentaar stadCom = _stedenIndex_VM.StedenList.ToList()[stadId];
            ViewBag.CommentaarText = stadCom.Commentaar.CommentaarText;
            return View(stadCom);
        }

        // POST: DataGent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int commentaarId, int stadId, IFormCollection collection)
        {
            try
            {
                // Add delete logic here
                Commentaar commentaar = new Commentaar
                {
                    CommentaarId = commentaarId,
                    UserId = _userManager.GetUserId(HttpContext.User),
                    StadId = stadId,
                    CommentaarText = collection["Commentaar"],
                    Tijdstip = DateTime.Now
                };

                _dataGentService.DeleteCommentaar(commentaar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Test()
        {
            Commentaar commentaar = new Commentaar()
            {
                CommentaarId = 0
            };
            return View(commentaar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Test(Commentaar commentaar, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                if (string.IsNullOrEmpty(commentaar.CommentaarText))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}