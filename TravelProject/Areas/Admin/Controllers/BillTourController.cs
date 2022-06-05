using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelProject.Models;

namespace TravelProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class BillTourController : Controller
    {
        // GET: Admin/BillTour
        private TravelContext travel = new TravelContext();
        private object form;

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
        public ActionResult EditBillTour(int id)
        {
            var item = travel.PhieuDatTours.Find(id);
            var NguoiLienHe = travel.NguoiLienHes.Where(p => p.MaNguoiLienHe == item.MaNguoiLienHe).FirstOrDefault();
            ViewBag.KhachHang = travel.KhachHangs.Where(p => p.MaNguoiLienHe == NguoiLienHe.MaNguoiLienHe).ToList();
            ViewBag.tour = travel.Tours.Where(p => p.MaTour == item.MaTour).FirstOrDefault();
            ViewBag.PhieuTour = item;
            ViewBag.BangGia = travel.BangGias.Where(p => p.MaTour == item.MaTour).FirstOrDefault();
            ViewBag.KhachHang_Count = travel.KhachHangs.Where(p => p.MaNguoiLienHe == NguoiLienHe.MaNguoiLienHe).Count();
            return View(NguoiLienHe);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult EditBillTour()
        {
            KhachHang kh = new KhachHang();
            NguoiLienHe nlh = new NguoiLienHe();
            PhieuDatTour pdt = new PhieuDatTour();

            return View();
        }
        [HttpPost]

        public void CapnhatPhieuDatTour(int maphieutour, string pickupplace, int matour, int gia)

        {
            TravelContext md = new TravelContext();
            PhieuDatTour pdt = new PhieuDatTour();
            pdt = md.PhieuDatTours.Where(p => p.MaPhieuDat == maphieutour).FirstOrDefault();
            pdt.DiaDiemDon = pickupplace;
            pdt.MaTour = matour;
            pdt.TongGia = gia;
            md.SaveChanges();

        }
        [HttpPost]
        public void CapnhatKH(int manlh, string mangten, string mangdiachi, string mangloai, string manggt, string mangngay, int soluong)
        {
            TravelContext md = new TravelContext();
            List<KhachHang> kh_delete = new List<KhachHang>();
            kh_delete = md.KhachHangs.Where(p => p.MaNguoiLienHe == manlh).ToList();
            foreach (var item in kh_delete)
            {
                md.KhachHangs.Remove(item);
                md.SaveChanges();
            }


            int idkh = md.KhachHangs.Count() + 1;
            dynamic ten = JsonConvert.DeserializeObject(mangten);
            dynamic diachi = JsonConvert.DeserializeObject(mangdiachi);
            dynamic loai = JsonConvert.DeserializeObject(mangloai);
            dynamic gioitinh = JsonConvert.DeserializeObject(manggt);
            dynamic ngaysinh = JsonConvert.DeserializeObject(mangngay);
            for (int i = 0; i < soluong; i++)
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = idkh;
                kh.TenKH = ten[i].ToString();
                kh.NgaySinh = ngaysinh[i].ToString();
                kh.DiaChi = diachi[i].ToString();
                kh.GioiTinh = gioitinh[i].ToString();
                kh.Loai = loai[i].ToString();
                kh.MaNguoiLienHe = manlh;
                md.KhachHangs.Add(kh);
                idkh++;
            }
            md.SaveChanges();

        }
        [HttpPost]
        public JsonResult CapnhatTTNLH(FormCollection form)
        {
            JsonResult js = new JsonResult();
            string ma = form["ma"];
            string ten= form["ten"];
            string email= form["email"];
            string diachi= form["diachi"];
            string dienthoai= form["dienthoai"];
            string note= form["note"];
            string mangten= form["mangten"];
            string mangdiachi= form["mangdiachi"];
            string mangloai= form["mangloai"];
            string manggt= form["manggt"];
            string mangngay= form["mangngay"];
            int soluong= int.Parse(form["soluong"]);

            // Cập nhập Người liên hệ
            TravelContext md = new TravelContext();
            NguoiLienHe nlh = new NguoiLienHe();
            int manlh = int.Parse(ma.ToString());
            nlh = md.NguoiLienHes.Where(p => p.MaNguoiLienHe == manlh).FirstOrDefault();

            nlh.TenNguoiLienHe = ten;
            nlh.SoDienThoai = dienthoai;
            nlh.Email = email;
            nlh.GhiChu = note;
            nlh.Diachi = diachi;
            md.SaveChanges();

            List<KhachHang> kh_delete = new List<KhachHang>();
            kh_delete = md.KhachHangs.Where(p => p.MaNguoiLienHe == manlh).ToList();
            foreach (var item in kh_delete)
            {
                md.KhachHangs.Remove(item);
                md.SaveChanges();
            }


            int idkh = md.KhachHangs.ToList().Last().MaKH + 1;
            dynamic tenkh = JsonConvert.DeserializeObject(mangten);
            dynamic diachikh = JsonConvert.DeserializeObject(mangdiachi);
            dynamic loaikh = JsonConvert.DeserializeObject(mangloai);
            dynamic gioitinhkh = JsonConvert.DeserializeObject(manggt);
            dynamic ngaysinhkh = JsonConvert.DeserializeObject(mangngay);
            for (int i = 0; i < soluong; i++)
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = idkh;
                kh.TenKH = tenkh[i].ToString();
                kh.NgaySinh = ngaysinhkh[i].ToString();
                kh.DiaChi = diachikh[i].ToString();
                kh.GioiTinh = gioitinhkh[i].ToString();
                kh.Loai = loaikh[i].ToString();
                kh.MaNguoiLienHe = manlh;
                md.KhachHangs.Add(kh);
                md.SaveChanges();
                idkh++;
            }
            // 
            int maphieutour= int.Parse(form["maphieutour"]);
            string pickupplace= form["pickupplace"];
            int matour= int.Parse(form["matour"]);
            int gia = int.Parse(form["gia"]);
            PhieuDatTour pdt = new PhieuDatTour();
            pdt = md.PhieuDatTours.Where(p => p.MaPhieuDat == maphieutour).FirstOrDefault();
            pdt.DiaDiemDon = pickupplace;
            pdt.MaTour = matour;
            pdt.TongGia = gia;
            md.SaveChanges();
            js.Data = new
            {
                status = "OK",
              


            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteBillTour(int id)
        {

            PhieuDatTour pdt = travel.PhieuDatTours.Find(id);
            var manlh = pdt.MaNguoiLienHe;
            List<KhachHang> kh = travel.KhachHangs.Where(p => p.MaNguoiLienHe == manlh).ToList();
            foreach (var item in kh)
            {
                travel.KhachHangs.Remove(item);
                travel.SaveChanges();
            }

            NguoiLienHe nlh = travel.NguoiLienHes.Where(p => p.MaNguoiLienHe == manlh).FirstOrDefault();
            travel.NguoiLienHes.Remove(nlh);
            travel.SaveChanges();
            travel.PhieuDatTours.Remove(pdt);
           
            travel.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}