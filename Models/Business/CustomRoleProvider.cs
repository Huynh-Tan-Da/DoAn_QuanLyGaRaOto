using QuanLyGaraOto.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGaraOto.Models.Business
{
    public class CustomRoleProvider : AuthorizeAttribute
    {
        AutoGaraEntities db = new AutoGaraEntities();

        public string RoleName { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var nv = HttpContext.Current.Session["nhanvien"] as NhanVien;
            try
            {
                var role = db.NhanViens.Find(nv.IDTK);
                if (role.PhanQuyen.ManHinhLoad == RoleName)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "/Views/Shared/Error.cshtml"
            };
        }
    }
}