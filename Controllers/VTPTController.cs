using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class VTPTController : Controller
    {

        // GET: VTPT
        private AutoGaraEntities db = new AutoGaraEntities();
        public ActionResult Index()
        {
            var model = db.VTPTs.OrderByDescending(x => x.IDVTPT);
            return View(model.ToList());
        }

        public JsonResult Delete(int ID)
        {

            try
            {
                var model = db.VTPTs.Find(ID);
                db.VTPTs.Remove(model);
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
        public ActionResult frmAdd(VTPT entity)
        {
            try
            {
                entity.SoLuongTon = 0;
                db.VTPTs.Add(entity);
                db.SaveChanges();
                TempData["add_success"] = "Thêm vật tư phụ tùng thành công";
                return Redirect("/vtpt/index");

            }
            catch
            {
                TempData["add_success"] = "Thêm vật tư phụ tùng KHÔNG thành công";
                return Redirect("/vtpt/index");
            }
        }


        [HttpPost]
        public ActionResult frmEdit(VTPT entity)
        {
            try
            {
                var model = db.VTPTs.Find(entity.IDVTPT);
                model.Ten = entity.Ten;
                model.DonGia = entity.DonGia;
                model.BaoHanh = entity.BaoHanh;
                db.SaveChanges();
                TempData["add_success"] = "Cập nhật vật tư phụ tùng thành công";
                return Redirect("/vtpt/index");
            }
            catch
            {
                TempData["add_success"] = "Cập nhật vật tư phụ tùng KHÔNG thành công";
                return Redirect("/vtpt/index");
            }
        }

        public JsonResult GetByID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.VTPTs.Find(ID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}