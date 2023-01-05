using QuanLyGaraOto.Models.Business;
using QuanLyGaraOto.Models.DTO;
using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Controllers
{
    public class NhapKhoController : Controller
    {
        // GET: NhapKho
        private AutoGaraEntities db = new AutoGaraEntities();
        public ActionResult Index(int? month)
        {
            var model = db.PhieuNhaps.OrderByDescending(x => x.NgayNhap).ToList();
            var query = from ct in db.CTPhieuNhaps
                        join pn in db.PhieuNhaps on ct.IDPN equals pn.IDPN
                        where pn.NgayNhap.Month == (month!= null ? month : DateTime.Now.Month)
                        select ct;
            ViewBag.lstCtPhieuNhap = query.OrderByDescending(x => x.SoLuong).ToList();
            ViewBag.Month = (month != null ? month : DateTime.Now.Month);
            return View(model);
        }

        public ActionResult Add()
        {
            ViewBag.lstVTPT = db.VTPTs.OrderBy(x => x.Ten).ToList();
            ViewBag.lstNhaCC = db.NhaCungCaps.OrderBy(x => x.Ten).ToList();
            return View();
        }


        [HttpPost]
        public ActionResult AddPhieuNhap(PhieuNhap entity)
        {
            var res = new NhapKhoBusiness().Add_PN(entity);
            if (res)
            {
                var nhapkho = (List<NhapKhoDTO>)Session["add_nhapkho"];
                foreach (var item in nhapkho)
                {
                    var detail = new CTPhieuNhap();
                    detail.IDPN = db.PhieuNhaps.Max(x => x.IDPN);
                    detail.IDVTPT = item.VTPT.IDVTPT;
                    detail.SoLuong = item.Quantity;
                    detail.DonGiaNhap = item.VTPT.DonGia;

                    detail.TonKho = db.VTPTs.Find(detail.IDVTPT).SoLuongTon;//Lưu số lượng đang tồn kho hiện tại
                    new NhapKhoBusiness().Add_CTPN(detail);

                    //Cộng tồn kho
                    new NhapKhoBusiness().Add_Quantity(item.VTPT.IDVTPT, item.Quantity);
                }
                Session["add_nhapkho"] = null;
                TempData["add_success"] = "Nhập kho thành công.";
                return Redirect("/nhapkho/index");
            }
            else
            {
                return Redirect("/nhapkho/add");
            }
        }


        public JsonResult addVTPT(int IDVTPT, int quantity)
        {
            //var product = new NhapKhoBusiness().searchVTPT(product_name);
            var product = db.VTPTs.Find(IDVTPT);
            var vtpt = Session["add_nhapkho"];
            if (vtpt != null)
            {
                var list = (List<NhapKhoDTO>)vtpt;
                if (list.Exists(x => x.VTPT.IDVTPT == product.IDVTPT))
                {
                    foreach (var item in list)
                    {
                        if (item.VTPT.IDVTPT == product.IDVTPT)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //Tạo đối tượng mới
                    var item = new NhapKhoDTO();
                    item.VTPT = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else//nếu giỏ hàng trống
            {
                var item = new NhapKhoDTO();
                item.VTPT = product;
                item.Quantity = quantity;
                var list = new List<NhapKhoDTO>();
                list.Add(item);

                Session["add_nhapkho"] = list;
            }
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);

        }


        //Xóa từng sản phẩm
        public JsonResult Delete(long ID)
        {
            var cartSec = (List<NhapKhoDTO>)Session["add_nhapkho"];
            cartSec.RemoveAll(x => x.VTPT.IDVTPT == ID);
            Session["add_nhapkho"] = cartSec;
            return Json(new
            {
                status = true
            });
        }

        //Sửa số lượng sp trong giỏ hàng
        public JsonResult Edit(long ID, int Quantity)
        {
            var productSec = (List<NhapKhoDTO>)Session["add_nhapkho"];

            foreach (var item in productSec)
            {
                if (item.VTPT.IDVTPT == ID)
                {
                    item.Quantity = Quantity;
                }

            }

            Session["add_nhapkho"] = productSec;
            return Json(new
            {
                status = true
            });
        }


        //Chi tiết nhập kho
        public ActionResult CTPhieuNhap(long ID)
        {
            var model = db.CTPhieuNhaps.Where(x => x.IDPN == ID).ToList();
            ViewBag.PhieuNhap = db.PhieuNhaps.Find(ID);
            return View(model);
        }

        public JsonResult ListName(string q)
        {
            var query = db.VTPTs.Where(x => x.Ten.Contains(q)).Select(x => x.Ten);
            return Json(new
            {
                data = query,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        //Chi tiết nhập kho
        public ActionResult Detail(int ID)
        {
            ViewBag.lstCT = db.CTPhieuNhaps.Where(x => x.IDPN == ID).ToList();
            ViewBag.PhieuNhap = db.PhieuNhaps.Find(ID);
            return View();
        }
    }
}