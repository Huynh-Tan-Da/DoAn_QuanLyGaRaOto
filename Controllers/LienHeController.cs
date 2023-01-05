using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class LienHeController : Controller
    {
        private AutoGaraEntities db = new AutoGaraEntities();
        // GET: LienHe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSach()
        {
            var model = db.LienHes.OrderByDescending(x => x.NgayLH).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult frmContact(LienHe entity)
        {
            entity.NgayLH = DateTime.Now;
            entity.NgayDat = DateTime.Parse(entity.NgayDat.Value.ToString("yyyy-MM-dd hh:mm"));
            entity.TrangThai = false;
            db.LienHes.Add(entity);
            db.SaveChanges();
            TempData["success"] = "Đặt lịch thành công. Cám ơn bạn đã tin tưởng và sử dụng dịch vụ của chúng tôi. Đội ngũ chăm sóc khách hàng sẽ liên hệ bạn trong thời gian sớm nhất";
            return Redirect("/lienhe");
        }

        public JsonResult ChangeStatus(int ID)
        {

            var model = db.LienHes.Find(ID);
            model.TrangThai = true;
            db.SaveChanges();
            return Json(new
            {
                status = true
            });


        }
    }
}