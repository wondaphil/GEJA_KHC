using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class ReportGraphViewModel
    {
        List<Member> MemberList;
        List<Group> GroupList;
        List<AddressInfo> AddressInfoList;
        List<EducationAndJobInfo> EducationAndJobInfoList;
        List<FamilyInfo> FamilyInfoList;
        List<MemberService> MemberServiceList;
        List<Subcity> SubcityList;
        List<Service> ServiceList;
        List<MembershipMeans> MembershipMeansList;
        List<MaritalStatus> MaritalStatusList;
        List<JobType> JobTypeList;
        List<Job> JobList;
        List<Gender> GenderList;
        List<HouseOwnershipType> HouseOwnershipTypeList;
        List<FieldOfStudy> FieldOfStudyList;
        List<EducationLevel> EducationLevelList;


        public ReportGraphViewModel()
        {
            try
            { 
                MemberList = DbViewModel.GetAllMembers().Result.ToList();
                GroupList = DbViewModel.GetGroupList().Result.ToList();
                AddressInfoList = DbViewModel.GetAddressInfoList().Result.ToList();
                EducationAndJobInfoList = DbViewModel.GetEducationAndJobInfoList().Result.ToList();
                FamilyInfoList = DbViewModel.GetFamilyInfoList().Result.ToList();
                MemberServiceList = DbViewModel.GetAllMemberServices().Result.ToList();
                SubcityList = DbViewModel.GetSubcityList().Result.ToList();
                ServiceList = DbViewModel.GetServiceList().Result.ToList();
                MembershipMeansList = DbViewModel.GetMembershipMeansList().Result.ToList();
                MaritalStatusList = DbViewModel.GetMaritalStatusList().Result.ToList();
                JobTypeList = DbViewModel.GetJobTypeList().Result.ToList();
                JobList = DbViewModel.GetJobList().Result.ToList();
                GenderList = DbViewModel.GetGenderList().Result.ToList();
                HouseOwnershipTypeList = DbViewModel.GetHouseOwnershipTypeList().Result.ToList();
                FieldOfStudyList = DbViewModel.GetFieldOfStudyList().Result.ToList();
                EducationLevelList = DbViewModel.GetEducationLevelList().Result.ToList();
            }
            catch (Exception err)
            {
                App.Current.MainPage.DisplayAlert("Error", err.Message, "Ok");
            }
        }
        
        public List<MemberCountViewModel> GetMemberCountByGroup(ref int membercount)
        {
            // Get member count by group
            var noofmembersbygroup = (from mem in MemberList
                                        join grp in GroupList on mem.GroupId equals grp.GroupId
                                        group grp by new { grp } into g
                                        select new MemberCountViewModel
                                        {
                                            Group = g.Key.grp.GroupName,
                                            MemberCount = g.Count()
                                        }).ToList();

            foreach (var item in noofmembersbygroup)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbygroup;
        }
        
        public List<MemberCountViewModel> GetMemberCountByGender(ref int membercount)
        {
            // Get member count by gender
            var noofmembersbygender = (from mem in MemberList
                                      join gen in GenderList on mem.GenderId equals gen.GenderId
                                      group gen by new { gen } into g
                                      select new MemberCountViewModel
                                      {
                                          Gender = g.Key.gen.GenderName,
                                          MemberCount = g.Count()
                                      }).ToList();

            foreach (var item in noofmembersbygender)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbygender;
        }

        public List<MemberCountViewModel> GetMemberCountByEducationLevel(ref int membercount)
        {
            // Get member count by education level
            var noofmembersbyeducation = (from mem in EducationAndJobInfoList
                                          join gen in EducationLevelList on mem.EducationLevelId equals gen.EducationLevelId
                                          group gen by new { gen } into g
                                          select new MemberCountViewModel
                                          {
                                              EducationLevel = g.Key.gen.EducationLevelName,
                                              MemberCount = g.Count()
                                          }).ToList();

            foreach (var item in noofmembersbyeducation)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbyeducation;
        }

        public List<MemberCountViewModel> GetMemberCountByMaritalStatus(ref int membercount)
        {
            // Get member count by maritalstatus level
            var noofmembersbymaritalstatus = (from mem in FamilyInfoList
                                              join gen in MaritalStatusList on mem.MaritalStatusId equals gen.MaritalStatusId
                                              group gen by new { gen } into g
                                              select new MemberCountViewModel
                                              {
                                                  MaritalStatus = g.Key.gen.MaritalStatusName,
                                                  MemberCount = g.Count()
                                              }).ToList();

            foreach (var item in noofmembersbymaritalstatus)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbymaritalstatus;
        }

        public List<MemberCountViewModel> GetMemberCountByMembershipMeans(ref int membercount)
        {
            // Get member count by maritalmeans level
            var noofmembersbymaritalmeans = (from mem in MemberList
                                             join gen in MembershipMeansList on mem.MembershipMeansId equals gen.MembershipMeansId
                                             group gen by new { gen } into g
                                             select new MemberCountViewModel
                                             {
                                                 MembershipMeans = g.Key.gen.MembershipMeansName,
                                                 MemberCount = g.Count()
                                             }).ToList();

            foreach (var item in noofmembersbymaritalmeans)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbymaritalmeans;
        }

        public List<MemberCountViewModel> GetMemberCountByMembershipYear(ref int membercount)
        {
            // Get member count by maritalyear level
            /*
            var noofmembersbymaritalyear = (from mem in MemberList
                                            group mem by new { mem } into g
                                            select new MemberCountViewModel
                                            {
                                                MembershipYear = g.Key.mem.MembershipYear.ToString(),
                                                MemberCount = g.Count()
                                            }).ToList();
            */
            var noofmembersbymaritalyear = (from mem in MemberList
                                           group mem by mem.MembershipYear into g
                                           select new MemberCountViewModel
                                           {
                                               MembershipYear = g.Key != null ? g.Key.Value.ToString() : "",
                                               MemberCount = g.Count()
                                           }).ToList();

            foreach (var item in noofmembersbymaritalyear)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbymaritalyear;
        }

        public List<MemberCountViewModel> GetMemberCountBySubcity(ref int membercount)
        {
            // Get member count by maritalmeans level
            var noofmembersbymaritalsubcity = (from mem in AddressInfoList
                                               join gen in SubcityList on mem.SubcityId equals gen.SubcityId
                                               group gen by new { gen } into g
                                               select new MemberCountViewModel
                                               {
                                                   Subcity = g.Key.gen.SubcityName,
                                                   MemberCount = g.Count()
                                               }).ToList();

            foreach (var item in noofmembersbymaritalsubcity)
            {
                membercount += item.MemberCount;
            }

            return noofmembersbymaritalsubcity;
        }

        //    public List<MemberCulvertCountViewModel> GetMemberCountByConstructionYear(ref int membercount)
        //    {
        //        List<ConstructionYear> constrYear = db.Table<ConstructionYear>().ToList();

        //        var memberRecord = from gen in MemberGeneralInfoList
        //                           from con in constrYear
        //                           where (gen.ReplacedYear >= con.FromYear) && (gen.ReplacedYear <= con.ToYear)
        //                           select new
        //                           {
        //                               genInfo = gen,
        //                               constrYear = con
        //                           };

        //        var noofmembersbyconstructionyear = (from c in memberRecord
        //                                             group c by new { c.constrYear } into g
        //                                             select new MemberCulvertCountViewModel
        //                                             {
        //                                                 Id = g.Key.constrYear.CategoryId,
        //                                                 ConstructionYear = g.Key.constrYear.ConstructionYears,
        //                                                 MemberCount = g.Count()
        //                                             }).OrderBy(s => s.ConstructionYear).ToList();

        //        foreach (var item in noofmembersbyconstructionyear)
        //            membercount += item.MemberCount;

        //        return noofmembersbyconstructionyear;
        //    }

        //    public List<MemberCulvertCountViewModel> GetMemberCountByMemberLength(ref int membercount)
        //    {
        //        List<MemberLength> memgLegth = db.Table<MemberLength>().ToList();

        //        var memberRecord = (from gen in MemberGeneralInfoList
        //                            from len in memgLegth
        //                            where (gen.MemberLength >= len.FromLength) && (gen.MemberLength < len.ToLength)
        //                            select new
        //                            {
        //                                genInfo = gen,
        //                                memgLegth = len
        //                            }).OrderBy(s => s.memgLegth.MemberLengthId);

        //        var noofmembersbymemberlength = (from c in memberRecord
        //                                         group c by new { c.memgLegth } into g
        //                                         select new MemberCulvertCountViewModel
        //                                         {
        //                                             Id = g.Key.memgLegth.MemberLengthId,
        //                                             MemberLength = g.Key.memgLegth.MemberLengthName,
        //                                             MemberCount = g.Count()
        //                                         }).ToList();


        //        foreach (var item in noofmembersbymemberlength)
        //            membercount += item.MemberCount;

        //        return noofmembersbymemberlength;
        //    }

        //    public List<MemberCulvertByTypeViewModel> GetMemberCountByMemberType(ref int membercount)
        //    {
        //        List<MemberType> memberTypes = db.Table<MemberType>().ToList();

        //        var memberRecord = from mem in MemberList
        //                           join sup in SuperStructureList on mem.MemberId equals sup.MemberId into table1
        //                           from sup in table1.ToList()
        //                           join memtyp in memberTypes on sup.MemberTypeId equals memtyp.MemberTypeId into table2
        //                           from memtyp in table2.ToList()
        //                           select new
        //                           {
        //                               members = mem,
        //                               membertypes = memtyp
        //                           };

        //        var noofmembersbymembertype = (from b in memberRecord
        //                                       group b by new { b.membertypes } into g
        //                                       select new MemberCulvertByTypeViewModel
        //                                       {
        //                                           TypeId = g.Key.membertypes.MemberTypeId,
        //                                           TypeName = g.Key.membertypes.MemberTypeName,
        //                                           Count = g.Count()
        //                                       }).OrderBy(b => b.TypeId).ToList();

        //        foreach (var item in noofmembersbymembertype)
        //            membercount += item.Count;

        //        return noofmembersbymembertype;
        //    }

        //    public List<MemberCulvertByTypeViewModel> GetCulvertCountByMemberType(ref int culvertcount)
        //    {
        //        List<CulvertType> culvertTypes = db.Table<CulvertType>().ToList();

        //        var culvertRecord = from cul in CulvertList
        //                            join str in CulvertStructureList on cul.CulvertId equals str.CulvertId into table1
        //                            from str in table1.ToList()
        //                            join memtyp in culvertTypes on str.CulvertTypeId equals memtyp.CulvertTypeId into table2
        //                            from memtyp in table2.ToList()
        //                            select new
        //                            {
        //                                culverts = cul,
        //                                culverttypes = memtyp
        //                            };

        //        var noofculvertsbyculverttype = (from c in culvertRecord
        //                                         group c by new { c.culverttypes } into g
        //                                         select new MemberCulvertByTypeViewModel
        //                                         {
        //                                             TypeId = g.Key.culverttypes.CulvertTypeId,
        //                                             TypeName = g.Key.culverttypes.CulvertTypeName,
        //                                             Count = g.Count()
        //                                         }).OrderBy(c => c.TypeId).ToList();

        //        foreach (var item in noofculvertsbyculverttype)
        //            culvertcount += item.Count;

        //        return noofculvertsbyculverttype;
        //    }

        //    public List<MemberCulvertCountViewModel> GetInspectedMemberCountByInspectionYear()
        //    {
        //        var noofmembersbyinspctionyear = (from dmg in db.Table<ResultInspMajor>()
        //                                          group dmg by new { dmg.InspectionYear } into g
        //                                          select new MemberCulvertCountViewModel
        //                                          {
        //                                              InspectionYear = g.Key.InspectionYear,
        //                                              MemberCount = g.Count()
        //                                          }).OrderByDescending(s => s.InspectionYear).ToList();

        //        return noofmembersbyinspctionyear;
        //    }

        //    public List<MemberCulvertCountViewModel> GetInspectedCulvertCountByInspectionYear(ref int culvertcount)
        //    {
        //        // Get no. of inspected culverts by inspection year by taking the 'year' part from 'InspectionDate'
        //        var noofculvertsbyinspctionyear = (from dmg in db.Table<ResultInspCulvert>()
        //                                          group dmg by new { InspectionYear = ((DateTime)dmg.InspectionDate).Year } into g
        //                                          select new MemberCulvertCountViewModel
        //                                          {
        //                                              InspectionYear = g.Key.InspectionYear,
        //                                              CulvertCount = g.Count(),
        //                                          }).ToList();

        //        // Now group inspection years based on Major Inspection year
        //        // e.g. If InspectionYear is 2021, then MajorInspectionYear becomes 2020 (which coverts year from 2018 - 2021)
        //        List<MemberCulvertCountViewModel> noofculvertsbymajorinspctionyear = new List<MemberCulvertCountViewModel>();

        //        foreach (var maj in MajorInspectionYearList)
        //        {
        //            int totalCulverts = 0;
        //            foreach (var cul in noofculvertsbyinspctionyear)
        //            {
        //                if ((cul.InspectionYear >= maj.StartYear) && (cul.InspectionYear <= maj.EndYear))
        //                    totalCulverts += cul.CulvertCount;
        //            }
        //            noofculvertsbymajorinspctionyear.Add(new MemberCulvertCountViewModel()
        //            {
        //                InspectionYear = maj.InspectionYear,
        //                CulvertCount = totalCulverts
        //            });
        //        }

        //        foreach (var item in noofculvertsbymajorinspctionyear)
        //            culvertcount += item.CulvertCount;

        //        return noofculvertsbymajorinspctionyear;
        //    }

        //    public List<MemberCulvertCountViewModel> GetMemberCountByServiceCondition(ref int membercount, int inspectionyear)
        //    {
        //        // Get all member inspection results in the given inspection year
        //        var memberRecord = from res in MemberInspectionList
        //                           where res.InspectionYear == inspectionyear
        //                           select new
        //                           {
        //                               memberid = res.MemberId,
        //                               condition = res.DmgPerc
        //                           };

        //        List<DamageConditionRange> dmgRange = db.Table<DamageConditionRange>().ToList();

        //        var membersbydmgperc = (from c in memberRecord
        //                                select new
        //                                {
        //                                    memberid = c.memberid,
        //                                    Condition =
        //                                        (
        //                                            c.condition >= dmgRange[0].ValueFrom && c.condition < dmgRange[0].ValueTo ? "Good" :
        //                                            c.condition >= dmgRange[1].ValueFrom && c.condition < dmgRange[1].ValueTo ? "Fair" :
        //                                            c.condition >= dmgRange[2].ValueFrom && c.condition < dmgRange[2].ValueTo ? "Bad" : ""
        //                                        ),
        //                                }).ToList();


        //        // Get No. of members vs member condition ("Good", "Fair" and "Bad")
        //        var noofmembersbycondition = (from c in membersbydmgperc
        //                                      group c by new { c.Condition } into d
        //                                      select new MemberCulvertCountViewModel
        //                                      {
        //                                          Condition = d.Key.Condition,
        //                                          MemberCount = d.Count(),
        //                                      }).OrderByDescending(s => s.Condition).ToList();

        //        foreach (var item in noofmembersbycondition)
        //            membercount += item.MemberCount;

        //        return noofmembersbycondition;
        //    }

        //    public List<MemberCulvertCountViewModel> GetInspectedMemberCountByDistrict(ref int membercount, int inspectionyear)
        //    {
        //        var inspBrgByDistrict = (from mem in MemberList
        //                                  join seg in SegmentList on mem.SegmentId equals seg.SegmentId into table1
        //                                  from seg in table1.ToList()
        //                                  join sec in SectionList on seg.SectionId equals sec.SectionId into table2
        //                                  from sec in table2.ToList()
        //                                  join dis in DistrictList on sec.DistrictId equals dis.DistrictId into table3
        //                                  from dis in table3.ToList()
        //                                  join insp in MemberInspectionList on mem.MemberId equals insp.MemberId into table4
        //                                  from insp in table4.ToList()
        //                                  where insp.InspectionYear == inspectionyear
        //                                  group dis by new { dis.DistrictName, dis.DistrictNo } into g
        //                                  select new MemberCulvertCountViewModel
        //                                  {
        //                                      DistrictNo = g.Key.DistrictNo,
        //                                      DistrictName = g.Key.DistrictName,
        //                                      MemberCount = g.Count()
        //                                  }).OrderBy(d => d.DistrictNo).ToList();

        //        foreach (var item in inspBrgByDistrict)
        //            membercount += item.MemberCount;

        //        return inspBrgByDistrict;
        //    }

        //    public int GetMajorInspectionYear(int inspectionyear)
        //    {
        //        foreach (var maj in MajorInspectionYearList)
        //        {
        //            if ((inspectionyear >= maj.StartYear) && (inspectionyear <= maj.EndYear))
        //                return maj.InspectionYear;
        //        }

        //        return 0;
        //    }

        //    public List<MemberCulvertCountViewModel> GetInspectedCulvertCountByDistrict(ref int culvertcount, int inspectionyear)
        //    {
        //        var inspCulByDistrict = (from cul in CulvertList
        //                                 join seg in SegmentList on cul.SegmentId equals seg.SegmentId into table1
        //                                 from seg in table1.ToList()
        //                                 join sec in SectionList on seg.SectionId equals sec.SectionId into table2
        //                                 from sec in table2.ToList()
        //                                 join dis in DistrictList on sec.DistrictId equals dis.DistrictId into table3
        //                                 from dis in table3.ToList()
        //                                 join insp in CulvertInspectionList on cul.CulvertId equals insp.CulvertId into table4
        //                                 from insp in table4.ToList()
        //                                 where (GetMajorInspectionYear(((DateTime)insp.InspectionDate).Year) == inspectionyear)
        //                                 group dis by new { dis.DistrictName, dis.DistrictNo } into g
        //                                 select new MemberCulvertCountViewModel
        //                                 {
        //                                     DistrictNo = g.Key.DistrictNo,
        //                                     DistrictName = g.Key.DistrictName,
        //                                     CulvertCount = g.Count()
        //                                 }).OrderBy(d => d.DistrictNo).ToList();


        //        foreach (var item in inspCulByDistrict)
        //            culvertcount += item.CulvertCount;

        //        return inspCulByDistrict;
        //    }
    }
}
