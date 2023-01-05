using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class DangNhapController : Controller
    {
        private AutoGaraEntities db = new AutoGaraEntities();
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult frmLogin(NhanVien model)
        {
            if (db.NhanViens.Count(x => x.TenDN == model.TenDN && x.MatKhau == model.MatKhau) > 0)
            {
                Session["nhanvien"] = db.NhanViens.SingleOrDefault(x => x.TenDN == model.TenDN && x.MatKhau == model.MatKhau);
                return Redirect("/home/thongke");
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác";
                return Redirect("/dangnhap/index");
            }
        }

        public ActionResult DoiMK()
        {
            return View();
        }


        [HttpPost]
        public ActionResult frmChangePass(string Old_Pass, string New_Pass)
        {
            var nv = Session["nhanvien"] as NhanVien;
            if(nv.MatKhau != Old_Pass)
            {
                TempData["error"] = "Mật khẩu cũ không chính xác, vui lòng nhập lại";
                return Redirect("/dangnhap/doimk");
            }
            else
            {
                var tk = db.NhanViens.Find(nv.IDTK);
                tk.MatKhau = New_Pass;
                db.SaveChanges();
                Session["nhanvien"] = null;
                TempData["error"] = "Đổi mật khẩu thành công, vui lòng đăng nhập lại để tiếp tục.";
                return Redirect("/dangnhap/index");
            }
        }


        public ActionResult Logout()
        {
            Session["nhanvien"] = null;
            return Redirect("/dangnhap/index");
        }
    }
}