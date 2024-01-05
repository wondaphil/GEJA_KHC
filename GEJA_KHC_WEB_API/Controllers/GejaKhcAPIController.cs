using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GEJA_KHC_WEB_API.Models;
using GEJA_KHC_WEB_API.ViewModels;

namespace GEJA_KHC_WEB_API.Controllers
{
    public class GejaKhcAPIController : ApiController
    {
        Geja_KHC_Entities db = new Geja_KHC_Entities();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(string id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
        }

        //////////////////////////
        // Group
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetGroupList")]
        [System.Web.Http.HttpPost]
        public List<GroupViewModel> GetGroupList()
        {
            List<Group> groups = db.Group.ToList();
            List<GroupViewModel> groupsVm = (from g in groups
                                           select new GroupViewModel
                                            {
                                                GroupId = g.GroupId,
                                                GroupName = g.GroupName,
                                                Pastor = g.Pastor,
                                                Remark = g.Remark
                                            }).ToList();
            
            return groupsVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetGroup")]
        [System.Web.Http.HttpPost]
        public GroupViewModel GetGroup(Group group)
        {
            GroupViewModel groupVm = new GroupViewModel();
            if (group != null)
            {
                Group grp = db.Group.Find(group.GroupId);
                groupVm = new GroupViewModel()
                {
                    GroupId = grp.GroupId,
                    GroupName = grp.GroupName,
                    Pastor = grp.Pastor,
                    Remark = grp.Remark
                };
            }

            return groupVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetGroup")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetGroup(Group group)
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
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }

                return (new ErrorViewModel());
            }
            else
                return (new ErrorViewModel());
        }

        
        [System.Web.Http.Route("api/GejaKhcAPI/DeleteGroup")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteGroup(Group group)
        {
            if (group != null)
            {
                Group grp = db.Group.Find(group.GroupId);
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        //////////////////////////
        // Member
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetMemberList")]
        [System.Web.Http.HttpPost]
        //public List<Member> GetMemberList(Group group)
        //{
        //    List<Member> members = new List<Member>();
        //    if (group != null)
        //        members = db.Member.Where(c => c.GroupId == group.GroupId).ToList();

        //    return members;
        //}

        public List<MemberViewModel> GetMemberList(Group group)
        {
            List<Member> members = db.Member.Where(m => m.GroupId == group.GroupId).ToList();
            
            List<MemberViewModel> membersVm = (from mem in members
                                               select new MemberViewModel
                                               {
                                                   MemberId = mem.MemberId,
                                                   FullName = mem.FullName,
                                                   MotherName = mem.MotherName,
                                                   GenderId = mem.GenderId,
                                                   BirthDate = mem.BirthDate,
                                                   BirthMonthId = mem.BirthMonthId,
                                                   BirthYear = mem.BirthYear,
                                                   GroupId = mem.GroupId,
                                                   MembershipDate = mem.MembershipDate,
                                                   MembershipMonthId = mem.MembershipMonthId,
                                                   MembershipYear = mem.MembershipYear,
                                                   MembershipMeansId = mem.MembershipMeansId,
                                                   NoServiceReason = mem.NoServiceReason,
                                                   Remark = mem.Remark
                                               }).ToList();

            return membersVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetAllMembers")]
        [System.Web.Http.HttpPost]
        public List<MemberViewModel> GetAllMembers()
        {
            List<Member> members = db.Member.ToList();
            List<MemberViewModel> membersVm = (from g in members
                                               select new MemberViewModel
                                               {
                                                   MemberId = g.MemberId,
                                                   FullName = g.FullName,
                                                   MotherName = g.MotherName,
                                                   GenderId = g.GenderId,
                                                   BirthDate = g.BirthDate,
                                                   BirthMonthId = g.BirthMonthId,
                                                   BirthYear = g.BirthYear,
                                                   GroupId = g.GroupId,
                                                   MembershipDate = g.MembershipDate,
                                                   MembershipMonthId = g.MembershipMonthId,
                                                   MembershipYear = g.MembershipYear,
                                                   MembershipMeansId = g.MembershipMeansId,
                                                   NoServiceReason = g.NoServiceReason,
                                                   Remark = g.Remark
                                               }).ToList();

            return membersVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMember")]
        [System.Web.Http.HttpPost]
        public MemberViewModel GetMember(Member member)
        {
            MemberViewModel memberVm = null;
            if (member != null && member.MemberId != "")
            {
                Member mem = db.Member.Find(member.MemberId);
                memberVm = new MemberViewModel()
                {
                    MemberId = mem.MemberId,
                    FullName = mem.FullName,
                    MotherName = mem.MotherName,
                    GenderId = mem.GenderId,
                    BirthDate = mem.BirthDate,
                    BirthMonthId = mem.BirthMonthId,
                    BirthYear = mem.BirthYear,
                    GroupId = mem.GroupId,
                    MembershipDate = mem.MembershipDate,
                    MembershipMonthId = mem.MembershipMonthId,
                    MembershipYear = mem.MembershipYear,
                    MembershipMeansId = mem.MembershipMeansId,
                    NoServiceReason = mem.NoServiceReason,
                    Remark = mem.Remark
                };
            }

            return memberVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetMember")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetMember(Member member)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
                
                return (new ErrorViewModel());
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/DeleteMember")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteMember(Member member)
        {
            if (member != null)
            {
                Member grp = db.Member.Find(member.MemberId);
                if (grp != null)
                {
                    // Delete the member if exists
                    db.Member.Remove(grp);

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
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetGender")]
        [System.Web.Http.HttpPost]
        public GenderViewModel GetGender(Gender gender)
        {
            GenderViewModel genderVm = new GenderViewModel();
            if (gender != null)
            {
                Gender gen = db.Gender.Find(gender.GenderId);
                genderVm = new GenderViewModel()
                {
                    GenderId = gen.GenderId,
                    GenderName = gen.GenderName,
                };
            }

            return genderVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetGenderList")]
        [System.Web.Http.HttpPost]
        public List<GenderViewModel> GetGenderList()
        {
            List<Gender> gender = db.Gender.ToList();
            List<GenderViewModel> genderVm = (from g in gender
                                              select new GenderViewModel
                                              {
                                                  GenderId = g.GenderId,
                                                  GenderName = g.GenderName,
                                              }).ToList();

            return genderVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetSubcity")]
        [System.Web.Http.HttpPost]
        public SubcityViewModel GetSubcity(Subcity subcity)
        {
            SubcityViewModel subcityVm = new SubcityViewModel();
            if (subcity != null)
            {
                Subcity sub = db.Subcity.Find(subcity.SubcityId);
                subcityVm = new SubcityViewModel()
                {
                    SubcityId = sub.SubcityId,
                    SubcityName = sub.SubcityName,
                    Remark = sub.Remark
                };
            }

            return subcityVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetSubcityList")]
        [System.Web.Http.HttpPost]
        public List<SubcityViewModel> GetSubcityList()
        {
            List<Subcity> subcity = db.Subcity.ToList();
            List<SubcityViewModel> subcityVm = (from g in subcity
                                                select new SubcityViewModel
                                                {
                                                    SubcityId = g.SubcityId,
                                                    SubcityName = g.SubcityName,
                                                    Remark = g.Remark,
                                                }).ToList();

            return subcityVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetEducationLevel")]
        [System.Web.Http.HttpPost]
        public EducationLevelViewModel GetEducationLevel(EducationLevel educationlevel)
        {
            EducationLevelViewModel educationlevelVm = new EducationLevelViewModel();
            if (educationlevel != null)
            {
                EducationLevel education = db.EducationLevel.Find(educationlevel.EducationLevelId);
                educationlevelVm = new EducationLevelViewModel()
                {
                    EducationLevelId = education.EducationLevelId,
                    EducationLevelName = education.EducationLevelName,
                    Remark = education.Remark
                };
            }

            return educationlevelVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetEducationLevelList")]
        [System.Web.Http.HttpPost]
        public List<EducationLevelViewModel> GetEducationLevelList()
        {
            List<EducationLevel> eduJob = db.EducationLevel.ToList();
            List<EducationLevelViewModel> eduJobVm = (from g in eduJob
                                                      select new EducationLevelViewModel
                                                      {
                                                          EducationLevelId = g.EducationLevelId,
                                                          EducationLevelName = g.EducationLevelName,
                                                          Remark = g.Remark,
                                                      }).ToList();

            return eduJobVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetFieldOfStudy")]
        [System.Web.Http.HttpPost]
        public FieldOfStudyViewModel GetFieldOfStudy(FieldOfStudy fieldofstudy)
        {
            FieldOfStudyViewModel fieldVm = new FieldOfStudyViewModel();
            if (fieldofstudy != null)
            {
                FieldOfStudy field = db.FieldOfStudy.Find(fieldofstudy.FieldOfStudyId);
                fieldVm = new FieldOfStudyViewModel()
                {
                    FieldOfStudyId = field.FieldOfStudyId,
                    FieldOfStudyName = field.FieldOfStudyName,
                    EnglishName = field.EnglishName,
                    Remark = field.Remark
                };
            }

            return fieldVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetFieldOfStudyList")]
        [System.Web.Http.HttpPost]
        public List<FieldOfStudyViewModel> GetFieldOfStudyList()
        {
            List<FieldOfStudy> field = db.FieldOfStudy.ToList();
            List<FieldOfStudyViewModel> fieldVm = (from g in field
                                                   select new FieldOfStudyViewModel
                                                   {
                                                       FieldOfStudyId = g.FieldOfStudyId,
                                                       FieldOfStudyName = g.FieldOfStudyName,
                                                       EnglishName = g.EnglishName,
                                                       Remark = g.Remark,
                                                   }).ToList();

            return fieldVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetFieldOfStudy")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetFieldOfStudy(FieldOfStudy field)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }

                return (new ErrorViewModel());
            }
            else
                return (new ErrorViewModel());
        }


        [System.Web.Http.Route("api/GejaKhcAPI/DeleteFieldOfStudy")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteFieldOfStudy(FieldOfStudy field)
        {
            if (field != null)
            {
                FieldOfStudy fld = db.FieldOfStudy.Find(field.FieldOfStudyId);
                if (fld != null)
                {
                    // Delete the field if exists
                    db.FieldOfStudy.Remove(fld);

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
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetHouseOwnershipType")]
        [System.Web.Http.HttpPost]
        public HouseOwnershipTypeViewModel GetHouseOwnershipType(HouseOwnershipType ownership)
        {
            HouseOwnershipTypeViewModel ownerVm = new HouseOwnershipTypeViewModel();
            if (ownership != null)
            {
                HouseOwnershipType owner = db.HouseOwnershipType.Find(ownership.HouseOwnershipTypeId);
                ownerVm = new HouseOwnershipTypeViewModel()
                {
                    HouseOwnershipTypeId = owner.HouseOwnershipTypeId,
                    HouseOwnershipTypeName = owner.HouseOwnershipTypeName,
                    Remark = owner.Remark
                };
            }

            return ownerVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetHouseOwnershipTypeList")]
        [System.Web.Http.HttpPost]
        public List<HouseOwnershipTypeViewModel> GetHouseOwnershipTypeList()
        {
            List<HouseOwnershipType> ownership = db.HouseOwnershipType.ToList();
            List<HouseOwnershipTypeViewModel> ownershipVm = (from g in ownership
                                                             select new HouseOwnershipTypeViewModel
                                                             {
                                                                 HouseOwnershipTypeId = g.HouseOwnershipTypeId,
                                                                 HouseOwnershipTypeName = g.HouseOwnershipTypeName,
                                                                 Remark = g.Remark,
                                                             }).ToList();

            return ownershipVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetJob")]
        [System.Web.Http.HttpPost]
        public JobViewModel GetJob(Job job)
        {
            JobViewModel jobVm = new JobViewModel();
            if (job != null)
            {
                Job jb = db.Job.Find(job.JobId);
                jobVm = new JobViewModel()
                {
                    JobId = jb.JobId,
                    JobName = jb.JobName,
                    Remark = jb.Remark
                };
            }

            return jobVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetJobList")]
        [System.Web.Http.HttpPost]
        public List<JobViewModel> GetJobList()
        {
            List<Job> job = db.Job.ToList();
            List<JobViewModel> jobVm = (from g in job
                                        select new JobViewModel
                                        {
                                            JobId = g.JobId,
                                            JobName = g.JobName,
                                            Remark = g.Remark,
                                        }).ToList();

            return jobVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetJob")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetJob(Job job)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }

                return (new ErrorViewModel());
            }
            else
                return (new ErrorViewModel());
        }


        [System.Web.Http.Route("api/GejaKhcAPI/DeleteJob")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteJob(Job job)
        {
            if (job != null)
            {
                Job jb = db.Job.Find(job.JobId);
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetJobType")]
        [System.Web.Http.HttpPost]
        public JobTypeViewModel GetJobType(JobType jobtype)
        {
            JobTypeViewModel jobVm = new JobTypeViewModel();
            if (jobtype != null)
            {
                JobType job = db.JobType.Find(jobtype.JobTypeId);
                jobVm = new JobTypeViewModel()
                {
                    JobTypeId = job.JobTypeId,
                    JobTypeName = job.JobTypeName,
                    Remark = job.Remark
                };
            }

            return jobVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetJobTypeList")]
        [System.Web.Http.HttpPost]
        public List<JobTypeViewModel> GetJobTypeList()
        {
            List<JobType> jobType = db.JobType.ToList();
            List<JobTypeViewModel> jobTypeVm = (from g in jobType
                                                select new JobTypeViewModel
                                                {
                                                    JobTypeId = g.JobTypeId,
                                                    JobTypeName = g.JobTypeName,
                                                    Remark = g.Remark,
                                                }).ToList();

            return jobTypeVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMaritalStatus")]
        [System.Web.Http.HttpPost]
        public MaritalStatusViewModel GetMaritalStatus(MaritalStatus maritalstatus)
        {
            MaritalStatusViewModel statusVm = new MaritalStatusViewModel();
            if (maritalstatus != null)
            {
                MaritalStatus status = db.MaritalStatus.Find(maritalstatus.MaritalStatusId);
                statusVm = new MaritalStatusViewModel()
                {
                    MaritalStatusId = status.MaritalStatusId,
                    MaritalStatusName = status.MaritalStatusName,
                    Remark = status.Remark
                };
            }

            return statusVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMaritalStatusList")]
        [System.Web.Http.HttpPost]
        public List<MaritalStatusViewModel> GetMaritalStatusList()
        {
            List<MaritalStatus> status = db.MaritalStatus.ToList();
            List<MaritalStatusViewModel> statusVm = (from g in status
                                                     select new MaritalStatusViewModel
                                                     {
                                                         MaritalStatusId = g.MaritalStatusId,
                                                         MaritalStatusName = g.MaritalStatusName,
                                                         Remark = g.Remark,
                                                     }).ToList();

            return statusVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMembershipMeans")]
        [System.Web.Http.HttpPost]
        public MembershipMeansViewModel GetMembershipMeans(MembershipMeans membershipmeans)
        {
            MembershipMeansViewModel meansVm = new MembershipMeansViewModel();
            if (membershipmeans != null)
            {
                MembershipMeans meanns = db.MembershipMeans.Find(membershipmeans.MembershipMeansId);
                meansVm = new MembershipMeansViewModel()
                {
                    MembershipMeansId = meanns.MembershipMeansId,
                    MembershipMeansName = meanns.MembershipMeansName,
                    Remark = meanns.Remark
                };
            }

            return meansVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMembershipMeansList")]
        [System.Web.Http.HttpPost]
        public List<MembershipMeansViewModel> GetMembershipMeansList()
        {
            List<MembershipMeans> means = db.MembershipMeans.ToList();
            List<MembershipMeansViewModel> meansVm = (from g in means
                                                      select new MembershipMeansViewModel
                                                      {
                                                          MembershipMeansId = g.MembershipMeansId,
                                                          MembershipMeansName = g.MembershipMeansName,
                                                          Remark = g.Remark,
                                                      }).ToList();

            return meansVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMonth")]
        [System.Web.Http.HttpPost]
        public MonthViewModel GetMonth(Month month)
        {
            MonthViewModel monthVm = new MonthViewModel();
            if (month != null)
            {
                Month mon = db.Month.Find(month.MonthId);
                monthVm = new MonthViewModel()
                {
                    MonthId = mon.MonthId,
                    MonthName = mon.MonthName,
                };
            }

            return monthVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMonthList")]
        [System.Web.Http.HttpPost]
        public List<MonthViewModel> GetMonthList()
        {
            List<Month> month = db.Month.ToList();
            List<MonthViewModel> monthVm = (from g in month
                                            select new MonthViewModel
                                            {
                                                MonthId = g.MonthId,
                                                MonthName = g.MonthName,
                                            }).ToList();

            return monthVm;
        }
        
        [System.Web.Http.Route("api/GejaKhcAPI/GetService")]
        [System.Web.Http.HttpPost]
        public ServiceViewModel GetService(Service service)
        {
            ServiceViewModel serviceVm = new ServiceViewModel();
            if (service != null)
            {
                Service serv = db.Service.Find(service.ServiceId);
                serviceVm = new ServiceViewModel()
                {
                    ServiceId = serv.ServiceId,
                    ServiceName = serv.ServiceName,
                    Remark = serv.Remark
                };
            }

            return serviceVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetServiceList")]
        [System.Web.Http.HttpPost]
        public List<ServiceViewModel> GetServiceList()
        {
            List<Service> service = db.Service.ToList();
            List<ServiceViewModel> serviceVm = (from g in service
                                                select new ServiceViewModel
                                                {
                                                    ServiceId = g.ServiceId,
                                                    ServiceName = g.ServiceName,
                                                    Remark = g.Remark,
                                                }).ToList();

            return serviceVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetService")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetService(Service service)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }

                return (new ErrorViewModel());
            }
            else
                return (new ErrorViewModel());
        }


        [System.Web.Http.Route("api/GejaKhcAPI/DeleteService")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteService(Service service)
        {
            if (service != null)
            {
                Service serv = db.Service.Find(service.ServiceId);
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetServiceType")]
        [System.Web.Http.HttpPost]
        public ServiceTypeViewModel GetServiceType(ServiceType servicetype)
        {
            ServiceTypeViewModel serviceTypeVm = new ServiceTypeViewModel();
            if (servicetype != null)
            {
                ServiceType serv = db.ServiceType.Find(servicetype.ServiceTypeId);
                serviceTypeVm = new ServiceTypeViewModel()
                {
                    ServiceTypeId = serv.ServiceTypeId,
                    ServiceTypeName = serv.ServiceTypeName,
                    Remark = serv.Remark
                };
            }

            return serviceTypeVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetServiceTypeList")]
        [System.Web.Http.HttpPost]
        public List<ServiceTypeViewModel> GetServiceTypeList()
        {
            List<ServiceType> serviceType = db.ServiceType.ToList();
            List<ServiceTypeViewModel> serviceTypeVm = (from g in serviceType
                                                        select new ServiceTypeViewModel
                                                        {
                                                            ServiceTypeId = g.ServiceTypeId,
                                                            ServiceTypeName = g.ServiceTypeName,
                                                            Remark = g.Remark,
                                                        }).ToList();

            return serviceTypeVm;
        }

        //////////////////////////
        // Member Photo
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/SetMemberPhoto")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetMemberPhoto(MemberPhoto memberPhoto)
        {
            if (memberPhoto != null)
            {
                MemberPhoto photo = db.MemberPhoto.Find(memberPhoto.MemberId);
                if (photo != null)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }
            
            return (new ErrorViewModel());
        }


        //////////////////////////
        // Address Info
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetMemberPhoto")]
        [System.Web.Http.HttpPost]
        public MemberPhotoViewModel GetMemberPhoto(MemberPhoto photo)
        {
            MemberPhotoViewModel photoVm = new MemberPhotoViewModel();
            if (photo != null)
            {
                MemberPhoto ph = db.MemberPhoto.Find(photo.MemberId);
                if (ph != null)
                {
                    photoVm.MemberId = ph.MemberId;
                    photoVm.PhotoFilePath = ph.PhotoFilePath;
                    photoVm.Photo = ph.Photo;
                    photoVm.Remark = ph.Remark;
                }
            }

            return photoVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetAddressInfo")]
        [System.Web.Http.HttpPost]
        public AddressInfoViewModel GetAddressInfo(AddressInfo addressinfo)
        {
            AddressInfoViewModel addressVm = null;

            if (addressinfo != null)
            {
                AddressInfo address = db.AddressInfo.Find(addressinfo.MemberId);
                if (address != null)
                {
                    addressVm = new AddressInfoViewModel()
                    {
                        MemberId = address.MemberId,
                        SubcityId = address.SubcityId,
                        Woreda = address.Woreda,
                        Kebele = address.Kebele,
                        HouseNo = address.HouseNo,
                        HouseOwnershipId = address.HouseOwnershipId,
                        HomePhoneNo = address.HomePhoneNo,
                        OfficePhoneNo = address.OfficePhoneNo,
                        MobilePhoneNo = address.MobilePhoneNo,
                        Email = address.Email,
                    };
                }
                else
                    addressVm = null;
            }

            return addressVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetAddressInfoList")]
        [System.Web.Http.HttpPost]
        public List<AddressInfoViewModel> GetAddressInfoList()
        {
            List<AddressInfo> addresses = db.AddressInfo.ToList();
            List<AddressInfoViewModel> addressesVm = (from g in addresses
                                                      select new AddressInfoViewModel
                                                      {
                                                          MemberId = g.MemberId,
                                                          SubcityId = g.SubcityId,
                                                          Woreda = g.Woreda,
                                                          Kebele = g.Kebele,
                                                          HouseNo = g.HouseNo,
                                                          HouseOwnershipId = g.HouseOwnershipId,
                                                          HomePhoneNo = g.HomePhoneNo,
                                                          OfficePhoneNo = g.OfficePhoneNo,
                                                          MobilePhoneNo = g.MobilePhoneNo,
                                                          Email = g.Email
                                                      }).ToList();

            return addressesVm;
        }
        
        [System.Web.Http.Route("api/GejaKhcAPI/SetAddressInfo")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetAddressInfo(AddressInfo addressInfo)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }


        //////////////////////////
        // Family Info
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetFamilyInfo")]
        [System.Web.Http.HttpPost]
        public FamilyInfoViewModel GetFamilyInfo(FamilyInfo familyinfo)
        {
            FamilyInfoViewModel familyVm = null;
            if (familyinfo != null)
            {
                FamilyInfo family = db.FamilyInfo.Find(familyinfo.MemberId);
                if (family != null)
                {
                    familyVm = new FamilyInfoViewModel()
                    {
                        MemberId = family.MemberId,
                        MaritalStatusId = family.MaritalStatusId,
                        SpouseName = family.SpouseName,
                        MarriageYear = family.MarriageYear,
                        NoOfSons = family.NoOfSons,
                        NoOfDaughters = family.NoOfDaughters,
                        NoOfSons1to5 = family.NoOfSons1to5,
                        NoOfDaughters1to5 = family.NoOfDaughters1to5,
                        NoOfSons6to12 = family.NoOfSons6to12,
                        NoOfDaughters6to12 = family.NoOfDaughters6to12,
                        NoOfSons13to20 = family.NoOfSons13to20,
                        NoOfDaughters13to20 = family.NoOfDaughters13to20,
                        NoOfSonsAbove20 = family.NoOfSonsAbove20,
                        NoOfDaughtersAbove20 = family.NoOfDaughtersAbove20,
                    };
                }
                else
                    familyVm = null;
            }

            return familyVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetFamilyInfoList")]
        [System.Web.Http.HttpPost]
        public List<FamilyInfoViewModel> GetFamilyInfoList()
        {
            List<FamilyInfo> families = db.FamilyInfo.ToList();
            List<FamilyInfoViewModel> familiesVm = (from g in families
                                                    select new FamilyInfoViewModel
                                                    {
                                                        MemberId = g.MemberId,
                                                        MaritalStatusId = g.MaritalStatusId,
                                                        SpouseName = g.SpouseName,
                                                        MarriageYear = g.MarriageYear,
                                                        NoOfSons = g.NoOfSons,
                                                        NoOfDaughters = g.NoOfDaughters,
                                                        NoOfSons1to5 = g.NoOfSons1to5,
                                                        NoOfDaughters1to5 = g.NoOfDaughters1to5,
                                                        NoOfSons6to12 = g.NoOfSons6to12,
                                                        NoOfDaughters6to12 = g.NoOfDaughters6to12,
                                                        NoOfSons13to20 = g.NoOfSons13to20,
                                                        NoOfDaughters13to20 = g.NoOfDaughters13to20,
                                                        NoOfSonsAbove20 = g.NoOfSonsAbove20,
                                                        NoOfDaughtersAbove20 = g.NoOfDaughtersAbove20
                                                    }).ToList();

            return familiesVm;
        }
        
        [System.Web.Http.Route("api/GejaKhcAPI/SetFamilyInfo")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetFamilyInfo(FamilyInfo familyInfo)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        
        //////////////////////////
        // Education and Job Info
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetEducationAndJobInfo")]
        [System.Web.Http.HttpPost]
        public EducationAndJobInfoViewModel GetEducationAndJobInfo(EducationAndJobInfo eduJob)
        {
            EducationAndJobInfoViewModel eduJobVm = null;
            if (eduJob != null)
            {
                EducationAndJobInfo edu = db.EducationAndJobInfo.Find(eduJob.MemberId);
                if (edu != null)
                {
                    eduJobVm = new EducationAndJobInfoViewModel()
                    {
                        MemberId = edu.MemberId,
                        EducationLevelId = edu.EducationLevelId,
                        FieldOfStudyId = edu.FieldOfStudyId,
                        JobId = edu.JobId,
                        JobTypeId = edu.JobTypeId,
                    };
                }
            }

            return eduJobVm;
        }


        [System.Web.Http.Route("api/GejaKhcAPI/GetEducationAndJobInfoList")]
        [System.Web.Http.HttpPost]
        public List<EducationAndJobInfoViewModel> GetEducationAndJobInfoList()
        {
            List<EducationAndJobInfo> eduJob = db.EducationAndJobInfo.ToList();
            List<EducationAndJobInfoViewModel> eduJobVm = (from g in eduJob
                                                           select new EducationAndJobInfoViewModel
                                                           {
                                                               MemberId = g.MemberId,
                                                               EducationLevelId = g.EducationLevelId,
                                                               FieldOfStudyId = g.FieldOfStudyId,
                                                               JobId = g.JobId,
                                                               JobTypeId = g.JobTypeId,
                                                           }).ToList();

            return eduJobVm;
        }


        [System.Web.Http.Route("api/GejaKhcAPI/SetEducationAndJobInfo")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel SetEducationAndJobInfo(EducationAndJobInfo educAndJobInfo)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                            ErrorViewModel err = new ErrorViewModel()
                            {
                                ExceptionNumber = sqlEx.Number,
                            };
                            return err;
                        }
                    }
            }

            return (new ErrorViewModel());
        }

        //////////////////////////
        // Member Service
        //////////////////////////

        [System.Web.Http.Route("api/GejaKhcAPI/GetMemberServiceList")]
        [System.Web.Http.HttpPost]
        public List<MemberServiceViewModel> GetMemberServiceList(MemberService service)
        {
            List<MemberServiceViewModel> serviceVm = new List<MemberServiceViewModel>();
            if (service != null)
            {
                List<MemberService> serv = db.MemberService.Where(m => m.MemberId == service.MemberId).ToList();
                serviceVm = (from g in serv 
                             select new MemberServiceViewModel
                             {
                                 Id = g.Id,
                                 MemberId = g.MemberId,
                                 ServiceTypeId = g.ServiceTypeId,
                                 ServiceId = g.ServiceId
                             }).ToList();
            }

            return serviceVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetAllMemberServices")]
        [System.Web.Http.HttpPost]
        public List<MemberServiceViewModel> GetAllMemberServices()
        {
            List<MemberService> memberservices = db.MemberService.ToList();
            List<MemberServiceViewModel> memberServicesVm = (from g in memberservices
                                                             select new MemberServiceViewModel
                                                             {
                                                                 Id = g.Id,
                                                                 MemberId = g.MemberId,
                                                                 ServiceTypeId = g.ServiceTypeId,
                                                                 ServiceId = g.ServiceId
                                                             }).ToList();

            return memberServicesVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMemberServiceById")]
        [System.Web.Http.HttpPost]
        public MemberServiceViewModel GetMemberServiceById(MemberService memberservice)
        {
            MemberServiceViewModel memserviceVm = null;
            if (memberservice != null)
            {
                MemberService serv = db.MemberService.Find(memberservice.Id);
                if (serv != null)
                {
                    memserviceVm = new MemberServiceViewModel()
                    {
                        Id = serv.Id,
                        MemberId = serv.MemberId,
                        ServiceTypeId = serv.ServiceTypeId,
                        ServiceId = serv.ServiceId
                    };
                }
            }

            return memserviceVm;
        }

        [System.Web.Http.Route("api/GejaKhcAPI/SetMemberService")]
        [System.Web.Http.HttpPost]
        //public MemberService SetMemberService(MemberService memberService)
        public ErrorViewModel SetMemberService(MemberService memberService)
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }
            
            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/DeleteMemberService")]
        [System.Web.Http.HttpPost]
        public ErrorViewModel DeleteMemberService(MemberService memberService)
        {
            if (memberService != null)
            {
                MemberService memserv = db.MemberService.Find(memberService.Id);
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
                        ErrorViewModel err = new ErrorViewModel()
                        {
                            ExceptionNumber = sqlEx.Number,
                        };
                        return err;
                    }
                }
            }

            return (new ErrorViewModel());
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetEducationLevelByGroup")]
        [System.Web.Http.HttpPost]
        public List<EducationLevelByGroup> GetEducationLevelByGroup()
        {
            return db.EducationLevelByGroup.OrderBy(b => b.GroupId).ToList();
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMaritalStatusByGroup")]
        [System.Web.Http.HttpPost]
        public List<MaritalStatusByGroup> GetMaritalStatusByGroup()
        {
            return db.MaritalStatusByGroup.OrderBy(b => b.GroupId).ToList();
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetMembershipMeansByGroup")]
        [System.Web.Http.HttpPost]
        public List<MembershipMeansByGroup> GetMembershipMeansByGroup()
        {
            return db.MembershipMeansByGroup.OrderBy(b => b.GroupId).ToList();
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetSubcityByGroup")]
        [System.Web.Http.HttpPost]
        public List<SubcityByGroup> GetSubcityByGroup()
        {
            return db.SubcityByGroup.OrderBy(b => b.GroupId).ToList();
        }

        [System.Web.Http.Route("api/GejaKhcAPI/GetGenderByGroup")]
        [System.Web.Http.HttpPost]
        public List<GenderByGroup> GetGenderByGroup()
        {
            return db.GenderByGroup.OrderBy(b => b.GroupId).ToList();
        }

        // I added this temporary function for running simple queries
        [System.Web.Http.Route("api/GejaKhcAPI/RunQuery")]
        [System.Web.Http.HttpPost]
        public void RunQuery()
        {
            db.Database.ExecuteSqlCommand("ALTER TABLE Group2 ADD GroupName2 nvarchar(50) collate SQL_Latin1_General_CP437_BIN");

            db.SaveChanges();
            
        }

        [System.Web.Http.Route("api/GejaKhcAPI/UploadImage")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage UploadImage()
        {
            HttpPostedFile postedImageFile;

            try
            {
                //Create the Directory if it does not exist
                //string path = HttpContext.Current.Server.MapPath("~/Photos/");
                string path = HttpContext.Current.Server.MapPath("~/Photos/");
                //string path = "http://192.168.43.17:777/test/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Fetch the image File
                postedImageFile = HttpContext.Current.Request.Files[0];

                //Save the File
                postedImageFile.SaveAs(path + postedImageFile.FileName);
            }
            catch(Exception)
            {
                // Send "Internal Server Error" Response to Client
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            // Send "OK" Response to Client
            return Request.CreateResponse(HttpStatusCode.OK, postedImageFile.FileName);
        }
    }
}
