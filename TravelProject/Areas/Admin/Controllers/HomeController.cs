using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelProject.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace TravelProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private TravelContext travel = new TravelContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null && Session["TaiKhoan"] != "")
            {
                ThanhVien tv = (ThanhVien)Session["TaiKhoan"];
                if (@tv.username == "admin")
                { return View(); }
                else
                {
                    return Redirect("/Home/Login");
                }
            }
            return Redirect("/Home/Login");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemmoiTour(Tour model, HttpPostedFileBase LinkImage)
        {
            foreach( var item in travel.Tours)
            {
                if(model.MaTour == item.MaTour)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Lỗi! Không thể lưu được!');</script>") ;
                    
                }
            }
            if (LinkImage != null && LinkImage.ContentLength > 0)
            {
                int id = model.MaTour;
                string _Filename = "";
                int index = LinkImage.FileName.IndexOf('.');
                _Filename = "images/tour/" + id.ToString() + "." + LinkImage.FileName.Substring(index + 1); ;
                string _path = Path.Combine(Server.MapPath("~/Content/"), _Filename);
                LinkImage.SaveAs(_path);
                model.LinkImage = _Filename;


            }
            travel.Tours.Add(model);
            travel.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult EditTour(int id)
        {
            var item = travel.Tours.Find(id);
            return View(item);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditTour(Tour model, string link_image, HttpPostedFileBase LinkImage)
        {
            if (string.IsNullOrEmpty(model.TenTour) == true)
            {
                ModelState.AddModelError("", "Ten khong duoc de trong");
                return View(model);
            }

            var updateTour = travel.Tours.Find(model.MaTour);
            updateTour.TenTour = model.TenTour;
            updateTour.Mota = model.Mota;
            updateTour.Gia = model.Gia;
            updateTour.MaDiaDanh = model.MaDiaDanh;
            updateTour.MaChiTietTour = model.MaChiTietTour;
            updateTour.NumView = model.NumView;

            updateTour.DiaDiem = model.DiaDiem;
            updateTour.NumDay = model.NumDay;
            updateTour.NumStar = model.NumStar;
            
            if (LinkImage != null && LinkImage.ContentLength > 0)
            {
                string _Filename = "";
                _Filename = link_image;
                string _path = Path.Combine(Server.MapPath("~/Content/"), _Filename);
                LinkImage.SaveAs(_path);
                updateTour.LinkImage = _Filename;
                

            }
            travel.SaveChanges();
            return RedirectToAction("Index");
            //if (id > 0)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "khong the luu duoc");
            //    return View(model);
            //}
        }
        public ActionResult DeleteTour(int id)
        {
               Tour foundTour = travel.Tours.Find(id);
               List<DanhGia> dgia = travel.DanhGias.Where(x => x.MaTour == id).ToList();
               List<BangGia> bang = travel.BangGias.Where(x => x.MaTour == id).ToList();
               List<PhieuDatTour> phieu = travel.PhieuDatTours.Where(x => x.MaTour == id).ToList();
               for (int i = 0; i < dgia.Count; i++)
               {
                    travel.DanhGias.Remove(dgia[i]);
               }
               for (int i = 0; i < bang.Count; i++)
               {
                    travel.BangGias.Remove(bang[i]);
               }
               for (int i = 0; i < phieu.Count; i++)
               {
                    travel.PhieuDatTours.Remove(phieu[i]);
               }
               travel.Tours.Remove(foundTour);
               travel.SaveChanges();

             
            return RedirectToAction("Index");
        }
       
    }
}