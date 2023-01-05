using ClosedXML.Excel;
using QuanLyGaraOto.Models.Business;
using QuanLyGaraOto.Models.DTO;
using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        private AutoGaraEntities db = new AutoGaraEntities();
        [CustomRoleProvider(RoleName = "Admin")]
        public ActionResult Index()
        {
            var model = db.NhanViens.Where(x => x.TenDN != "admin").OrderByDescending(x => x.IDTK);
            ViewBag.lstPhanQuyen = db.PhanQuyens.ToList();
            return View(model.ToList());
        }


        public ActionResult Luong(int thang = 0)
        {
            var lstLuong = new List<LuongDTO>();
           
            if (thang != 0)
            {
                foreach (var item in db.NhanViens.Where(x => x.TenDN != "admin").ToList())
                {
                    var luong = new LuongDTO();
                    luong.TenNhanVien = item.Ten;
                    luong.LuongCung = item.Luong;

                    var model = (from detail in db.ChiTietPSCs
                                 join psc in db.PhieuSuaChuas on detail.IDPSC equals psc.IDPSC
                                 join tc in db.TienCongs on detail.IDTienCong equals tc.IDTienCong
                                 where psc.IDTK == item.IDTK && psc.NgaySuaChua.Month == thang
                                 select new PhieuSuaChuaDTO
                                 {
                                     Luong = tc.TienCong1
                                 }).Sum(x => x.Luong);
                    if (model != null)
                        luong.Thuong = (model * 20) / 100;
                    else
                        luong.Thuong = 0;
                    luong.TongLuong = luong.LuongCung + luong.Thuong;
                    lstLuong.Add(luong);

                }
                ViewBag.Month = thang;
            }
            else
            {
                foreach (var item in db.NhanViens.Where(x => x.TenDN != "admin").ToList())
                {
                    var luong = new LuongDTO();

                    luong.TenNhanVien = item.Ten;
                    luong.LuongCung = item.Luong;

                    var model = (from detail in db.ChiTietPSCs
                                 join psc in db.PhieuSuaChuas on detail.IDPSC equals psc.IDPSC
                                 join tc in db.TienCongs on detail.IDTienCong equals tc.IDTienCong
                                 where psc.IDTK == item.IDTK && psc.NgaySuaChua.Month == DateTime.Now.Month
                                 select new PhieuSuaChuaDTO()
                                 {
                                     Luong = tc.TienCong1
                                 }).Sum(x => x.Luong);
                    if (model != null)
                        luong.Thuong = (model * 20) / 100;
                    else
                        luong.Thuong = 0;
                    luong.TongLuong = luong.LuongCung + luong.Thuong;
                    lstLuong.Add(luong);

                }
                ViewBag.Month = DateTime.Now.Month;
            }

            ViewBag.lstLuong = lstLuong.OrderBy(x => x.TenNhanVien).ToList();
            return View();
        }

        public JsonResult Delete(int ID)
        {

            try
            {
                var model = db.NhanViens.Find(ID);
                db.NhanViens.Remove(model);
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
        public ActionResult frmAdd(NhanVienDTO entity)
        {
            try
            {
                if(db.NhanViens.Count(x => x.TenDN == entity.TenDN) > 0)
                {
                    TempData["add_success"] = "Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.";
                    TempData["alert"] = "alert-danger";
                    return Redirect("/nhanvien/index");
                }
                var nv = new NhanVien();
                nv.Ten = entity.Ten;
                nv.CMND = entity.CMND;
                nv.DiaChi = entity.DiaChi;
                nv.SDT = entity.SDT;

                nv.TenDN = entity.TenDN;
                nv.MatKhau = entity.MatKhau;
                nv.IDPhanQuyen = entity.IDPhanQuyen;
                nv.ChucVu = entity.ChucVu;
                nv.Luong = entity.Luong;
                db.NhanViens.Add(nv);
                db.SaveChanges();
                
                TempData["add_success"] = "Thêm tài khoản thành công";
                TempData["alert"] = "alert-success";
                return Redirect("/nhanvien/index");

            }
            catch
            {
                TempData["add_success"] = "Thêm tài khoản KHÔNG thành công";
                TempData["alert"] = "alert-danger";
                return Redirect("/nhanvien/index");
            }
        }


        [HttpPost]
        public ActionResult frmEdit(NhanVienDTO entity)
        {
            try
            {
                var nv = db.NhanViens.Find(entity.IDTK);
                nv.Ten = entity.Ten;
                nv.CMND = entity.CMND;
                nv.DiaChi = entity.DiaChi;
                nv.SDT = entity.SDT;
                nv.ChucVu = entity.ChucVu;
                nv.Luong = entity.Luong;

                var tk = db.NhanViens.Find(entity.IDTK);
                tk.IDPhanQuyen = entity.IDPhanQuyen;

                db.SaveChanges();
                TempData["add_success"] = "Cập nhật tài khoản thành công";
                return Redirect("/nhanvien/index");
            }
            catch
            {
                TempData["add_success"] = "Cập nhật tài khoản KHÔNG thành công";
                return Redirect("/nhanvien/index");
            }
        }

        public JsonResult GetByID(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.NhanViens.Find(ID);
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public FileResult Export(int thang = 0)
        {
            var lstLuong = new List<LuongDTO>();

            if (thang != 0)
            {
                foreach (var item in db.NhanViens.Where(x => x.TenDN != "admin").ToList())
                {
                    var luong = new LuongDTO();
                    luong.TenNhanVien = item.Ten;
                    luong.LuongCung = item.Luong;

                    var model = (from detail in db.ChiTietPSCs
                                 join psc in db.PhieuSuaChuas on detail.IDPSC equals psc.IDPSC
                                 join tc in db.TienCongs on detail.IDTienCong equals tc.IDTienCong
                                 where psc.IDTK == item.IDTK && psc.NgaySuaChua.Month == thang
                                 select new PhieuSuaChuaDTO
                                 {
                                     Luong = tc.TienCong1
                                 }).Sum(x => x.Luong);
                    if (model != null)
                        luong.Thuong = (model * 20) / 100;
                    else
                        luong.Thuong = 0;
                    luong.TongLuong = luong.LuongCung + luong.Thuong;
                    lstLuong.Add(luong);

                }
            }
            else
            {
                foreach (var item in db.NhanViens.Where(x => x.TenDN != "admin").ToList())
                {
                    var luong = new LuongDTO();

                    luong.TenNhanVien = item.Ten;
                    luong.LuongCung = item.Luong;

                    var model = (from detail in db.ChiTietPSCs
                                 join psc in db.PhieuSuaChuas on detail.IDPSC equals psc.IDPSC
                                 join tc in db.TienCongs on detail.IDTienCong equals tc.IDTienCong
                                 where psc.IDTK == item.IDTK && psc.NgaySuaChua.Month == DateTime.Now.Month
                                 select new PhieuSuaChuaDTO()
                                 {
                                     Luong = tc.TienCong1
                                 }).Sum(x => x.Luong);
                    if (model != null)
                        luong.Thuong = (model * 20) / 100;
                    else
                        luong.Thuong = 0;
                    luong.TongLuong = luong.LuongCung + luong.Thuong;
                    lstLuong.Add(luong);

                }
            }



            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("STT"),
                                            new DataColumn("Họ và tên nhân viên"),
                                            new DataColumn("Lương cứng (đ)"),
                                            new DataColumn("Thưởng (đ)"),
                                            new DataColumn("Tổng lương (đ)") });
            dt.Rows.Add("", "Danh sách lương tháng: ", thang != 0 ? thang : DateTime.Now.Month, "", "");
            int dem = 1;
            foreach (var item in lstLuong)
            {
                dt.Rows.Add(dem, item.TenNhanVien, item.LuongCung.Value.ToString("N0"), item.Thuong.Value.ToString("N0"), item.TongLuong.Value.ToString("N0"));
                dem++;
            }

            if (thang == 0)
                thang = DateTime.Now.Month;
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danh-sach-luong-thang-" + thang + ".xlsx");
                }
            }
        }
    }
}