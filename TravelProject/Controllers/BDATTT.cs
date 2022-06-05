using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelProject.Models;

namespace TravelProject.Controllers
{
    public class BDATTT : Controller
    {
        // GET: BDATTT_ThaiBaTuyen
        public ActionResult Index()
        {
            Context md = new Context();
            var model = md.HocViens.ToList();
            return View(model);
        }
    }
}