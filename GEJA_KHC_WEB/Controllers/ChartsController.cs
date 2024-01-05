using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using GEJA_KHC_WEB.ViewModels;
using GEJA_KHC_WEB.Models;

namespace GEJA_KHC_WEB.Controllers
{
    public class ChartsController : Controller
    {
        private List<SelectListItem> chartList = new List<SelectListItem>() 
        {
            new SelectListItem { Text = "--ቻርት ምረጥ--", Value = "0" },
            new SelectListItem { Text = "የአባላት ብዛት በፆታ", Value = "1" },
            new SelectListItem { Text = "የአባላት ብዛት በምድብ", Value = "2" },
            new SelectListItem { Text = "የአባላት ብዛት በትምህርት ደረጃ", Value = "3" },
            new SelectListItem { Text = "የአባላት ብዛት በጋብቻ ሁኔታ", Value = "4" },
            new SelectListItem { Text = "የአባላት ብዛት በአባልነት መንገድ", Value = "5" },
            new SelectListItem { Text = "የአባላት ብዛት በአባልነት ዘመን", Value = "6" },
            new SelectListItem { Text = "የአባላት ብዛት በክፍለ ከተማ", Value = "7" }
        };
        
        private List<SelectListItem> chartTypeList = new List<SelectListItem>() 
        {
            new SelectListItem { Text = "'Bar' ቻርት", Value = "bar" },
            new SelectListItem { Text = "'Line' ቻርት", Value = "line" },
            new SelectListItem { Text = "'Pie' ቻርት", Value = "pie" }
        };
        
        // GET: Charts
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                int graphIndex = (int)id;
                if (graphIndex >= 0 && graphIndex <= chartList.Count)
                {
                    //chartList[graphIndex].Selected = true;
                    chartList.Where(g => g.Value == id.ToString()).FirstOrDefault().Selected = true;
                }
            }

            ViewBag.ChartList = chartList;
            ViewBag.ChartTypeList = chartTypeList;
            
            return View();
        }
        

        public JsonResult _NoOfMembersVsGroupChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<Group> groups = DbViewModel.GetGroupList();
            
            var memberRecord = from mem in members
                               join grp in groups on mem.GroupId equals grp.GroupId into table1
                               from grp in table1.ToList()
                               select new
                               {
                                   members = mem,
                                   groups = grp
                               };

            var noOfMembersByGroup = (from c in memberRecord.OrderBy(m => m.groups.GroupId)
                                      group c by new { c.groups } into g
                                      select new
                                      {
                                          GroupName = g.Key.groups.GroupName,
                                          MemberCount = g.Count()
                                      }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersByGroup);

            List<object> iData = new List<object>();
            
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsEducationLevelChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<EducationAndJobInfo> educJobs = DbViewModel.GetEducationAndJobInfoList();
            List<EducationLevel> educLevel = DbViewModel.GetEducationLevelList();

            var memberRecord = from mem in members
                               join ej in educJobs on mem.MemberId equals ej.MemberId into table1
                               from ej in table1.ToList()
                               join edu in educLevel on ej.EducationLevelId equals edu.EducationLevelId into table2
                               from edu in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   educationlevels = edu
                               };

            var noOfMembersVsEducationLevel = (from c in memberRecord.OrderBy(m => m.educationlevels.EducationLevelId)
                                           group c by new { c.educationlevels } into g
                                           select new
                                           {
                                               educationlevel = g.Key.educationlevels.EducationLevelName,
                                               MemberCount = g.Count()
                                           }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersVsEducationLevel);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsMaritalStatusChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<FamilyInfo> family = DbViewModel.GetFamilyInfoList();
            List<MaritalStatus> status = DbViewModel.GetMaritalStatusList();

            var memberRecord = from mem in members
                               join st in family on mem.MemberId equals st.MemberId into table1
                               from st in table1.ToList()
                               join edu in status on st.MaritalStatusId equals edu.MaritalStatusId into table2
                               from edu in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   maritalstatus = edu
                               };

            var noOfMembersVsMaritalStatusChart = (from c in memberRecord.OrderBy(m => m.maritalstatus.MaritalStatusId)
                                                   group c by new { c.maritalstatus } into g
                                                   select new
                                                   {
                                                       MaritalStatus = g.Key.maritalstatus.MaritalStatusName,
                                                       MemberCount = g.Count()
                                                   }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersVsMaritalStatusChart);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsMembershipMeansChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<MembershipMeans> means = DbViewModel.GetMembershipMeansList();

            var memberRecord = from mem in members
                               join mns in means on mem.MembershipMeansId equals mns.MembershipMeansId into table1
                               from mns in table1.ToList()
                               select new
                               {
                                   members = mem,
                                   membershipmeans = mns
                               };

            var noOfMembersVsMembershipMeansChart = (from c in memberRecord.OrderBy(m => m.membershipmeans.MembershipMeansId)
                                                     group c by new { c.membershipmeans } into g
                                                     select new
                                                     {
                                                         MembershipMeans = g.Key.membershipmeans.MembershipMeansName,
                                                         MemberCount = g.Count()
                                                     }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersVsMembershipMeansChart);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsMembershipYearChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            var years = (from mem in members
                              orderby mem.MembershipYear descending
                              select new
                              {
                                  mem.MembershipYear
                              }).Distinct().OrderByDescending(s => s.MembershipYear).ToList();

            var memberRecord = from mem in members
                               join yrs in years on mem.MembershipYear equals yrs.MembershipYear into table1
                               from yrs in table1.ToList()
                               select new
                               {
                                   members = mem,
                                   membershipyears = yrs
                               };

            var noOfMembersVsMembershipYearChart = (from c in memberRecord.OrderBy(m => m.membershipyears.MembershipYear)
                                                    group c by new { c.membershipyears } into g
                                                    select new
                                                    {
                                                        MembershipYear = g.Key.membershipyears.MembershipYear,
                                                        MemberCount = g.Count()
                                                    }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersVsMembershipYearChart);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsGenderChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<Gender> genders = DbViewModel.GetGenderList();

            var memberRecord = from mem in members
                               join gen in genders on mem.GenderId equals gen.GenderId into table1
                               from gen in table1.ToList()
                               select new
                               {
                                   members = mem,
                                   genders = gen
                               };

            var noOfMembersByGender = (from c in memberRecord.OrderBy(m => m.genders.GenderId)
                                      group c by new { c.genders } into g
                                      select new
                                      {
                                          GenderName = g.Key.genders.GenderName,
                                          MemberCount = g.Count()
                                      }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersByGender);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _NoOfMembersVsSubcityChart()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<AddressInfo> address = DbViewModel.GetAddressInfoList();
            List<Subcity> subcity = DbViewModel.GetSubcityList();

            var memberRecord = from mem in members
                               join add in address on mem.MemberId equals add.MemberId into table1
                               from add in table1.ToList()
                               join sub in subcity on add.SubcityId equals sub.SubcityId into table2
                               from sub in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   subcity = sub
                               };

            var noOfMembersVsSubcityChart = (from c in memberRecord.OrderBy(m => m.subcity.SubcityId)
                                             group c by new { c.subcity } into g
                                             select new
                                             {
                                                 Subcity = g.Key.subcity.SubcityName,
                                                 MemberCount = g.Count()
                                             }).ToList();

            DataTable dt = ChartViewModel.ListToDataTable(noOfMembersVsSubcityChart);

            List<object> iData = new List<object>();

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }
    }
}