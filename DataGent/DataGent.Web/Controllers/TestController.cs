using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGent.Models;
using DataGent.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGent.Web.Controllers
{
    public class TestController : Controller
    {

        /*admin@test.be
         * pass: Admin+1
         * */

        private IDataGentRepoAsync _dataGentRepoAsync;

        public TestController(IDataGentRepoAsync dataGentRepoAsync)
        {
            this._dataGentRepoAsync = dataGentRepoAsync;
        }


        // GET: Test
        public ActionResult Index()
        {
            //Stad stad = new Stad() {Id = 1, Naam = "Roomer", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" };
            List<Stad> stedenTest = new List<Stad>();
            stedenTest.Add(new Stad() { Id = 1, Naam = "Roomer", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "something", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "test", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });
            stedenTest.Add(new Stad() { Id = 1, Naam = "testie", Straat = "straat", Categorie = "categorie", Gemeente = "gemeente", Lat = "0", Long = "0", Nummer = "10", Opmerkingen = "opmerking", Postcode = "9999", Telefoon = "04712464787", Webadres = "www.howest.be" });


            var stedenList = _dataGentRepoAsync.GetDataGentFromURL();

            return View(stedenList);
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
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


        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
 