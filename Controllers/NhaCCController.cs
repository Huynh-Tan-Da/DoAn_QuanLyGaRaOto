using QuanLyGaraOto.Models.Business;
using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class NhaCCController : Controller
    {
        private AutoGaraEntities db = new AutoGaraEntities();
        // GET: NhaCC
        [CustomRoleProvider(RoleName = "Admin")]
        public ActionResult Index()
        {
            var model = db.NhaCungCaps.OrderByDescending(x => x.IDNCC);
            return View(model.ToList());
        }

        public JsonResult Delete(int ID)
        {

            try
            {
                var model = db.NhaCungCaps.Find(ID);
                db.NhaCungCaps.Remove(model);
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
        public ActionResult frmAdd(NhaCungCap entity)
        {
            try
            {
                var model = new NhaCungCap();
                model.Ten = entity.Ten;
                
                db.NhaCungCaps.Add(model);
                db.SaveChanges();
                TempData["add_success"] = "Thêm Nhà cung cấp thành công";
                return Redirect("/nhacc/index");

            }
            catch
            {
                TempData["add_success"] = "Thêm Nhà cung cấp KHÔNG thành công";
                return Redirect("/nhacc/index");
            }
        }


        [HttpPost]
        public ActionResult frmEdit(NhaCungCap entity)
        {
            try
            {
                var model = db.NhaCungCaps.Find(entity.IDNCC);
                model.Ten = entity.Ten;
                db.SaveChanges();
                TempData["add_success"] = "Cập nhật Nhà cung cấp thành công";
                return Redirect("/nhacc/index");
            }
            catch
            {
                TempData["add_success"] = "Cập nhật Nhà cung cấp KHÔNG thành công";
                return Redirect("/nhacc/index");
            }
        }

        public JsonResult GetByID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.NhaCungCaps.Find(ID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}