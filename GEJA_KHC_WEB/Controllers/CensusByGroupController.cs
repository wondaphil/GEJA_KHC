using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GEJA_KHC_WEB.Controllers
{
    public class CensusByGroupController : Controller
    {
        // GET: CensusByGroup
        public ActionResult Index()
        {
            List<SelectListItem> countReportList = new List<SelectListItem>();

            //Add items
            countReportList.Insert(0, new SelectListItem { Text = "--ሪፖርት ምረጥ--", Value = "0" });
            countReportList.Insert(1, new SelectListItem { Text = "የአባላት ብዛት በአባልነት መንገድ በየምድቡ", Value = "1" });
            countReportList.Insert(2, new SelectListItem { Text = "የአባላት ብዛት በፆታ በየምድቡ", Value = "2" });
            countReportList.Insert(3, new SelectListItem { Text = "የአባላት ብዛት በጋብቻ ሁኔታ በየምድቡ", Value = "3" });
            countReportList.Insert(4, new SelectListItem { Text = "የአባላት ብዛት በትምህርት ደረጃ በየምድቡ", Value = "4" });
            countReportList.Insert(5, new SelectListItem { Text = "የአባላት ብዛት በክፍለ ከተማ በየምድቡ", Value = "5" });

            ViewBag.CountReports = countReportList;

            return View();
        }

        public ActionResult _GetCountByEducationLevelByGroup()
        {
            List<EducationLevelByGroup> eduByGroup = DbViewModel.GetEducationLevelByGroup();

            return View(eduByGroup);
        }

        public ActionResult _GetCountByMaritalStatusByGroup()
        {
            List<MaritalStatusByGroup> status = DbViewModel.GetMaritalStatusByGroup();

            return View(status);
        }

        public ActionResult _GetCountBySubcityByGroup()
        {
            List<SubcityByGroup> subcity = DbViewModel.GetSubcityByGroup();

            return View(subcity);
        }

        public ActionResult _GetCountByMembershipMeansByGroup()
        {
            List<MembershipMeansByGroup> means = DbViewModel.GetMembershipMeansByGroup();

            return View(means);
        }

        public ActionResult _GetCountByGenderByGroup()
        {
            List<GenderByGroup> means = DbViewModel.GetGenderByGroup();

            return View(means);
        }
    }
}