using QuanLyGaraOto.Models.Business;
using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class HieuXeController : Controller
    {
        // GET: HieuXe
        private AutoGaraEntities db = new AutoGaraEntities();
        [CustomRoleProvider(RoleName = "Admin")]
        public ActionResult Index()
        {
            var model = db.HieuXes.OrderByDescending(x => x.IDHieuXe);
            return View(model.ToList());
        }

        public JsonResult Delete(int ID)
        {

            try
            {
                var model = db.HieuXes.Find(ID);
                db.HieuXes.Remove(model);
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
        public ActionResult frmAdd(HieuXe entity)
        {
            try
            {
                var model = new HieuXe();
                model.HieuXe1 = entity.HieuXe1;

                db.HieuXes.Add(model);
                db.SaveChanges();
                TempData["add_success"] = "Thêm hiệu xe thành công";
                return Redirect("/hieuxe/index");

            }
            catch
            {
                TempData["add_success"] = "Thêm hiệu xe KHÔNG thành công";
                return Redirect("/hieuxe/index");
            }
        }


        [HttpPost]
        public ActionResult frmEdit(HieuXe entity)
        {
            try
            {
                var model = db.HieuXes.Find(entity.IDHieuXe);
                model.HieuXe1 = entity.HieuXe1;
                db.SaveChanges();
                TempData["add_success"] = "Cập nhật hiệu xe thành công";
                return Redirect("/hieuxe/index");
            }
            catch
            {
                TempData["add_success"] = "Cập nhật hiệu xe KHÔNG thành công";
                return Redirect("/hieuxe/index");
            }
        }

        public JsonResult GetByID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.HieuXes.Find(ID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}