using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using GEJA_KHC_WEB.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Data.SqlClient;

namespace GEJA_KHC_WEB.ViewModels
{
    public class DbViewModel
    {
        static Geja_KHC_Entities db = new Geja_KHC_Entities();

        /// <summary>
        /// Returns a Group object from Database via webapi
        /// </summary>
        /// <param name="groupid">The primary key of the required Group 
        public static Group GetGroup(int? groupid)
        {
            return db.Group.Find(groupid);
        }

        /// <summary>
        /// Returns a list of all Group objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static List<Group> GetGroupList()
        {
            return db.Group.ToList();
        }

        /// <summary>
        /// Set a Group object from Database via web api
        /// </summary>
        /// <param name="group">The group to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetGroup(Group group)
        {
            if (group != null)
            {
                Group grp = db.Group.Find(group.GroupId);
                if (grp != null)
                {
                    // If an already existing Group, modify data
                    //grp.GroupId = group.GroupId;
                    grp.GroupName = group.GroupName;
                    grp.Pastor = group.Pastor;
                    grp.Remark = group.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err.ExceptionNumber;
                    }
                }
                else
                {
                    // Otherwise, create a new group
                    int lastId = db.Group.Select(s => s.GroupId).Max(); // get the last Id given
                    int newId = lastId + 1; // calculate the new Id

                    Group newGroup = new Group()
                    {
                        GroupId = newId,
                        GroupName = group.GroupName,
                        Pastor = group.Pastor,
                        Remark = group.Remark,
                    };

                    db.Group.Add(newGroup);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }

                return 0;
            }
            else
                return 0;
        }

