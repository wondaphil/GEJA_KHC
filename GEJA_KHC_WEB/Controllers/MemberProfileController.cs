using GEJA_KHC_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using System.Reflection;

namespace GEJA_KHC_WEB.Controllers
{
    public class MemberProfileController : Controller
    {
        // GET: MemberProfile
        public ActionResult Index(string id = "")
        {
            Member member = DbViewModel.GetMember(id);
            MemberPhoto photo = DbViewModel.GetMemberPhoto(id);
            AddressInfo address = DbViewModel.GetAddressInfo(id);
            FamilyInfo family = DbViewModel.GetFamilyInfo(id);
            EducationAndJobInfo educJob = DbViewModel.GetEducationAndJobInfo(id);
            List<MemberService> services = DbViewModel.GetMemberServiceList(id).OrderBy(s => s.ServiceTypeId).ToList();

            var memberVm = new MemberViewModel()
            {
                MemberInfo = member,
                MemberPhoto = photo,
                AddressInfo = address,
                FamilyInfo = family,
                EducationAndJobInfo = educJob,
                MemberService = services
            };

            var GroupList = DbViewModel.GetGroupsDDL(); // For a populated groups dropdownlist

            //Add the first unselected item
            GroupList.Insert(0, new DropDownViewModel { Text = "--ምድብ ይምረጡ--", Value = "0" });
            ViewBag.Groups = GroupList;

            if (id == "" || member == null)
            {
                ViewBag.Members = new List<SelectListItem>(); // For an empty members dropdownlist

                return View();
            }
            else
            {
                // For a dropdownlist that has a list of members in the current group and the current member selected
                ViewBag.Members = DbViewModel.GetMembersDDL((int)member.GroupId);
                TempData["MemberId"] = id;

                // For a dropdownlist that has a list of all groups and the current group selected
                ViewBag.Groups = DbViewModel.GetGroupsDDL();
                ViewBag.GroupId = member.GroupId;

                // For displaying group and member names on reports
                ViewBag.GroupName = DbViewModel.GetGroup(member.GroupId).GroupName;
                ViewBag.MemberName = member.FullName;

                TempData["Group"] = (member == null) ? "" : DbViewModel.GetGroup(member.GroupId).GroupName;
                TempData["Gender"] = (member == null || member.GenderId == null) ? "" : DbViewModel.GetGender((int)member.GenderId).GenderName;
                TempData["BirthMonth"] = (member == null || member.BirthMonthId == null) ? "" : DbViewModel.GetMonth((int)member.BirthMonthId).MonthName;
                TempData["MembershipMonth"] = (member == null || member.MembershipMonthId == null) ? "" : DbViewModel.GetMonth((int)member.MembershipMonthId).MonthName;
                TempData["MembershipMeans"] = (member == null || member.MembershipMeansId == null) ? "" : DbViewModel.GetMembershipMeans((int)member.MembershipMeansId).MembershipMeansName;
                TempData["Subcity"] = (address == null || address.SubcityId == null) ? "" : DbViewModel.GetSubcity((int)address.SubcityId).SubcityName;
                TempData["HouseOwnershipType"] = (address == null || address.HouseOwnershipId == null) ? "" : DbViewModel.GetHouseOwnershipType((int)address.HouseOwnershipId).HouseOwnershipTypeName;
                TempData["MaritalStatus"] = (family == null || family.MaritalStatusId == 0) ? "" : DbViewModel.GetMaritalStatus((int)family.MaritalStatusId).MaritalStatusName;
                TempData["EducationLevel"] = (educJob == null || educJob.EducationLevelId == null) ? "" : DbViewModel.GetEducationLevel((int)educJob.EducationLevelId).EducationLevelName;
                TempData["FieldOfStudy"] = (educJob == null || educJob.FieldOfStudyId == null) ? "" : DbViewModel.GetFieldOfStudy((int)educJob.FieldOfStudyId).FieldOfStudyName;
                TempData["Job"] = (educJob == null || educJob.JobId == null) ? "" : DbViewModel.GetJob((int)educJob.JobId).JobName;
                TempData["JobType"] = (educJob == null || educJob.JobTypeId == null) ? "" : DbViewModel.GetJobType((int)educJob.JobTypeId).JobTypeName;
                
                return View(memberVm);
            }
        }

        public ActionResult _GetMemberProfile(string id)
        {
            Member member = DbViewModel.GetMember(id);
            MemberPhoto photo = DbViewModel.GetMemberPhoto(id);
            AddressInfo address = DbViewModel.GetAddressInfo(id);
            FamilyInfo family = DbViewModel.GetFamilyInfo(id);
            EducationAndJobInfo educJob = DbViewModel.GetEducationAndJobInfo(id);
            List<MemberService> services = DbViewModel.GetMemberServiceList(id).OrderBy(s => s.ServiceTypeId).ToList();

            var memberVm = new MemberViewModel()
            {
                MemberInfo = member,
                MemberPhoto = photo,
                AddressInfo = address,
                FamilyInfo = family,
                EducationAndJobInfo = educJob,
                MemberService = services
            };

            TempData["Group"] = (member == null) ? "" : DbViewModel.GetGroup(member.GroupId).GroupName;
            TempData["Gender"] = (member == null || member.GenderId == null) ? "" : DbViewModel.GetGender((int)member.GenderId).GenderName;
            TempData["BirthMonth"] = (member == null || member.BirthMonthId == null) ? "" : DbViewModel.GetMonth((int)member.BirthMonthId).MonthName;
            TempData["MembershipMonth"] = (member == null || member.MembershipMonthId == null) ? "" : DbViewModel.GetMonth((int)member.MembershipMonthId).MonthName;
            TempData["MembershipMeans"] = (member == null || member.MembershipMeansId == null) ? "" : DbViewModel.GetMembershipMeans((int)member.MembershipMeansId).MembershipMeansName;
            TempData["Subcity"] = (address == null || address.SubcityId == null) ? "" : DbViewModel.GetSubcity((int)address.SubcityId).SubcityName;
            TempData["HouseOwnershipType"] = (address == null || address.HouseOwnershipId == null) ? "" : DbViewModel.GetHouseOwnershipType((int)address.HouseOwnershipId).HouseOwnershipTypeName;
            TempData["MaritalStatus"] = (family == null || family.MaritalStatusId == 0) ? "" : DbViewModel.GetMaritalStatus((int)family.MaritalStatusId).MaritalStatusName;
            TempData["EducationLevel"] = (educJob == null || educJob.EducationLevelId == null) ? "" : DbViewModel.GetEducationLevel((int)educJob.EducationLevelId).EducationLevelName;
            TempData["FieldOfStudy"] = (educJob == null || educJob.FieldOfStudyId == null) ? "" : DbViewModel.GetFieldOfStudy((int)educJob.FieldOfStudyId).FieldOfStudyName;
            TempData["Job"] = (educJob == null || educJob.JobId == null) ? "" : DbViewModel.GetJob((int)educJob.JobId).JobName;
            TempData["JobType"] = (educJob == null || educJob.JobTypeId == null) ? "" : DbViewModel.GetJobType((int)educJob.JobTypeId).JobTypeName;

            return PartialView(memberVm);
        }

