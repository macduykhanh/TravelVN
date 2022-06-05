﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelProject.Models;
using PagedList.Mvc;
using PagedList;

namespace TravelProject.Controllers
{
    [AllowAnonymous]
    public class DestinationController : Controller
    {
        private TravelContext travel = new TravelContext();
        // GET: Destination
        public ActionResult Index(int? page, string search="", int? mavung=0)
        {
            TravelContext mdt = new TravelContext();
            var model = mdt.Vung_Diadanh.ToList();
            if (mavung == 0 && search != "")
            {
                model = mdt.Vung_Diadanh.Where(x => x.TenDiaDanh.Contains(search) || x.TenVung.Contains(search)).ToList();
            }
            if (mavung !=0 && search == "") {
                model = mdt.Vung_Diadanh.Where(x=> x.Mavung == mavung).ToList();
            }
            if(mavung != 0 && search != "")
            {
                model = mdt.Vung_Diadanh.Where(x => ((x.TenDiaDanh.Contains(search) || x.TenVung.Contains(search)) && x.Mavung == mavung)).ToList();
            }
            int pageSize = 4;
            int no_of_page = (page ?? 1);
            ViewBag.search = search;
            ViewBag.mavung = mavung;
            var des = model.ToPagedList(no_of_page, pageSize);
            if (des.Count() != 0)
            {
                return View(model.ToPagedList(no_of_page, pageSize));
            }
            else
            {
                TempData["Destination"] = "<script>alert('Not found destination!');</script>";
                return RedirectToAction("Index", "Destination");
            }
           
           
        }
        
        public ActionResult DestinationDetail(int? id)
        {

            if (id==null)
            {
                return RedirectToAction("Index", "Destination");
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng
            DiaDanh dd = travel.DiaDanhs.SingleOrDefault(n => n.MaDiaDanh == id);
            if (dd == null)
            {
                //Thông báo nếu như không có sản phẩm đó
                return HttpNotFound();
            }
            return View(dd);
        }
        public ActionResult ListDesByArea(int mavung)
        {
            var model = travel.Vung_Diadanh.ToList();
            if (mavung != 0) {
                TravelContext mdt = new TravelContext();
                model = mdt.Vung_Diadanh.Where(x => x.Mavung == mavung).ToList();
            }
            return PartialView("DesByArea", model);
        }
        public ActionResult ListDesByName(string search)
        {
            TravelContext mdt = new TravelContext();
            var model = mdt.Vung_Diadanh.Where(x => x.TenDiaDanh.Contains(search)).ToList();
            return PartialView("DesByArea", model);
        }
    }
}