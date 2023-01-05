using QuanLyGaraOto.Models.Business;
using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class TienCongController : Controller
    {
        // GET: TienCong
        private AutoGaraEntities db = new AutoGaraEntities();
        [CustomRoleProvider(RoleName = "Admin")]
        public ActionResult Index()
        {
            var model = db.TienCongs.OrderByDescending(x => x.IDTienCong);
            return View(model.ToList());
        }

        public JsonResult Delete(int ID)
        {

            try
            {
                var model = db.TienCongs.Find(ID);
                db.TienCongs.Remove(model);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }

        }


        [HttpPost]
        public ActionResult frmAdd(TienCong entity)
        {
            try
            {
                var model = new TienCong();
                model.NoiDung = entity.NoiDung;
                model.TienCong1 = entity.TienCong1;

                db.TienCongs.Add(model);
                db.SaveChanges();
                TempData["add_success"] = "Thêm tiền công thành công";
                return RedirectToAction("index");

            }
            catch
            {
                TempData["add_success"] = "Thêm tiền công KHÔNG thành công";
                return RedirectToAction("index");
            }
        }


        [HttpPost]
        public ActionResult frmEdit(TienCong entity)
        {
            try
            {
                var model = db.TienCongs.Find(entity.IDTienCong);
                model.NoiDung = entity.NoiDung;
                model.TienCong1 = entity.TienCong1;
                db.SaveChanges();
                TempData["add_success"] = "Cập nhật tiền công thành công";
                return RedirectToAction("index");
            }
            catch
            {
                TempData["add_success"] = "Cập nhật tiền công KHÔNG thành công";
                return RedirectToAction("index");
            }
        }

        public JsonResult GetByID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.TienCongs.Find(ID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}