        /// <summary>
        /// Set an Group object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Group to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteGroup(int id)
        {
            var group = db.Group.Find(id);
            if (group != null)
            {
                var grp = db.Group.Find(group.GroupId);
                if (grp != null)
                {
                    // Delete the group if exists
                    db.Group.Remove(grp);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a Member object from Database via web api
        /// </summary>
        /// <param name="memberid">The primary key of the required Member</param>
        /// <returns></returns>
        public static Member GetMember(string memberid)
        {
            var member = db.Member.Find(memberid);

            return member;
        }

        /// <summary>
        /// Set a Member object from Database via web api
        /// </summary>
        /// <param name="Member">The member to be set</param>
        /// <returns>he status id of the operation</returns>
        public static int SetMember(Member member)
        {
            if (member != null)
            {
                Member mem = db.Member.Find(member.MemberId);
                if (mem != null)
                {
                    // If an already existing Member, modify data
                    mem.MemberId = member.MemberId;
                    mem.FullName = member.FullName;
                    mem.MotherName = member.MotherName;
                    mem.GenderId = member.GenderId;
                    mem.BirthDate = member.BirthDate;
                    mem.BirthMonthId = member.BirthMonthId;
                    mem.BirthYear = member.BirthYear;
                    mem.GroupId = member.GroupId;
                    mem.MembershipDate = member.MembershipDate;
                    mem.MembershipMonthId = member.MembershipMonthId;
                    mem.MembershipYear = member.MembershipYear;
                    mem.MembershipMeansId = member.MembershipMeansId;
                    mem.NoServiceReason = member.NoServiceReason;
                    mem.Remark = member.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
                else
                {
                    // Otherwise, create a new member
                    Member newMember = new Member()
                    {
                        MemberId = member.MemberId,
                        FullName = member.FullName,
                        MotherName = member.MotherName,
                        GenderId = member.GenderId,
                        BirthDate = member.BirthDate,
                        BirthMonthId = member.BirthMonthId,
                        BirthYear = member.BirthYear,
                        GroupId = member.GroupId,
                        MembershipDate = member.MembershipDate,
                        MembershipMonthId = member.MembershipMonthId,
                        MembershipYear = member.MembershipYear,
                        MembershipMeansId = member.MembershipMeansId,
                        NoServiceReason = member.NoServiceReason,
                        Remark = member.Remark
                    };

                    db.Member.Add(newMember);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }

                return 0;
            }
            else
                return 0;
        }

        /// <summary>
        /// Set an Member object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Member to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteMember(string id)
        {
            Member member = db.Member.Find(id);
            if (member != null)
            {
                // Delete the member if exists
                db.Member.Remove(member);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                {
                    return sqlEx.Number;
                }
            }
            
            return 0;
        }
        
        public static List<Member> GetAllMembers()
        {
            return db.Member.ToList();
        }

        /// <summary>
        /// Returns a list of Member objects within a given Group from Database via web api
        /// </summary>
        /// <param name="groupid">The primary key of the Group that encompasses the Members</param>
        /// <returns></returns>
        public static List<Member> GetMemberList(int groupid)
        {
            return db.Member.Where(m => m.GroupId == groupid).ToList();
        }
        
        /// <summary>
        /// Returns a drop down list of all Groups from Database via web api
        /// </summary>
        /// <returns></returns>
        public static List<DropDownViewModel> GetGroupsDDL()
        {
            return ListViewModel.GetGroupList();
        }


        /// <summary>
        /// Returns a drop down list of Members within a given Group from Database via web api
        /// </summary>
        /// <param name="grouplist">The primary key of the Group that encompasses the Members</param>
        /// <returns></returns>
        public static List<DropDownViewModel> GetMembersDDL(int groupid)
        {
            return ListViewModel.GetMemberNameList(groupid);
        }

        /// <summary>
        /// Returns a Subcity object from Database via webapi
        /// </summary>
        /// <param name="subcityid">The primary key of the required Subcity 
        public static Subcity GetSubcity(int subcityid)
        {
            return db.Subcity.Find(subcityid);
        }

        /// <summary>
        /// Returns a EducationLevel object from Database via webapi
        /// </summary>
        /// <param name="educationlevelid">The primary key of the required EducationLevel 
        public static EducationLevel GetEducationLevel(int educationlevelid)
        {
            return db.EducationLevel.Find(educationlevelid);
        }

        /// <summary>
        /// Returns a HouseOwnershipType object from Database via webapi
        /// </summary>
        /// <param name="houseownershiptypeid">The primary key of the required HouseOwnershipType 
        public static HouseOwnershipType GetHouseOwnershipType(int houseownershiptypeid)
        {
            return db.HouseOwnershipType.Find(houseownershiptypeid);
        }

        /// <summary>
        /// Returns a JobType object from Database via webapi
        /// </summary>
        /// <param name="jobtypeid">The primary key of the required JobType 
        public static JobType GetJobType(int jobtypeid)
        {
            return db.JobType.Find(jobtypeid);
        }

        /// <summary>
        /// Returns a MaritalStatus object from Database via webapi
        /// </summary>
        /// <param name="maritalstatusid">The primary key of the required MaritalStatus 
        public static MaritalStatus GetMaritalStatus(int maritalstatusid)
        {
            return db.MaritalStatus.Find(maritalstatusid);
        }

        /// <summary>
        /// Returns a MembershipMeans object from Database via webapi
        /// </summary>
        /// <param name="membershipmeansid">The primary key of the required MembershipMeans 
        public static MembershipMeans GetMembershipMeans(int membershipmeansid)
        {
            return db.MembershipMeans.Find(membershipmeansid);
        }

        /// <summary>
        /// Returns a Month object from Database via webapi
        /// </summary>
        /// <param name="monthid">The primary key of the required Month 
        public static Month GetMonth(int monthid)
        {
            return db.Month.Find(monthid);
        }

        /// <summary>
        /// Returns a Service object from Database via webapi
        /// </summary>
        /// <param name="serviceid">The primary key of the required Service 
        public static Service GetService(int serviceid)
        {
            return db.Service.Find(serviceid);
        }

        // <summary>
        /// Set a Service object from Database via web api
        /// </summary>
        /// <param name="Service">The service to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetService(Service service)
        {
            if (service != null)
            {
                Service serv = db.Service.Find(service.ServiceId);
                if (serv != null)
                {
                    // If an already existing Service, modify data
                    //serv.ServiceId = service.ServiceId;
                    serv.ServiceName = service.ServiceName;
                    serv.Remark = service.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
                else
                {
                    // Otherwise, create a new service
                    int lastId = db.Service.Select(s => s.ServiceId).Max(); // get the last Id given
                    int newId = lastId + 1; // calculate the new Id

                    Service newService = new Service()
                    {
                        ServiceId = newId,
                        ServiceName = service.ServiceName,
                        Remark = service.Remark,
                    };

                    db.Service.Add(newService);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }

                return 0;
            }
            else
                return 0;
        }

        /// <summary>
        /// Set an Service object from Database via web api
        /// </summary>
        /// <param name="Service">The Service to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteService(int id)
        {
            var service = db.Service.Find(id);
            if (service != null)
            {
                var serv = db.Service.Find(service.ServiceId);
                if (serv != null)
                {
                    // Delete the service if exists
                    db.Service.Remove(serv);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a ServiceType object from Database via webapi
        /// </summary>
        /// <param name="serviceid">The primary key of the required ServiceType 
        public static ServiceType GetServiceType(int serviceid)
        {
            return db.ServiceType.Find(serviceid);
        }

        /// <summary>
        /// Returns a MemberPhoto object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static MemberPhoto GetMemberPhoto(string memberid)
        {
            return db.MemberPhoto.Find(memberid);
        }

        /// <summary>
        /// Set an MemberPhoto object from Database via web api
        /// </summary>
        /// <param name="memberPhoto">The memberPhoto to be set</param>
        /// <returns>The newly set memberPhoto</returns>
        public static MemberPhoto SetMemberPhoto(MemberPhoto memberPhoto)
        {
            if (memberPhoto != null)
            {
                MemberPhoto photo = db.MemberPhoto.Find(memberPhoto.MemberId);
                if (photo != null && photo.Photo != null)
                {
                    // If an already existing MemberPhoto, modify data
                    //addr.MemberId = memberPhoto.MemberId;
                    photo.PhotoFilePath = memberPhoto.PhotoFilePath;
                    photo.Photo = memberPhoto.Photo;
                    photo.Remark = memberPhoto.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new MemberPhoto();
                    }
                }
                else
                {
                    // Otherwise, create a new memberPhoto
                    MemberPhoto newPhoto = new MemberPhoto()
                    {
                        MemberId = memberPhoto.MemberId,
                        PhotoFilePath = memberPhoto.PhotoFilePath,
                        Photo = memberPhoto.Photo,
                        Remark = memberPhoto.Remark
                    };

                    db.MemberPhoto.Add(newPhoto);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new MemberPhoto();
                    }
                }
            }

            return new MemberPhoto();
        }

        /// <summary>
        /// Returns a AddressInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required AddressInfo 
        public static AddressInfo GetAddressInfo(string memberid)
        {
            return db.AddressInfo.Find(memberid);
        }

        /// <summary>
        /// Returns a list of AddressInfo objects from Database via webapi
        /// </summary>
        public static List<AddressInfo> GetAddressInfoList()
        {
            return db.AddressInfo.ToList();
        }

        /// <summary>
        /// Set an AddressInfo object from Database via web api
        /// </summary>
        /// <param name="AddressInfo">The addressInfo to be set</param>
        /// <returns>The newly set addressInfo</returns>
        public static AddressInfo SetAddressInfo(AddressInfo addressInfo)
        {
            if (addressInfo != null)
            {
                AddressInfo addr = db.AddressInfo.Find(addressInfo.MemberId);
                if (addr != null)
                {
                    // If an already existing AddressInfo, modify data
                    //addr.MemberId = addressInfo.MemberId;
                    addr.SubcityId = addressInfo.SubcityId;
                    addr.Woreda = addressInfo.Woreda;
                    addr.Kebele = addressInfo.Kebele;
                    addr.HouseNo = addressInfo.HouseNo;
                    addr.HouseOwnershipId = addressInfo.HouseOwnershipId;
                    addr.HomePhoneNo = addressInfo.HomePhoneNo;
                    addr.OfficePhoneNo = addressInfo.OfficePhoneNo;
                    addr.MobilePhoneNo = addressInfo.MobilePhoneNo;
                    addr.Email = addressInfo.Email;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new AddressInfo();
                    }
                }
                else
                {
                    // Otherwise, create a new addressInfo
                    AddressInfo newAddressInfo = new AddressInfo()
                    {
                        MemberId = addressInfo.MemberId,
                        SubcityId = addressInfo.SubcityId,
                        Woreda = addressInfo.Woreda,
                        Kebele = addressInfo.Kebele,
                        HouseNo = addressInfo.HouseNo,
                        HouseOwnershipId = addressInfo.HouseOwnershipId,
                        HomePhoneNo = addressInfo.HomePhoneNo,
                        OfficePhoneNo = addressInfo.OfficePhoneNo,
                        MobilePhoneNo = addressInfo.MobilePhoneNo,
                        Email = addressInfo.Email,
                    };

                    db.AddressInfo.Add(newAddressInfo);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new AddressInfo();
                    }
                }
            }

            return new AddressInfo();
        }

        /// <summary>
        /// Returns a FamilyInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required FamilyInfo 
        public static FamilyInfo GetFamilyInfo(string memberid)
        {
            return db.FamilyInfo.Find(memberid);
        }

        /// <summary>
        /// Returns a list of FamilyInfo objects from Database via webapi
        /// </summary>
        public static List<FamilyInfo> GetFamilyInfoList()
        {
            return db.FamilyInfo.ToList();
        }

        /// <summary>
        /// Set an FamilyInfo object from Database via web api
        /// </summary>
        /// <param name="FamilyInfo">The familyInfo to be set</param>
        /// <returns>The newly set familyInfo</returns>
        public static FamilyInfo SetFamilyInfo(FamilyInfo familyInfo)
        {
            if (familyInfo != null)
            {
                FamilyInfo fam = db.FamilyInfo.Find(familyInfo.MemberId);
                if (fam != null)
                {
                    // If an already existing FamilyInfo, modify data
                    //fam.MemberId = familyInfo.MemberId
                    fam.MaritalStatusId = familyInfo.MaritalStatusId;
                    fam.SpouseName = familyInfo.SpouseName;
                    fam.MarriageYear = familyInfo.MarriageYear;
                    fam.NoOfSons = familyInfo.NoOfSons;
                    fam.NoOfDaughters = familyInfo.NoOfDaughters;
                    fam.NoOfSons1to5 = familyInfo.NoOfSons1to5;
                    fam.NoOfDaughters1to5 = familyInfo.NoOfDaughters1to5;
                    fam.NoOfSons6to12 = familyInfo.NoOfSons6to12;
                    fam.NoOfDaughters6to12 = familyInfo.NoOfDaughters6to12;
                    fam.NoOfSons13to20 = familyInfo.NoOfSons13to20;
                    fam.NoOfDaughters13to20 = familyInfo.NoOfDaughters13to20;
                    fam.NoOfSonsAbove20 = familyInfo.NoOfSonsAbove20;
                    fam.NoOfDaughtersAbove20 = familyInfo.NoOfDaughtersAbove20;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new FamilyInfo();
                    }
                }
                else
                {
                    // Otherwise, create a new familyInfo
                    FamilyInfo newFamilyInfo = new FamilyInfo()
                    {
                        MemberId = familyInfo.MemberId,
                        MaritalStatusId = familyInfo.MaritalStatusId,
                        SpouseName = familyInfo.SpouseName,
                        MarriageYear = familyInfo.MarriageYear,
                        NoOfSons = familyInfo.NoOfSons,
                        NoOfDaughters = familyInfo.NoOfDaughters,
                        NoOfSons1to5 = familyInfo.NoOfSons1to5,
                        NoOfDaughters1to5 = familyInfo.NoOfDaughters1to5,
                        NoOfSons6to12 = familyInfo.NoOfSons6to12,
                        NoOfDaughters6to12 = familyInfo.NoOfDaughters6to12,
                        NoOfSons13to20 = familyInfo.NoOfSons13to20,
                        NoOfDaughters13to20 = familyInfo.NoOfDaughters13to20,
                        NoOfSonsAbove20 = familyInfo.NoOfSonsAbove20,
                        NoOfDaughtersAbove20 = familyInfo.NoOfDaughtersAbove20,
                    };

                    db.FamilyInfo.Add(newFamilyInfo);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new FamilyInfo();
                    }
                }
            }

            return (new FamilyInfo());
        }


        /// <summary>
        /// Returns a EducationAndJobInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required EducationAndJobInfo 
        public static EducationAndJobInfo GetEducationAndJobInfo(string memberid)
        {
            return db.EducationAndJobInfo.Find(memberid);
        }

        /// <summary>
        /// Returns a list of Education and Job Info objects from Database via webapi
        /// </summary>
        public static List<EducationAndJobInfo> GetEducationAndJobInfoList()
        {
            return db.EducationAndJobInfo.ToList();
        }

        /// <summary>
        /// Set an EducationAndJobInfo object from Database via web api
        /// </summary>
        /// <param name="EducationAndJobInfo">The educAndJobInfo to be set</param>
        /// <returns>The newly set educAndJobInfo</returns>
        public static EducationAndJobInfo SetEducationAndJobInfo(EducationAndJobInfo educAndJobInfo)
        {
            if (educAndJobInfo != null)
            {
                EducationAndJobInfo edJob = db.EducationAndJobInfo.Find(educAndJobInfo.MemberId);
                if (edJob != null)
                {
                    // If an already existing EducationAndJobInfo, modify data
                    //edJob.MemberId = educAndJobInfo.MemberId;
                    edJob.EducationLevelId = educAndJobInfo.EducationLevelId;
                    edJob.FieldOfStudyId = educAndJobInfo.FieldOfStudyId;
                    edJob.JobId = educAndJobInfo.JobId;
                    edJob.JobTypeId = educAndJobInfo.JobTypeId;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new EducationAndJobInfo();
                    }
                }
                else
                {
                    // Otherwise, create a new educAndJobInfo
                    EducationAndJobInfo newEducationAndJobInfo = new EducationAndJobInfo()
                    {
                        MemberId = educAndJobInfo.MemberId,
                        EducationLevelId = educAndJobInfo.EducationLevelId,
                        FieldOfStudyId = educAndJobInfo.FieldOfStudyId,
                        JobId = educAndJobInfo.JobId,
                        JobTypeId = educAndJobInfo.JobTypeId,
                    };

                    db.EducationAndJobInfo.Add(newEducationAndJobInfo);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new EducationAndJobInfo();
                    }
                }
            }

            return (new EducationAndJobInfo());
        }