        public ActionResult MemberDetails(string id)
        {
            Member member = DbViewModel.GetMember(id);

            TempData["Group"] = (member == null) ? "" : DbViewModel.GetGroup(member.GroupId).GroupName;
            TempData["Gender"] = (member == null || member.GenderId == null) ? "" : DbViewModel.GetGender((int)member.GenderId).GenderName;
            TempData["BirthMonth"] = (member == null || member.BirthMonthId == null) ? "" : DbViewModel.GetMonth((int)member.BirthMonthId).MonthName;
            TempData["MembershipMonth"] = (member == null || member.MembershipMonthId == null) ? "" : DbViewModel.GetMonth((int)member.MembershipMonthId).MonthName;
            TempData["MembershipMeans"] = (member == null || member.MembershipMeansId == null) ? "" : DbViewModel.GetMembershipMeans((int)member.MembershipMeansId).MembershipMeansName;
            
            if (member == null)
            {
                return PartialView("NoData");
            }

            return PartialView(member);
        }

        public ActionResult AddressDetails(string id)
        {
            AddressInfo address = DbViewModel.GetAddressInfo(id);
            
            TempData["Subcity"] = (address == null || address.SubcityId == null) ? "" : DbViewModel.GetSubcity((int)address.SubcityId).SubcityName;
            TempData["HouseOwnershipType"] = (address == null || address.HouseOwnershipId == null) ? "" : DbViewModel.GetHouseOwnershipType((int)address.HouseOwnershipId).HouseOwnershipTypeName;
            
            if (address == null)
            {
                return PartialView("NoData");
            }

            return PartialView(address);
        }

        public ActionResult FamilyDetails(string id)
        {
            FamilyInfo family = DbViewModel.GetFamilyInfo(id);
            
            TempData["MaritalStatus"] = (family == null || family.MaritalStatusId == 0) ? "" : DbViewModel.GetMaritalStatus((int)family.MaritalStatusId).MaritalStatusName;
            
            if (family == null)
            {
                return PartialView("NoData");
            }

            return PartialView(family);
        }

        public ActionResult EducationAndJobDetails(string id)
        {
            EducationAndJobInfo educJob = DbViewModel.GetEducationAndJobInfo(id);
            
            TempData["EducationLevel"] = (educJob == null || educJob.EducationLevelId == null) ? "" : DbViewModel.GetEducationLevel((int)educJob.EducationLevelId).EducationLevelName;
            TempData["FieldOfStudy"] = (educJob == null || educJob.FieldOfStudyId == null) ? "" : DbViewModel.GetFieldOfStudy((int)educJob.FieldOfStudyId).FieldOfStudyName;
            TempData["Job"] = (educJob == null || educJob.JobId == null) ? "" : DbViewModel.GetJob((int)educJob.JobId).JobName;
            TempData["JobType"] = (educJob == null || educJob.JobTypeId == null) ? "" : DbViewModel.GetJobType((int)educJob.JobTypeId).JobTypeName;

            if (educJob == null)
            {
                return PartialView("NoData");
            }

            return PartialView(educJob);
        }
        public ActionResult ServiceDetails(string id)
        {
            List<MemberService> services = DbViewModel.GetMemberServiceList(id).OrderBy(s => s.ServiceTypeId).ToList();
            
            if (services == null)
            {
                return PartialView("NoData");
            }

            return PartialView(services);
        }

        // GET: MemberList
        public ActionResult MemberList()
        {
            var GroupList = DbViewModel.GetGroupsDDL(); // For a populated groups dropdownlist

            //Add the first unselected item
            GroupList.Insert(0, new DropDownViewModel { Text = "--ምድብ ይምረጡ--", Value = "-1" });

            GroupList.Insert(1, new DropDownViewModel { Text = "በሁሉም ምድቦች", Value = "0" });
            ViewBag.Groups = GroupList;

            return View();
        }

        public ActionResult _GetMemberListByGroup(int groupid /* drop down value */)
        {
            List<Member> members = DbViewModel.GetMemberList(groupid);

            ViewBag.GroupName = DbViewModel.GetGroup(groupid).GroupName;

            return PartialView(members);
        }

        public ActionResult _GetMemberListAll()
        {
            List<Member> members = DbViewModel.GetAllMembers();

            return PartialView(members);
        }


    }
}