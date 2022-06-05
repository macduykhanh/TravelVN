using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelProject.Models;

namespace TravelProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private TravelContext travel = new TravelContext();
        public ActionResult Index()
        {
            TravelContext mdt = new TravelContext();
            var model = mdt.DiaDanhs.ToList();
            return View(model);
        }
        public ActionResult View()
        {
            TravelContext mdt = new TravelContext();
            var model = mdt.DiaDanhs.ToList();
            return View(model);
        }

    }
}