        /// <summary>
        /// Returns a list of MemberService objects from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required MemberService 
        public static List<MemberService> GetMemberServiceList(string memberid)
        {
            return db.MemberService.Where(m => m.MemberId == memberid).ToList();
        }

        /// <summary>
        /// Returns all MemberService objects from Database via webapi
        /// </summary>
        public static List<MemberService> GetAllMemberServices()
        {
            return db.MemberService.ToList();
        }

        /// <summary>
        /// Returns a MemberService object from Database via webapi
        /// </summary>
        /// <param name="id">The primary key of the required MemberService 
        public static MemberService GetMemberServiceById(int id)
        {
            return db.MemberService.Find(id);
        }

        /// <summary>
        /// Set an MemberService object from Database via web api
        /// </summary>
        /// <param name="MemberService">The memberService to be set</param>
        /// <returns>The newly set memberService</returns>
        public static MemberService SetMemberService(MemberService memberService)
        {
            if (memberService != null)
            {
                MemberService memServ = db.MemberService.Find(memberService.Id);
                if (memServ != null)
                {
                    // If an already existing MemberService, modify data
                    //memServ.Id = memberService.Id;
                    //memServ.MemberId = memberService.MemberId;
                    memServ.ServiceTypeId = memberService.ServiceTypeId;
                    memServ.ServiceId = memberService.ServiceId;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new MemberService();
                    }
                }
                else
                {
                    // Otherwise, create a new memberService
                    MemberService newMemberService = new MemberService()
                    {
                        MemberId = memberService.MemberId,
                        ServiceTypeId = memberService.ServiceTypeId,
                        ServiceId = memberService.ServiceId,
                    };

                    db.MemberService.Add(newMemberService);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return new MemberService();
                    }
                }
            }

            return new MemberService();
        }

        /// <summary>
        /// Set an MemberService object from Database via web api
        /// </summary>
        /// <param name="MemberService">The memberService to be set</param>
        /// <returns>The newly set memberService</returns>
        public static int DeleteMemberService(string id)
        {
            var memberservice = db.MemberService.Find(id);
            if (memberservice != null)
            {
                var memserv = db.MemberService.Find(memberservice.Id);
                if (memserv != null)
                {
                    // Delete the member service if exists
                    db.MemberService.Remove(memserv);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a Gender object from Database via webapi
        /// </summary>
        /// <param name="genderid">The primary key of the required Gender 
        public static Gender GetGender(int genderid)
        {
            return db.Gender.Find(genderid);
        }

        /// <summary>
        /// Returns a list of all Genders objects from Database via web api
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Gender> GetGenderList(bool pleaseselect = false)
        {
            var genders = db.Gender.ToList();
            if (pleaseselect)
                genders.Insert(0, new Gender { GenderName = "", GenderId = 0 });

            return genders;
        }

        /// <summary>
        /// Returns a list of Subcity objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Subcity> GetSubcityList(bool pleaseselect = false)
        {
            var subcities = db.Subcity.ToList();
            if (pleaseselect)
                subcities.Insert(0, new Subcity { SubcityName = "", SubcityId = 0 });

            return subcities;
        }

        /// <summary>
        /// Returns a list of EducationLevel objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<EducationLevel> GetEducationLevelList(bool pleaseselect = false)
        {
            var educationlevel =  db.EducationLevel.ToList();
            if (pleaseselect)
                educationlevel.Insert(0, new EducationLevel { EducationLevelName = "", EducationLevelId = 0 });

            return educationlevel;
        }

        /// <summary>
        /// Returns a FieldOfStudy object from Database via webapi
        /// </summary>
        /// <param name="fieldofstudyid">The primary key of the required FieldOfStudy 
        public static FieldOfStudy GetFieldOfStudy(int fieldofstudyid)
        {
            return db.FieldOfStudy.Find(fieldofstudyid);
        }

        /// <summary>
        /// Returns a list of FieldOfStudy objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<FieldOfStudy> GetFieldOfStudyList(bool pleaseselect = false)
        {
            var fieldofstudy = db.FieldOfStudy.ToList();
            if (pleaseselect)
                fieldofstudy.Insert(0, new FieldOfStudy { FieldOfStudyName = "", FieldOfStudyId = 0 });

            return fieldofstudy;
        }

        // <summary>
        /// Set a FieldOfStudy object from Database via web api
        /// </summary>
        /// <param name="FieldOfStudy">The field to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetFieldOfStudy(FieldOfStudy field)
        {
            if (field != null)
            {
                FieldOfStudy fld = db.FieldOfStudy.Find(field.FieldOfStudyId);
                if (fld != null)
                {
                    // If an already existing FieldOfStudy, modify data
                    //serv.FieldOfStudyId = field.FieldOfStudyId;
                    fld.FieldOfStudyName = field.FieldOfStudyName;
                    fld.EnglishName = field.EnglishName;
                    fld.Remark = field.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
                else
                {
                    int lastId = db.FieldOfStudy.Select(s => s.FieldOfStudyId).Max();
                    int newId = lastId + 1;

                    // Otherwise, create a new field
                    FieldOfStudy newFieldOfStudy = new FieldOfStudy()
                    {
                        FieldOfStudyId = newId,
                        FieldOfStudyName = field.FieldOfStudyName,
                        EnglishName = field.EnglishName,
                        Remark = field.Remark,
                    };

                    db.FieldOfStudy.Add(newFieldOfStudy);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }

                return 0;
            }
            else
                return 0;
        }

        /// <summary>
        /// Set an FieldOfStudy object from Database via web api
        /// </summary>
        /// <param name="FieldOfStudy">The FieldOfStudy to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteFieldOfStudy(int id)
        {
            var field = db.FieldOfStudy.Find(id);
            if (field != null)
            {
                var fld = db.FieldOfStudy.Find(field.FieldOfStudyId);
                if (fld != null)
                {
                    // Delete the field of study if exists
                    db.FieldOfStudy.Remove(fld);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a list of HouseOwnershipType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<HouseOwnershipType> GetHouseOwnershipTypeList(bool pleaseselect = false)
        {
            var houseownershiptypes = db.HouseOwnershipType.ToList();
            if (pleaseselect)
                houseownershiptypes.Insert(0, new HouseOwnershipType { HouseOwnershipTypeName = "", HouseOwnershipTypeId = 0 });

            return houseownershiptypes;
        }

        /// <summary>
        /// Returns a Job object from Database via webapi
        /// </summary>
        /// <param name="jobid">The primary key of the required Job 
        public static Job GetJob(int jobid)
        {
            return db.Job.Find(jobid);
        }


        /// <summary>
        /// Returns a list of Job objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Job> GetJobList(bool pleaseselect = false)
        {
            var jobs = db.Job.ToList();
            if (pleaseselect)
                jobs.Insert(0, new Job { JobName = "", JobId = 0 });

            return jobs;
        }

        // <summary>
        /// Set a Job object from Database via web api
        /// </summary>
        /// <param name="Job">The job to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetJob(Job job)
        {
            if (job != null)
            {
                Job jb = db.Job.Find(job.JobId);
                if (jb != null)
                {
                    // If an already existing Job, modify data
                    //serv.JobId = job.JobId;
                    jb.JobName = job.JobName;
                    jb.Remark = job.Remark;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
                else
                {
                    // Otherwise, create a new job
                    int lastId = db.Job.Select(s => s.JobId).Max(); // get the last Id given
                    int newId = lastId + 1; // calculate the new Id

                    Job newJob = new Job()
                    {
                        JobId = newId,
                        JobName = job.JobName,
                        Remark = job.Remark,
                    };

                    db.Job.Add(newJob);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }

                return 0;
            }
            else
                return 0;
        }

        /// <summary>
        /// Set an Job object from Database via web api
        /// </summary>
        /// <param name="Job">The Job to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteJob(int id)
        {
            var job = db.Job.Find(id);
            if (job != null)
            {
                var jb = db.Job.Find(job.JobId);
                if (jb != null)
                {
                    // Delete the job if exists
                    db.Job.Remove(jb);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) when (e.InnerException?.InnerException is SqlException sqlEx)
                    {
                        return sqlEx.Number;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a list of JobType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<JobType> GetJobTypeList(bool pleaseselect = false)
        {
            var jobtypes = db.JobType.ToList();
            if (pleaseselect)
                jobtypes.Insert(0, new JobType { JobTypeName = "", JobTypeId = 0 });

            return jobtypes;
        }

        /// <summary>
        /// Returns a list of MaritalStatus objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<MaritalStatus> GetMaritalStatusList(bool pleaseselect = false)
        {
            var maritalstatus = db.MaritalStatus.ToList();
            if (pleaseselect)
                maritalstatus.Insert(0, new MaritalStatus { MaritalStatusName = "", MaritalStatusId = 0 });

            return maritalstatus;
        }

        /// <summary>
        /// Returns a list of MembershipMeans objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<MembershipMeans> GetMembershipMeansList(bool pleaseselect = false)
        {
            var membershipmeans = db.MembershipMeans.ToList();
            if (pleaseselect)
                membershipmeans.Insert(0, new MembershipMeans { MembershipMeansName = "", MembershipMeansId = 0 });

            return membershipmeans;
        }

        /// <summary>
        /// Returns a list of Month objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Month> GetMonthList(bool pleaseselect = false)
        {
            var months = db.Month.ToList();
            if (pleaseselect)
                months.Insert(0, new Month { MonthName = "", MonthId = 0 });

            return months;
        }

        /// <summary>
        /// Returns a list of Service objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Service> GetServiceList(bool pleaseselect = false)
        {
            var services = db.Service.ToList();
            if (pleaseselect)
                services.Insert(0, new Service { ServiceName = "", ServiceId = 0 });

            return services;
        }

        /// <summary>
        /// Returns a list of ServiceType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<ServiceType> GetServiceTypeList(bool pleaseselect = false)
        {
            var servicetypes = db.ServiceType.ToList();
            if (pleaseselect)
                servicetypes.Insert(0, new ServiceType { ServiceTypeName = "", ServiceTypeId = 0 });

            return servicetypes;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given education level objects from Database via webapi
        /// </summary>
        public static List<EducationLevelByGroup> GetEducationLevelByGroup()
        {
            return db.EducationLevelByGroup.ToList();
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given marital status objects from Database via webapi
        /// </summary>
        public static List<MaritalStatusByGroup> GetMaritalStatusByGroup()
        {
            return db.MaritalStatusByGroup.ToList();
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given membership means objects from Database via webapi
        /// </summary>
        public static List<MembershipMeansByGroup> GetMembershipMeansByGroup()
        {
            return db.MembershipMeansByGroup.ToList();
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given subcity objects from Database via webapi
        /// </summary>
        public static List<SubcityByGroup> GetSubcityByGroup()
        {
            return db.SubcityByGroup.ToList();
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given gender objects from Database via webapi
        /// </summary>
        public static List<GenderByGroup> GetGenderByGroup()
        {
            return db.GenderByGroup.ToList();
        }
    }
}