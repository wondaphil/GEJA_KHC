using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace GEJA_KHC_WEB.Controllers
{
    public class CensusAllController : Controller
    {
        // GET: CensusAll
        public ActionResult Index()
        {
            List<SelectListItem> countReportList = new List<SelectListItem>();

            //Add items
            countReportList.Insert(0, new SelectListItem { Text = "--ሪፖርት ምረጥ--", Value = "0" });
            countReportList.Insert(1, new SelectListItem { Text = "የአባላት ብዛት በምድብ", Value = "1" });
            countReportList.Insert(2, new SelectListItem { Text = "የአባላት ብዛት በአባልነት መንገድ", Value = "2" });
            countReportList.Insert(3, new SelectListItem { Text = "የአባላት ብዛት በአባልነት ዘመን", Value = "3" });
            countReportList.Insert(4, new SelectListItem { Text = "የአባላት ብዛት በአገልግሎት ዘርፍ", Value = "4" });
            countReportList.Insert(5, new SelectListItem { Text = "የአባላት ብዛት በፆታ", Value = "5" });
            countReportList.Insert(6, new SelectListItem { Text = "የአባላት ብዛት በጋብቻ ሁኔታ", Value = "6" });
            countReportList.Insert(7, new SelectListItem { Text = "የአባላት ብዛት በትምህርት ደረጃ", Value = "7" });
            countReportList.Insert(8, new SelectListItem { Text = "የአባላት ብዛት በትምህርት መስክ", Value = "8" });
            countReportList.Insert(9, new SelectListItem { Text = "የአባላት ብዛት በሥራ ዘርፍ", Value = "9" });
            countReportList.Insert(10, new SelectListItem { Text = "የአባላት ብዛት በክፍለ ከተማ", Value = "10" });

            ViewBag.CountReports = countReportList;

            return View();
        }

        public ActionResult _GetMembersCountByGroup()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<Group> groups = DbViewModel.GetGroupList();

            // Get member count by group
            var memberCount = from mem in members
                              join grp in groups on mem.GroupId equals grp.GroupId into table1
                              from grp in table1.ToList()
                              group grp by new { grp } into g
                              select new CountByGroupViewModel
                              {
                                  Groups = g.Key.grp,
                                  MemberCount = g.Count()
                              };

            return PartialView(memberCount.OrderBy(m => m.Groups.GroupId));
        }

        public ActionResult _GetMembersCountByMembershipMeans()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<MembershipMeans> means = DbViewModel.GetMembershipMeansList();

            // Get member count by means
            var memberCount = from mem in members
                              join mns in means on mem.MembershipMeansId equals mns.MembershipMeansId into table1
                              from mns in table1.ToList()
                              group mns by new { mns } into g
                              select new CountByMembershipMeansViewModel
                              {
                                  MembershipMeans = g.Key.mns,
                                  MemberCount = g.Count()
                              };

            return PartialView(memberCount.OrderBy(m => m.MembershipMeans.MembershipMeansId));
        }

        public ActionResult _GetMembersCountByMembershipYear()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            var year = (from mem in DbViewModel.GetAllMembers()
                              select new
                              {
                                 MembershipYear = mem.MembershipYear
                              }).Distinct().ToList();

            // Get member count by membership year
            var memberCount = from mem in members
                              join yr in year on mem.MembershipYear equals yr.MembershipYear into table1
                              from yr in table1.ToList()
                              group yr by new { yr } into g
                              select new CountByMembershipYearViewModel
                              {
                                  MembershipYear = g.Key.yr.MembershipYear,
                                  MemberCount = g.Count()
                              };

            return PartialView(memberCount.OrderBy(m => m.MembershipYear));
        }

        public ActionResult _GetMembersCountByGender()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<Gender> gender = DbViewModel.GetGenderList();

            // Get member count by gender
            var memberCount = from mem in members
                              join gen in gender on mem.GenderId equals gen.GenderId into table1
                              from gen in table1.ToList()
                              group gen by new { gen } into g
                              select new CountByGenderViewModel
                              {
                                  Gender = g.Key.gen.GenderName,
                                  MemberCount = g.Count()
                              };

            return PartialView(memberCount);
        }

        public ActionResult _GetMembersCountByMaritalStatus()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<FamilyInfo> family = DbViewModel.GetFamilyInfoList();
            List<MaritalStatus> status = DbViewModel.GetMaritalStatusList();

            var memberRecord = from mem in members
                               join sta in family on mem.MemberId equals sta.MemberId into table1
                               from sta in table1.ToList()
                               join edu in status on sta.MaritalStatusId equals edu.MaritalStatusId into table2
                               from edu in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   status = edu
                               };

            var noofmembersbyeducationlevel = (from c in memberRecord.OrderBy(m => m.status.MaritalStatusId)
                                               group c by new { c.status } into g
                                               select new CountByMaritalStatusViewModel
                                               {
                                                   MaritalStatus = g.Key.status,
                                                   MemberCount = g.Count()
                                               }).ToList();

            return PartialView(noofmembersbyeducationlevel);
        }

        public ActionResult _GetMembersCountBySubcity()
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
                                   subcities = sub
                               };

            var noofmembersbyeducationlevel = (from c in memberRecord.OrderBy(m => m.subcities.SubcityId)
                                               group c by new { c.subcities } into g
                                               select new CountBySubcityViewModel
                                               {
                                                   Subcities = g.Key.subcities,
                                                   MemberCount = g.Count()
                                               }).ToList();

            return PartialView(noofmembersbyeducationlevel);
        }

        public ActionResult _GetMembersCountByEducationLevel()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<EducationAndJobInfo> eduJob = DbViewModel.GetEducationAndJobInfoList();
            List<EducationLevel> eduLevel = DbViewModel.GetEducationLevelList();

            var memberRecord = from mem in members
                               join inf in eduJob on mem.MemberId equals inf.MemberId into table1
                               from inf in table1.ToList()
                               join edu in eduLevel on inf.EducationLevelId equals edu.EducationLevelId into table2
                               from edu in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   educationlevels = edu
                               };

            var noofmembersbyeducationlevel = (from c in memberRecord.OrderBy(m => m.educationlevels.EducationLevelId)
                                               group c by new { c.educationlevels } into g
                                               select new CountByEducationLevelViewModel
                                               {
                                                   EducationLevels = g.Key.educationlevels,
                                                   MemberCount = g.Count()
                                               }).ToList();

            return PartialView(noofmembersbyeducationlevel);
        }

        public ActionResult _GetMembersCountByJob()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<EducationAndJobInfo> eduJob = DbViewModel.GetEducationAndJobInfoList();
            List<Job> job = DbViewModel.GetJobList();

            var memberRecord = from mem in members
                               join inf in eduJob on mem.MemberId equals inf.MemberId into table1
                               from inf in table1.ToList()
                               join jb in job on inf.JobId equals jb.JobId into table2
                               from jb in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   jobs = jb
                               };

            var noofmembersbyjob = (from c in memberRecord.OrderBy(m => m.jobs.JobId)
                                               group c by new { c.jobs } into g
                                               select new CountByJobViewModel
                                               {
                                                   Jobs = g.Key.jobs,
                                                   MemberCount = g.Count()
                                               }).ToList();

            return PartialView(noofmembersbyjob);
        }

        public ActionResult _GetMembersCountByFieldOfStudy()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<EducationAndJobInfo> eduFieldOfStudy = DbViewModel.GetEducationAndJobInfoList();
            List<FieldOfStudy> field = DbViewModel.GetFieldOfStudyList();

            var memberRecord = from mem in members
                               join inf in eduFieldOfStudy on mem.MemberId equals inf.MemberId into table1
                               from inf in table1.ToList()
                               join fld in field on inf.FieldOfStudyId equals fld.FieldOfStudyId into table2
                               from fld in table2.ToList()
                               select new
                               {
                                   members = mem,
                                   fields = fld
                               };

            var noofmembersbyjob = (from c in memberRecord.OrderBy(m => m.fields.FieldOfStudyId)
                                    group c by new { c.fields } into g
                                    select new CountByFieldOfStudyViewModel
                                    {
                                        FieldOfStudies = g.Key.fields,
                                        MemberCount = g.Count()
                                    }).ToList();

            return PartialView(noofmembersbyjob);
        }

        public ActionResult _GetMembersCountByServiceCurrent()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<MemberService> current = DbViewModel.GetAllMemberServices().Where(s => s.ServiceTypeId == 2).ToList(); // ServiceTypeId is 2 for current service (አሁን እያገለገሉ ያሉበት)
            List<Service> services = DbViewModel.GetServiceList();

            var currentServices = (from mem in members
                                   join cur in current on mem.MemberId equals cur.MemberId into table1
                                   from cur in table1.ToList()
                                   join serv in services on cur.ServiceId equals serv.ServiceId into table2
                                   from serv in table2.ToList()
                                   select new
                                   {
                                       members = mem,
                                       services = serv
                                   });

            var noofmembersbyserviceList = (from c in currentServices.OrderBy(s => s.services.ServiceId)
                                            group c by new { c.services } into g
                                            select new CountByServiceViewModel
                                            {
                                                Services = g.Key.services,
                                                MemberCount = g.Count()
                                            }).ToList();

            return PartialView(noofmembersbyserviceList);
        }
        public ActionResult _GetMembersCountByServicePrevious()
        {
            List<Member> members = DbViewModel.GetAllMembers();
            List<MemberService> previous = DbViewModel.GetAllMemberServices().Where(s => s.ServiceTypeId == 1).ToList(); // ServiceTypeId is 1 for previous service (ከዚህ በፊት ያገለገሉበት)
            List<Service> services = DbViewModel.GetServiceList();

            var currentServices = (from mem in members
                                   join prev in previous on mem.MemberId equals prev.MemberId into table1
                                   from prev in table1.ToList()
                                   join serv in services on prev.ServiceId equals serv.ServiceId into table2
                                   from serv in table2.ToList()
                                   select new
                                   {
                                       members = mem,
                                       services = serv
                                   });

            var noofmembersbyserviceList = (from c in currentServices.OrderBy(s => s.services.ServiceId)
                                            group c by new { c.services } into g
                                            select new CountByServiceViewModel
                                            {
                                                Services = g.Key.services,
                                                MemberCount = g.Count()
                                            }).ToList();

            return PartialView(noofmembersbyserviceList);
        }
    }
}