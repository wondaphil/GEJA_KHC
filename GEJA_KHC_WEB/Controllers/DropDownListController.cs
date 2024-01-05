using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;

namespace GEJA_KHC_WEB.Controllers
{
    public class DropDownListController : Controller
    {
        // GET: DropDownList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMembersDDL(int groupid)
        {
            var members = DbViewModel.GetMembersDDL(groupid);

            return this.Json(new SelectList(members.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}