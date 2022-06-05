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

namespace TravelProject.Areas.Admin.Controllers
{
     [AllowAnonymous]
     public class DestinationController : Controller
    {
          // GET: Admin/Destination
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
          [HttpPost]
          public ActionResult ThemmoiDes(DiaDanh model)
          {
               foreach (var item in travel.DiaDanhs)
               {
                    if (model.MaDiaDanh == item.MaDiaDanh)
                    {
                         return Content("<script language='javascript' type='text/javascript'>alert('Lỗi! Không thể lưu được!');</script>");

                    }
               }

               travel.DiaDanhs.Add(model);
               travel.SaveChanges();
               return RedirectToAction("Index");

          }
          public ActionResult EditDes(int id)
          {
               var item = travel.DiaDanhs.Find(id);
               return View(item);
          }
          [HttpPost]
          public ActionResult EditDes(DiaDanh model)
          {

               var updateTour = travel.DiaDanhs.Find(model.MaDiaDanh);

               updateTour.TenDiaDanh = model.TenDiaDanh;
               updateTour.MoTa = model.MoTa;
               updateTour.Img = model.Img;
               updateTour.MaVung = model.MaVung;
               updateTour.Img2 = model.Img2;


               var id = travel.SaveChanges();
               if (id > 0)
               {
                    return RedirectToAction("Index");
               }
               else
               {
                    ModelState.AddModelError("", "khong the luu duoc");
                    return View(model);
               }
          }
        //public ActionResult DeleteDes(int id)
        //{
        //    List<int> idTours = new List<int>();
        //    DiaDanh foundDes = travel.DiaDanhs.Find(id);
        //    List<Tour> tour = travel.Tours.Where(x => x.MaDiaDanh == id).ToList();
        //    List<DiaDanh_DanhMucTour> dd = travel.DiaDanh_DanhMucTour.Where(x => x.MaDiaDanh == id).ToList();
        //    //List<DanhGia> dgia = new List<DanhGia>();
        //    List<BangGia> bang = new List<BangGia>();
        //    List<PhieuDatTour> phieu = new List<PhieuDatTour>();
            
        //    for (int i = 0; i < dd.Count; i++)
        //    {
        //        travel.DiaDanh_DanhMucTour.Remove(dd[i]);
        //    }
        //    for (int i = 0; i < tour.Count; i++)
        //    {
        //        int k = tour[i].MaTour;
        //        idTours.Add(k);

        //    }

        //    for (int j = 0; j < idTours.Count; j++)
        //    {
                
        //         var query = travel.DanhGias.Where(x => x.MaTour == idTours[j]);
        //        List<DanhGia> dgia = query.ToList();
               
        //        bang = travel.BangGias.Where(x => x.MaTour == idTours[j]).ToList();
        //        phieu = travel.PhieuDatTours.Where(x => x.MaTour == idTours[j]).ToList();

        //        //for (int i = 0; i < travel.DanhGias.Where(x => x.MaTour == idTours[j]).ToList().Count; i++)
        //        //{
        //        //    travel.DanhGias.Remove(dgia[i]);
        //        //}


        //        for (int i = 0; i < bang.Count; i++)
        //        {
        //            travel.BangGias.Remove(bang[i]);
        //        }
        //        for (int i = 0; i < phieu.Count; i++)
        //        {
        //            travel.PhieuDatTours.Remove(phieu[i]);
        //        }


        //        travel.Tours.Remove(tour[j]);

        //    }

        //    travel.DiaDanhs.Remove(foundDes);
        //    travel.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}