using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GEJA_KHC_WEB.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public string GetMemberIdFromName(string fullname)
        {
            Member member = DbViewModel.GetAllMembers().Where(m => m.FullName == fullname).FirstOrDefault();

            if (member == null)
            {
                return "[{\"MemberId\": \"\"}]";
            }

            // A json string of the form [{ "MemberId": "005786" }]
            return "[{\"MemberId\": \"" + member.MemberId + "\"}]";
        }
        
        public ActionResult _FindMember(string fullname)
        {
            List<Member> member = DbViewModel.GetAllMembers().Where(m => m.FullName == fullname).ToList();

            if (member.Count == 0) // at least one member is found
            {
                ViewBag.Success = false;
                
                return PartialView();
            }

            ViewBag.Success = true;
            string memberid = member[0].MemberId;
            ViewBag.NoAddressInfo = (DbViewModel.GetAddressInfo(memberid) == null);
            ViewBag.NoFamilyInfo = (DbViewModel.GetFamilyInfo(memberid) == null);
            ViewBag.NoEducationAndJobInfo = (DbViewModel.GetEducationAndJobInfo(memberid) == null);
            ViewBag.NoServices = (DbViewModel.GetMemberServiceList(memberid).Count == 0);
                
            return PartialView(member);
        }
        
        
        // Get a list of member  starting with term (for AutoComplete)
        public ActionResult GetMemberListContaining(string term)
        {
            var result = (from m in DbViewModel.GetAllMembers()
                         where m.MemberId.Contains(term) || m.FullName.Contains(term)
                         select m.FullName).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}