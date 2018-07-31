using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommerceV3.Models;
using CommerceV3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommerceV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController (ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            //LinQ sorgulaması ile Slides veritabanındaki verileri listeleme
            ViewBag.Slides = db.Slides.Where(s => s.IsPublished == true).OrderBy(o=>o.Position).Take(10).ToList(); //Method-based LINQ to Entities
            ViewBag.Products = (from p in db.Products.Include(i=>i.Category) where p.IsPublished == true orderby p.CreateDate descending select p).Take(8).ToList(); //Query-based LINQ to Entities
              
            return View();

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Countries = new SelectList(db.Regions.Where(r => r.RegionType == RegionType.Country).OrderBy(o => o.Name).ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string fullName, string email, string country, string city, string subject, string message)
        {
            return View();
        }

        public IEnumerable<Region> GetCities (string parentRegionId)
        {
            return db.Regions.Where(r => r.RegionType == RegionType.City && r.ParentRegionId == parentRegionId).OrderBy(o => o.Name).ToList();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
