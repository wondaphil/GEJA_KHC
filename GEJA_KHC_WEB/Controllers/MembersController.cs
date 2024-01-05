using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace GEJA_KHC.Controllers
{
    //Only logged in user should have access
    [Authorize(Roles = "Admin, Pator, DataEncoder")]
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index(string id = "")
        {
            Member member = DbViewModel.GetMember(id);
            MemberPhoto memberPhoto = DbViewModel.GetMemberPhoto(id);
            AddressInfo addressInfo = DbViewModel.GetAddressInfo(id);
            FamilyInfo familyInfo = DbViewModel.GetFamilyInfo(id);
            EducationAndJobInfo educJobInfo = DbViewModel.GetEducationAndJobInfo(id);
            List<MemberService> serviceInfo = DbViewModel.GetMemberServiceList(id);

            MemberViewModel memberData = new MemberViewModel()
            {
                MemberInfo = member,
                MemberPhoto = memberPhoto,
                AddressInfo = addressInfo,
                FamilyInfo = familyInfo,
                EducationAndJobInfo = educJobInfo,
                MemberService = serviceInfo
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

                //For member info
                TempData["GenderId"] = (member.GenderId == null) ? new SelectList(DbViewModel.GetGenderList(true), "GenderId", "GenderName").ToList() : new SelectList(DbViewModel.GetGenderList(), "GenderId", "GenderName", member.GenderId).ToList();
                TempData["BirthMonthId"] = (member.BirthMonthId == null) ? new SelectList(DbViewModel.GetMonthList(true), "MonthId", "MonthName").ToList() : new SelectList(DbViewModel.GetMonthList(true), "MonthId", "MonthName", member.BirthMonthId).ToList();
                TempData["MembershipMonthId"] = (member.MembershipMonthId == null) ? new SelectList(DbViewModel.GetMonthList(true), "MonthId", "MonthName").ToList() : new SelectList(DbViewModel.GetMonthList(true), "MonthId", "MonthName", member.MembershipMonthId).ToList();
                TempData["GroupId"] = (member == null) ? new SelectList(DbViewModel.GetGroupList(), "GroupId", "GroupName").ToList() : new SelectList(DbViewModel.GetGroupList(), "GroupId", "GroupName", member.GroupId).ToList();
                TempData["MembershipMeansId"] = (member.MembershipMeansId == null) ? new SelectList(DbViewModel.GetMembershipMeansList(true), "MembershipMeansId", "MembershipMeansName").ToList() : new SelectList(DbViewModel.GetMembershipMeansList(), "MembershipMeansId", "MembershipMeansName", member.MembershipMeansId).ToList();
                
                // Date values of Ethiopian month (1 - 30)
                var datelist = GetRangeOfValues(1, 30).Select(c => new SelectListItem
                        { Text = c.ToString(), Value = c.ToString() }).ToList();
                datelist.Insert(0, new SelectListItem { Text = "", Value = "0" });
                TempData["DateList"] = datelist;

                // Year values of (1900 - current year)
                var yearlist = GetRangeOfValues(1900, DateTime.Now.Year - 7).Select(c => new SelectListItem
                                { Text = c.ToString(), Value = c.ToString() }).ToList();
                yearlist.Insert(0, new SelectListItem { Text = "", Value = "0" });
                TempData["YearList"] = yearlist;

                //For address info
                TempData["SubcityId"] = (addressInfo == null) ? new SelectList(DbViewModel.GetSubcityList(true), "SubcityId", "SubcityName").ToList() : new SelectList(DbViewModel.GetSubcityList(), "SubcityId", "SubcityName", addressInfo.SubcityId).ToList();
                TempData["HouseOwnershipTypeId"] = (addressInfo == null) ? new SelectList(DbViewModel.GetHouseOwnershipTypeList(true), "HouseOwnershipTypeId", "HouseOwnershipTypeName").ToList() : new SelectList(DbViewModel.GetHouseOwnershipTypeList(), "HouseOwnershipTypeId", "HouseOwnershipTypeName", addressInfo.HouseOwnershipId).ToList();

                //For family info
                TempData["MaritalStatusId"] = (familyInfo == null) ? new SelectList(DbViewModel.GetMaritalStatusList(true), "MaritalStatusId", "MaritalStatusName").ToList() : new SelectList(DbViewModel.GetMaritalStatusList(), "MaritalStatusId", "MaritalStatusName", familyInfo.MaritalStatusId).ToList();

                //For educ and job info
                TempData["EducationLevelId"] = (educJobInfo == null) ? new SelectList(DbViewModel.GetEducationLevelList(true), "EducationLevelId", "EducationLevelName").ToList() : new SelectList(DbViewModel.GetEducationLevelList(), "EducationLevelId", "EducationLevelName", educJobInfo.EducationLevelId).ToList();
                TempData["FieldOfStudyId"] = (educJobInfo == null) ? new SelectList(DbViewModel.GetFieldOfStudyList(true), "FieldOfStudyId", "FieldOfStudyName").ToList() : new SelectList(DbViewModel.GetFieldOfStudyList(), "FieldOfStudyId", "FieldOfStudyName", educJobInfo.FieldOfStudyId).ToList();
                TempData["JobId"] = (educJobInfo == null) ? new SelectList(DbViewModel.GetJobList(true), "JobId", "JobName").ToList() : new SelectList(DbViewModel.GetJobList(), "JobId", "JobName", educJobInfo.JobId).ToList();
                TempData["JobTypeId"] = (educJobInfo == null) ? new SelectList(DbViewModel.GetJobTypeList(true), "JobTypeId", "JobTypeName").ToList() : new SelectList(DbViewModel.GetJobTypeList(), "JobTypeId", "JobTypeName", educJobInfo.JobTypeId).ToList();

                //For service drop down list
                var serviceTypes = new SelectList(DbViewModel.GetServiceTypeList(true), "ServiceTypeId", "ServiceTypeName").ToList();

                //Add the first unselected item
                serviceTypes.Insert(0, new SelectListItem { Text = "", Value = "0" });
                TempData["ServiceTypeSelectList"] = serviceTypes;

                //For drop down list
                var services = new SelectList(DbViewModel.GetServiceList(), "ServiceId", "ServiceName").ToList();

                //Add the first unselected item
                services.Insert(0, new SelectListItem { Text = "", Value = "0" });
                TempData["ServiceSelectList"] = services;

                // For service json object in jquery
                TempData["ServiceTypeList"] = DbViewModel.GetServiceTypeList().Select(c => new { Id = c.ServiceTypeId, Name = c.ServiceTypeName }).ToList();
                TempData["ServiceList"] = DbViewModel.GetServiceList().Select(c => new { Id = c.ServiceId, Name = c.ServiceName }).ToList();

                return View(memberData);
            }
        }

        public string SuggestedMemberId(int groupid)
        {
            // MemberId is in the format "01-0000" or "02-0000" or "03-0000" or "04-0000" where "01", "02", "03" and "04" are group ids
            // If there is no member in the group, suggest 0001 (e.g. 01-0001 or 04-0001"
            // Otherwise, find the member no. of the lastly added member in the group, extract the last four digits and increment it by 1
            
            // Get the lastly added member Id in the given group
            var memberList = DbViewModel.GetMemberList(groupid).Select(s => s.MemberId).ToList();

            // If no member so far registered on the given group 
            if (memberList.Count == 0)
            {
                // suggest -0001 as member no (because it is the first member for the group)
                var grid = DbViewModel.GetGroupList().Where(g => g.GroupId == groupid).FirstOrDefault().GroupId.ToString();
                string firstMemberId = "0" + grid + "-0001";
                return "[{\"MemberId\": " + "\"" + firstMemberId + "\"}]";
            }

            // The lastly added member
            string lastMemberId = memberList.Max().ToString();

            string lastDigits = lastMemberId.Split('-').Last();

            int indexOfLastDigit = lastMemberId.IndexOf(lastDigits);
            string withoutlastDigits = lastMemberId.Remove(indexOfLastDigit, lastDigits.Length);

            string suggestedMemberId;
            int i = 1;

            // If the suggested member id already exists, keep on incrementing it 
            do
            {
                suggestedMemberId = withoutlastDigits + (Int32.Parse(lastDigits) + i).ToString().PadLeft(4, '0');
                i++;
            } while (memberList.Any(br => br == suggestedMemberId)); // repeat if member id exists

            // A json string of the form [{"MemberId": "01-0455"}]
            return "[{\"MemberId\": " + "\"" + suggestedMemberId + "\"}]";
        }

        //GET
        public ActionResult NewMember()
        {
            var GroupList = DbViewModel.GetGroupsDDL(); // For a populated groups dropdownlist

            //Add the first unselected item
            GroupList.Insert(0, new DropDownViewModel { Text = "--ምድብ ይምረጡ--", Value = "0" });
            ViewBag.Groups = GroupList;

            return View();
        }

        public JsonResult AddNewMember([Bind(Include = "MemberId,FullName,GroupId")] Member member)
        {
            int statusId = DbViewModel.SetMember(member);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        //Only Admin should have access
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteMember()
        {
            return View();
        }

        //Only Admin should have access
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string fullname)
        {
            if (fullname == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = DbViewModel.GetAllMembers().Where(m => m.FullName == fullname).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Success = false;
                ViewBag.NoDetailData = true;
            }
            else
            {
                var memberid = member.MemberId;

                Member memInfo = DbViewModel.GetMember(memberid);
                AddressInfo address = DbViewModel.GetAddressInfo(memberid);
                FamilyInfo family = DbViewModel.GetFamilyInfo(memberid);
                EducationAndJobInfo edujob = DbViewModel.GetEducationAndJobInfo(memberid);
                List<MemberService> services = DbViewModel.GetMemberServiceList(memberid);

                if (address == null || family == null || edujob == null) // member exists but no or incomplete detail data
                    ViewBag.NoDetailData = true;
                else // member exists and has complete inventory data
                    ViewBag.NoDetailData = false;

                ViewBag.Success = true;
            }

            return PartialView(member);
        }

        //Only Ammin should have access
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string memberid)
        {
            //Member member = DbViewModel.GetMember(memberid);
            Geja_KHC_Entities db = new Geja_KHC_Entities();

            Member member = db.Member.Find(memberid);
            DbViewModel.DeleteMember(member.MemberId);
            
            return RedirectToAction("DeleteMember");
        }
        
        //GET
        public ActionResult MemberInfo([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member memberInfo = DbViewModel.GetMember(memberid);
            
            TempData["GenderId"] = DbViewModel.GetGenderList().Select(c => new SelectListItem
                                        { Text = c.GenderName, Value = c.GenderId.ToString() }).ToList();
            TempData["BirthMonthId"] = DbViewModel.GetMonthList(true).Select(c => new SelectListItem
                                        { Text = c.MonthName, Value = c.MonthId.ToString() }).ToList();
            TempData["MembershipMonthId"] = DbViewModel.GetMonthList(true).Select(c => new SelectListItem
                                                { Text = c.MonthName, Value = c.MonthId.ToString() }).ToList();
            TempData["GroupId"] = DbViewModel.GetGroupList().Select(c => new SelectListItem
                                    { Text = c.GroupName, Value = c.GroupId.ToString() }).ToList();
            TempData["MembershipMeansId"] = DbViewModel.GetMembershipMeansList().Select(c => new SelectListItem
                                                { Text = c.MembershipMeansName, Value = c.MembershipMeansId.ToString() }).ToList();
            
            // Date values of Ethiopian month (1 - 30)
            var datelist = GetRangeOfValues(1, 30).Select(c => new SelectListItem
                                { Text = c.ToString(), Value = c.ToString() }).ToList();
            datelist.Insert(0, new SelectListItem { Text = "", Value = "0" });
            TempData["DateList"] = datelist;

            // Year values of (1900 - current year)
            var yearlist = GetRangeOfValues(1900, DateTime.Now.Year - 7).Select(c => new SelectListItem
                                    { Text = c.ToString(), Value = c.ToString() }).ToList();
            yearlist.Insert(0, new SelectListItem { Text = "", Value = "0" });
            TempData["YearList"] = yearlist;

            TempData["MemberId"] = memberid;

            //If the member does not have a MemberInfo entry, return a "New" view
            //But if the member already has a MemberInfo entry, return an "Edit" view
            if (memberInfo == null)
            {
                return PartialView("MemberInfoNew", memberInfo);
            }

            return PartialView("MemberInfo", memberInfo);
        }

        public Member MemberInfoSave([Bind(Include = "MemberId,FullName,MotherName,GenderId,BirthDate,BirthMonthId,BirthYear,GroupId,MembershipDate,MembershipMonthId,MembershipYear,MembershipMeansId,NoServiceReason,Remark")] Member member)
        {
            DbViewModel.SetMember(member);

            return member;
        }

        /// <summary>
        /// Get a list of int values within the range start and end
        /// </summary>
        /// <param name="start">The starting value of the range</param>
        /// <param name="end">The ending value of the range</param>
        /// <returns></returns>
        public List<int> GetRangeOfValues(int start, int end)
        {
            List<int> numlist = new List<int>();
            for (int i = start; i <= end; i++)
                numlist.Add(i);

            return numlist;
        }

        //GET
        public ActionResult MemberPhoto([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPhoto memberPhoto = DbViewModel.GetMemberPhoto(memberid);
            TempData["MemberId"] = memberid;

            //If the member does not have a Photo entry, return a "New" view
            //But if the member already has a Photo entry, return an "Edit" view
            if (memberPhoto == null)
            {
                return PartialView("MemberPhotoNew", memberPhoto);
            }

            return PartialView("MemberPhoto", memberPhoto);
        }

        [HttpPost]
        public JsonResult MemberPhotoSave(MemberPhoto memberPhoto)
        {
            if (Request.Files.Count > 0)
            {
                //Upload file to server
                HttpPostedFileBase file = Request.Files[0];
                string fullpath = Server.MapPath(memberPhoto.PhotoFilePath); // Path including the file name
                string path = Server.MapPath(Path.GetDirectoryName(memberPhoto.PhotoFilePath)); // Path excluding the file name
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(fullpath);
                
                // Convert image file to bytes
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                memberPhoto.Photo = bytes;

                // Save media path, photo and remark to database
                DbViewModel.SetMemberPhoto(memberPhoto);
                
                ViewBag.Success = true;

            }

            return Json(memberPhoto);
        }
        
        public JsonResult MemberPhotoNew([Bind(Include = "MemberId,PhotoFilePath,Remark")] MemberPhoto memberPhoto)
        {
            // Save media path and description to database
            DbViewModel.SetMemberPhoto(memberPhoto);

            //Upload file to server
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string fullpath = Server.MapPath(memberPhoto.PhotoFilePath); // Path including the file name
                string path = Server.MapPath(Path.GetDirectoryName(memberPhoto.PhotoFilePath)); // Path excluding the file name
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(fullpath);
                ViewBag.Success = true;
            }

            return Json(memberPhoto);
        }

        //GET
        public ActionResult AddressInfo([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressInfo addressInfo = DbViewModel.GetAddressInfo(memberid);

            TempData["MemberId"] = memberid;
            TempData["SubcityId"] = DbViewModel.GetSubcityList().Select(c => new SelectListItem
            { Text = c.SubcityName, Value = c.SubcityId.ToString() }).ToList();
            TempData["HouseOwnershipTypeId"] = DbViewModel.GetHouseOwnershipTypeList().Select(c => new SelectListItem
            { Text = c.HouseOwnershipTypeName, Value = c.HouseOwnershipTypeId.ToString() }).ToList();

            //If the member does not have a AddressInfo entry, return a "New" view
            //But if the member already has a AddressInfo entry, return a "Edit" view
            if (addressInfo == null)
            {
                TempData["SubcityId"] = DbViewModel.GetSubcityList(true).Select(c => new SelectListItem
                { Text = c.SubcityName, Value = c.SubcityId.ToString() }).ToList();
                TempData["HouseOwnershipTypeId"] = DbViewModel.GetHouseOwnershipTypeList(true).Select(c => new SelectListItem
                { Text = c.HouseOwnershipTypeName, Value = c.HouseOwnershipTypeId.ToString() }).ToList();

                return PartialView("AddressInfoNew", addressInfo);
            }

            return PartialView("AddressInfo", addressInfo);
        }

        public AddressInfo AddressInfoSave([Bind(Include = "MemberId,SubcityId,Woreda,Kebele,HouseNo,HouseOwnershipId,HomePhoneNo,OfficePhoneNo,MobilePhoneNo,Email")] AddressInfo addressInfo)
        {
            DbViewModel.SetAddressInfo(addressInfo);

            return addressInfo;
        }

        public ActionResult AddressInfoNew([Bind(Include = "MemberId,SubcityId,Woreda,Kebele,HouseNo,HouseOwnershipId,HomePhoneNo,OfficePhoneNo,MobilePhoneNo,Email")] AddressInfo addressInfo)
        {
            DbViewModel.SetAddressInfo(addressInfo);

            TempData["SubcityId"] = DbViewModel.GetSubcityList(true).Select(c => new SelectListItem
            { Text = c.SubcityName, Value = c.SubcityId.ToString() }).ToList();
            TempData["HouseOwnershipTypeId"] = DbViewModel.GetHouseOwnershipTypeList(true).Select(c => new SelectListItem
            { Text = c.HouseOwnershipTypeName, Value = c.HouseOwnershipTypeId.ToString() }).ToList();

            return View(addressInfo);
        }



        //GET
        public ActionResult FamilyInfo([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyInfo familyInfo = DbViewModel.GetFamilyInfo(memberid);

            TempData["MemberId"] = memberid;
            TempData["MaritalStatusId"] = DbViewModel.GetMaritalStatusList().Select(c => new SelectListItem
                        { Text = c.MaritalStatusName, Value = c.MaritalStatusId.ToString() }).ToList();
            // Year values of (1900 - current year)
            var yearlist = GetRangeOfValues(1900, DateTime.Now.Year - 7).Select(c => new SelectListItem
                                    { Text = c.ToString(), Value = c.ToString() }).ToList();
            yearlist.Insert(0, new SelectListItem { Text = "", Value = "0" });
            TempData["YearList"] = yearlist;

            //If the member does not have a FamilyInfo entry, return a "New" view
            //But if the member already has a FamilyInfo entry, return an "Edit" view
            if (familyInfo == null)
            {
                TempData["MaritalStatusId"] = DbViewModel.GetMaritalStatusList(true).Select(c => new SelectListItem
                        { Text = c.MaritalStatusName, Value = c.MaritalStatusId.ToString() }).ToList();

                return PartialView("FamilyInfoNew", familyInfo);
            }
            
            return PartialView("FamilyInfo", familyInfo);
        }

        public FamilyInfo FamilyInfoSave([Bind(Include = "MemberId,MaritalStatusId,SpouseName,MarriageYear,NoOfSons,NoOfDaughters,NoOfSons1to5,NoOfDaughters1to5,NoOfSons6to12,NoOfDaughters6to12,NoOfSons13to20,NoOfDaughters13to20,NoOfSonsAbove20,NoOfDaughtersAbove20")] FamilyInfo familyInfo)
        {
            DbViewModel.SetFamilyInfo(familyInfo);

            return familyInfo;
        }

        public ActionResult FamilyInfoNew([Bind(Include = "MemberId,MaritalStatusId,SpouseName,MarriageYear,NoOfSons,NoOfDaughters,NoOfSons1to5,NoOfDaughters1to5,NoOfSons6to12,NoOfDaughters6to12,NoOfSons13to20,NoOfDaughters13to20,NoOfSonsAbove20,NoOfDaughtersAbove20")] FamilyInfo familyInfo)
        {
            if (ModelState.IsValid)
            {
                DbViewModel.SetFamilyInfo(familyInfo);
            }

            TempData["MaritalStatusId"] = DbViewModel.GetMaritalStatusList(true).Select(c => new SelectListItem
                            { Text = c.MaritalStatusName, Value = c.MaritalStatusId.ToString() }).ToList();
            
            return View(familyInfo);
        }

        //GET
        public ActionResult EducationAndJobInfo([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationAndJobInfo educJobInfo = DbViewModel.GetEducationAndJobInfo(memberid);

            TempData["MemberId"] = memberid;
            TempData["EducationLevelId"] = DbViewModel.GetEducationLevelList(true).Select(c => new SelectListItem
                                                { Text = c.EducationLevelName, Value = c.EducationLevelId.ToString() }).ToList();
            TempData["FieldOfStudyId"] = DbViewModel.GetFieldOfStudyList(true).Select(c => new SelectListItem
                                                { Text = c.FieldOfStudyName, Value = c.FieldOfStudyId.ToString() }).ToList();
            TempData["JobId"] = DbViewModel.GetJobList(true).Select(c => new SelectListItem
                                    { Text = c.JobName, Value = c.JobId.ToString() }).ToList();
            TempData["JobTypeId"] = DbViewModel.GetJobTypeList(true).Select(c => new SelectListItem
                                { Text = c.JobTypeName, Value = c.JobTypeId.ToString() }).ToList();

            //If the member does not have a MemberInfo entry, return a "New" view
            //But if the member already has a MemberInfo entry, return an "Edit" view
            if (educJobInfo == null)
            {
                TempData["EducationLevelId"] = DbViewModel.GetEducationLevelList(true).Select(c => new SelectListItem
                                                    { Text = c.EducationLevelName, Value = c.EducationLevelId.ToString() }).ToList();
                TempData["FieldOfStudyId"] = DbViewModel.GetFieldOfStudyList(true).Select(c => new SelectListItem
                                                    { Text = c.FieldOfStudyName, Value = c.FieldOfStudyId.ToString() }).ToList();
                TempData["JobId"] = DbViewModel.GetJobList(true).Select(c => new SelectListItem
                                        { Text = c.JobName, Value = c.JobId.ToString() }).ToList();
                TempData["JobTypeId"] = DbViewModel.GetJobTypeList(true).Select(c => new SelectListItem
                                            { Text = c.JobTypeName, Value = c.JobTypeId.ToString() }).ToList();

                return PartialView("EducationAndJobInfoNew", educJobInfo);
            }

            return PartialView("EducationAndJobInfo", educJobInfo);
        }

        public EducationAndJobInfo EducationAndJobInfoSave([Bind(Include = "MemberId,EducationLevelId,FieldOfStudyId,JobId,JobTypeId")] EducationAndJobInfo educJobInfo)
        {
            DbViewModel.SetEducationAndJobInfo(educJobInfo);

            return educJobInfo;
        }

        public ActionResult EducationAndJobInfoNew([Bind(Include = "MemberId,EducationLevelId,FieldOfStudyId,JobId,JobTypeId")] EducationAndJobInfo educJobInfo)
        {
            DbViewModel.SetEducationAndJobInfo(educJobInfo);

            TempData["EducationLevelId"] = DbViewModel.GetEducationLevelList(true).Select(c => new SelectListItem
            { Text = c.EducationLevelName, Value = c.EducationLevelId.ToString() }).ToList();
            TempData["FieldOfStudyId"] = DbViewModel.GetFieldOfStudyList(true).Select(c => new SelectListItem
            { Text = c.FieldOfStudyName, Value = c.FieldOfStudyId.ToString() }).ToList();
            TempData["JobId"] = DbViewModel.GetJobList(true).Select(c => new SelectListItem
            { Text = c.JobName, Value = c.JobId.ToString() }).ToList();
            TempData["JobTypeId"] = DbViewModel.GetJobTypeList(true).Select(c => new SelectListItem
            { Text = c.JobTypeName, Value = c.JobTypeId.ToString() }).ToList();
            
            return View(educJobInfo);
        }

        //GET
        public ActionResult MemberService([Optional] string memberid /* drop down value */)
        {
            List<MemberService> memberServiceList = DbViewModel.GetMemberServiceList(memberid).OrderBy(s => s.ServiceTypeId).ToList();

            TempData["MemberId"] = memberid;

            //For drop down list
            var serviceTypes = new SelectList(DbViewModel.GetServiceTypeList(), "ServiceTypeId", "ServiceTypeName").ToList();

            //Add the first unselected item
            serviceTypes.Insert(0, new SelectListItem { Text = "", Value = "0" });
            TempData["ServiceTypeSelectList"] = serviceTypes;

            //For drop down list
            var services = new SelectList(DbViewModel.GetServiceList(), "ServiceId", "ServiceName").ToList();

            //Add the first unselected item
            services.Insert(0, new SelectListItem { Text = "", Value = "0" });
            TempData["ServiceSelectList"] = services;

            // For json object in jquery
            TempData["ServiceTypeList"] = DbViewModel.GetServiceTypeList().Select(c => new { Id = c.ServiceTypeId, Name = c.ServiceTypeName }).ToList();
            TempData["ServiceList"] = DbViewModel.GetServiceList().Select(c => new { Id = c.ServiceId, Name = c.ServiceName }).ToList();

            return PartialView(memberServiceList);
        }

        public JsonResult AddMemberService([Bind(Include = "Id,MemberId,ServiceTypeId,ServiceId")] MemberService memService)
        {
            DbViewModel.SetMemberService(memService);

            return Json(memService);
        }

        //[HttpPost]
        public ActionResult DeleteMemberService(string Id)
        {
            MemberService memService = DbViewModel.GetMemberServiceById(Convert.ToInt32(Id));
            if (memService != null)
            {
                DbViewModel.DeleteMemberService(Id);
            }

            return new EmptyResult();
        }

        //GET
        public JsonResult GetLastMemberService(string memberid)
        {
            var lastMemberService = (from m in DbViewModel.GetAllMemberServices()
                                     where m.MemberId == memberid
                                    select new
                                    {
                                        Id = m.Id,
                                        MemberId = m.MemberId,
                                    }).ToList().LastOrDefault();

            return Json(lastMemberService, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectService()
        {
            List<Service> services = DbViewModel.GetServiceList();
            var list = services.Select(d => new[] { d.ServiceName, d.ServiceName });

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        public string UpdateService(string id, string origvalue, string value)
        {
            // No change
            if (origvalue == value)
                return value;

            MemberService memServ = DbViewModel.GetMemberServiceById(Convert.ToInt32(id));
            Service service = DbViewModel.GetServiceList().Find(d => d.ServiceName == value);

            memServ.ServiceId = service.ServiceId;
            DbViewModel.SetMemberService(memServ);

            return value;
        }

        public JsonResult SelectServiceType()
        {
            List<ServiceType> serviceTypes = DbViewModel.GetServiceTypeList();
            var list = serviceTypes.Select(d => new[] { d.ServiceTypeName, d.ServiceTypeName });

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        public string UpdateServiceType(string id, string origvalue, string value)
        {
            // No change
            if (origvalue == value)
                return value;

            MemberService memServ = DbViewModel.GetMemberServiceById(Convert.ToInt32(id));
            ServiceType serviceType = DbViewModel.GetServiceTypeList().Find(d => d.ServiceTypeName == value);

            memServ.ServiceTypeId = serviceType.ServiceTypeId;
            DbViewModel.SetMemberService(memServ);

            return value;
        }
    }
}