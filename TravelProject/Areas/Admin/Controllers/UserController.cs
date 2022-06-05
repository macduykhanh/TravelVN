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
    public class UserController : Controller
    {
        private TravelContext travel = new TravelContext();
        // GET: Admin/User
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
        public ActionResult DeleteUser(int id)
        {
            ThanhVien foundTv = travel.ThanhViens.Find(id);
            List<DanhGia> dgia = travel.DanhGias.Where(x => x.MaThanhVien == id).ToList();
           List<KhachHang> khang = travel.KhachHangs.Where(x => x.MaThanhVien == id).ToList();
            /*travel.KhachHangs.Remove(khang);*/
            for(int i = 0; i < dgia.Count; i++)
            {
                travel.DanhGias.Remove(dgia[i]);
            }
            for (int i = 0; i < khang.Count; i++)
            {
                travel.KhachHangs.Remove(khang[i]);
            }
            travel.ThanhViens.Remove(foundTv);
            travel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}