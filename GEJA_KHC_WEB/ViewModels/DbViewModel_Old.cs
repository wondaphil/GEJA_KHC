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


namespace GEJA_KHC_WEB.ViewModels
{
    public class DbViewModel_Old
    {
        /// <summary>
        /// URL of the web api server read from a text file
        /// </summary>
        static string webApiServerUrl = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/webapi_address.txt"));

        /// <summary>
        /// The URL for the Geja KHC Web API
        /// </summary>
        //static string gkhcApiUrl = webApiServerUrl + "/api/GejaKhcAPI";
        //static string gkhcApiUrl = webApiServerUrl + "/api/GejaKhcAPI";
        static string gkhcApiUrl = webApiServerUrl + "api/GejaKhcAPI";


        /// <summary>
        /// The URL for the Drop Down List Web API
        /// </summary>
        //static string ddlApiUrl = webApiServerUrl + "/api/DropDownListAPI";
        static string ddlApiUrl = webApiServerUrl + "api/DropDownListAPI"; 

        /// <summary>
        /// Returns a Group object from Database via webapi
        /// </summary>
        /// <param name="groupid">The primary key of the required Group 
        public static Group GetGroup(int? groupid)
        {
            var input = new
            {
                GroupId = groupid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetGroup", inputJson);
            Group group = (new JavaScriptSerializer()).Deserialize<Group>(json);

            return group;
        }

        /// <summary>
        /// Returns a list of all Group objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static List<Group> GetGroupList()
        {
            var input = new
            {
                GroupId = "",
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetGroupList", inputJson);
            List<Group> groups = (new JavaScriptSerializer()).Deserialize<List<Group>>(json);

            return groups;
        }

        //public static List<Group> GetGroupList()
        //{
        //    var input = new 
        //    { 
        //        GroupId = "" 
        //    };
        //    string inputJson = JsonConvert.SerializeObject(input);
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;

        //    string json = client.UploadString(gkhcApiUrl + "/GetGroupList", inputJson);
        //    List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(json);

        //    return groups;
        //}

        /// <summary>
        /// Set a Group object from Database via web api
        /// </summary>
        /// <param name="group">The group to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetGroup(Group group)
        {
            var input = new
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                Pastor = group.Pastor,
                Remark = group.Remark
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetGroup", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Set an Group object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Group to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteGroup(int id)
        {
            var input = new
            {
                GroupId = id,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/DeleteGroup", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a Member object from Database via web api
        /// </summary>
        /// <param name="memberid">The primary key of the required Member</param>
        /// <returns></returns>
        public static Member GetMember(string memberid)
        {
            var input = new
            {
                MemberId = memberid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMember", inputJson);
            Member member = (new JavaScriptSerializer()).Deserialize<Member>(json);

            return member;
        }

        /// <summary>
        /// Set a Member object from Database via web api
        /// </summary>
        /// <param name="Member">The member to be set</param>
        /// <returns>he status id of the operation</returns>
        public static int SetMember(Member member)
        {
            var input = new
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
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetMember", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        public static List<Member> GetAllMembers()
        {
            var input = new
            {
                GroupId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetAllMembers", inputJson);
            List<Member> members = (new JavaScriptSerializer()).Deserialize<List<Member>>(json);
            return members;
        }

        /// <summary>
        /// Returns a list of Member objects within a given Group from Database via web api
        /// </summary>
        /// <param name="groupid">The primary key of the Group that encompasses the Members</param>
        /// <returns></returns>
        public static List<Member> GetMemberList(int groupid)
        {
            var input = new
            {
                GroupId = groupid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMemberList", inputJson);
            List<Member> members = (new JavaScriptSerializer()).Deserialize<List<Member>>(json);
            return members;
        }
        
        /// <summary>
        /// Returns a drop down list of all Groups from Database via web api
        /// </summary>
        /// <returns></returns>
        public static List<DropDownViewModel> GetGroupsDDL()
        {
            var input = new
            {
                GroupId = "",
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(ddlApiUrl + "/GetGroupsDDL", inputJson);
            List<DropDownViewModel> groups = new JavaScriptSerializer().Deserialize<List<DropDownViewModel>>(json);

            return groups;
        }


        /// <summary>
        /// Returns a drop down list of Members within a given Group from Database via web api
        /// </summary>
        /// <param name="grouplist">The primary key of the Group that encompasses the Members</param>
        /// <returns></returns>
        public static List<DropDownViewModel> GetMembersDDL(int? groupid)
        {
            var input = new
            {
                GroupId = groupid.ToString(),
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(ddlApiUrl + "/GetMembersDDL", inputJson);
            List<DropDownViewModel> members = (new JavaScriptSerializer()).Deserialize<List<DropDownViewModel>>(json);

            return members.OrderBy(m => m.Text).ToList(); // order by member name
        }

        /// <summary>
        /// Returns a drop down list of all Genders from Database via web api
        /// </summary>
        /// <returns></returns>
        public static List<DropDownViewModel> GetGendersDDL()
        {
            var input = new
            {
                GenderId = "",
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(ddlApiUrl + "/GetGendersDDL", inputJson);
            List<DropDownViewModel> genders = new JavaScriptSerializer().Deserialize<List<DropDownViewModel>>(json);

            return genders;
        }

        /// <summary>
        /// Returns a Subcity object from Database via webapi
        /// </summary>
        /// <param name="subcityid">The primary key of the required Subcity 
        public static Subcity GetSubcity(int subcityid)
        {
            var input = new
            {
                SubcityId = subcityid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetSubcity", inputJson);
            Subcity subcity = (new JavaScriptSerializer()).Deserialize<Subcity>(json);

            return subcity;
        }

        /// <summary>
        /// Returns a EducationLevel object from Database via webapi
        /// </summary>
        /// <param name="educationlevelid">The primary key of the required EducationLevel 
        public static EducationLevel GetEducationLevel(int educationlevelid)
        {
            var input = new
            {
                EducationLevelId = educationlevelid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetEducationLevel", inputJson);
            EducationLevel educationlevel = (new JavaScriptSerializer()).Deserialize<EducationLevel>(json);

            return educationlevel;
        }

        /// <summary>
        /// Returns a HouseOwnershipType object from Database via webapi
        /// </summary>
        /// <param name="houseownershiptypeid">The primary key of the required HouseOwnershipType 
        public static HouseOwnershipType GetHouseOwnershipType(int houseownershiptypeid)
        {
            var input = new
            {
                HouseOwnershipTypeId = houseownershiptypeid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetHouseOwnershipType", inputJson);
            HouseOwnershipType houseownershiptype = (new JavaScriptSerializer()).Deserialize<HouseOwnershipType>(json);

            return houseownershiptype;
        }

        /// <summary>
        /// Returns a JobType object from Database via webapi
        /// </summary>
        /// <param name="jobtypeid">The primary key of the required JobType 
        public static JobType GetJobType(int jobtypeid)
        {
            var input = new
            {
                JobTypeId = jobtypeid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetJobType", inputJson);
            JobType jobtype = (new JavaScriptSerializer()).Deserialize<JobType>(json);

            return jobtype;
        }

        /// <summary>
        /// Returns a MaritalStatus object from Database via webapi
        /// </summary>
        /// <param name="maritalstatusid">The primary key of the required MaritalStatus 
        public static MaritalStatus GetMaritalStatus(int maritalstatusid)
        {
            var input = new
            {
                MaritalStatusId = maritalstatusid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMaritalStatus", inputJson);
            MaritalStatus maritalstatus = (new JavaScriptSerializer()).Deserialize<MaritalStatus>(json);

            return maritalstatus;
        }

        /// <summary>
        /// Returns a MembershipMeans object from Database via webapi
        /// </summary>
        /// <param name="membershipmeansid">The primary key of the required MembershipMeans 
        public static MembershipMeans GetMembershipMeans(int membershipmeansid)
        {
            var input = new
            {
                MembershipMeansId = membershipmeansid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMembershipMeans", inputJson);
            MembershipMeans membershipmeans = (new JavaScriptSerializer()).Deserialize<MembershipMeans>(json);

            return membershipmeans;
        }

        /// <summary>
        /// Returns a Month object from Database via webapi
        /// </summary>
        /// <param name="monthid">The primary key of the required Month 
        public static Month GetMonth(int monthid)
        {
            var input = new
            {
                MonthId = monthid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMonth", inputJson);
            Month month = (new JavaScriptSerializer()).Deserialize<Month>(json);

            return month;
        }

        /// <summary>
        /// Returns a Service object from Database via webapi
        /// </summary>
        /// <param name="serviceid">The primary key of the required Service 
        public static Service GetService(int serviceid)
        {
            var input = new
            {
                ServiceId = serviceid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetService", inputJson);
            Service service = (new JavaScriptSerializer()).Deserialize<Service>(json);

            return service;
        }

        // <summary>
        /// Set a Service object from Database via web api
        /// </summary>
        /// <param name="Service">The service to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetService(Service service)
        {
            var input = new
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Remark = service.Remark
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetService", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Set an Service object from Database via web api
        /// </summary>
        /// <param name="Service">The Service to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteService(int id)
        {
            var input = new
            {
                ServiceId = id,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/DeleteService", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a ServiceType object from Database via webapi
        /// </summary>
        /// <param name="serviceid">The primary key of the required ServiceType 
        public static ServiceType GetServiceType(int serviceid)
        {
            var input = new
            {
                ServiceTypeId = serviceid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetServiceType", inputJson);
            ServiceType servicetype = (new JavaScriptSerializer()).Deserialize<ServiceType>(json);

            return servicetype;
        }

        /// <summary>
        /// Returns a MemberPhoto object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required MemberPhoto 
        //public static MemberPhoto GetMemberPhoto(string memberid)
        //{
        //    var input = new
        //    {
        //        MemberId = memberid.ToString()
        //    };
        //    string inputJson = (new JavaScriptSerializer()).Serialize(input);
        //    WebClient client = new WebClient();
        //    //client.Headers["Content-type"] = "application/json";
        //    client.Headers["Content-type"] = "application/octet-stream";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(gkhcApiUrl + "/GetMemberPhoto", inputJson);
        //    MemberPhoto memberPhoto = (new JavaScriptSerializer()).Deserialize<MemberPhoto>(json);

        //    return memberPhoto;
        //}

        /// <summary>
        /// Returns a MemberPhoto object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<MemberPhoto> GetMemberPhoto(string memberid)
        {
            var input = new { MemberId = memberid };
            string inputJson = JsonConvert.SerializeObject(input);
            var photo = new MemberPhoto();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(gkhcApiUrl + "/GetMemberPhoto") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(photo);
            }

            if (responseMessage.IsSuccessStatusCode)
                photo = await responseMessage.Content.ReadFromJsonAsync<MemberPhoto>();

            return await Task.FromResult(photo);
        }

        /// <summary>
        /// Set an MemberPhoto object from Database via web api
        /// </summary>
        /// <param name="memberPhoto">The memberPhoto to be set</param>
        /// <returns>The newly set memberPhoto</returns>
        public static MemberPhoto SetMemberPhoto(MemberPhoto memberPhoto)
        {
            var input = new
            {
                MemberId = memberPhoto.MemberId,
                PhotoFilePath = memberPhoto.PhotoFilePath,
                Photo = memberPhoto.Photo,
                Remark = memberPhoto.Remark
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            string inputJson = serializer.Serialize(input);
            
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            
            string json = client.UploadString(gkhcApiUrl + "/SetMemberPhoto", inputJson);
            
            MemberPhoto mem = serializer.Deserialize<MemberPhoto>(json);

            return mem;
        }
        //public static async Task<int> SetMemberPhoto(MemberPhoto photo)
        //{
        //    var input = new
        //    {
        //        MemberId = photo.MemberId,
        //        PhotoFilePath = photo.PhotoFilePath,
        //        Photo = photo.Photo,
        //        Remark = photo.Remark
        //    };
        //    string inputJson = JsonConvert.SerializeObject(input);
        //    ErrorViewModel err = new ErrorViewModel();

        //    HttpClient client = new HttpClient() { BaseAddress = new Uri(gkhcApiUrl + "/SetMemberPhoto") };
        //    HttpResponseMessage responseMessage = new HttpResponseMessage();
        //    var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
        //    try
        //    {
        //        responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
        //    }
        //    catch (Exception)
        //    {
        //        err = await Task.FromResult(new ErrorViewModel());
        //        return err.ExceptionNumber;
        //    }

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
        //        return err.ExceptionNumber;
        //    }

        //    err = await Task.FromResult(err);
        //    return err.ExceptionNumber;
        //}

        /// <summary>
        /// Returns a AddressInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required AddressInfo 
        public static AddressInfo GetAddressInfo(string memberid)
        {
            var input = new
            {
                MemberId = memberid.ToString()
            };
            //JsonSerializerSettings jss = new JsonSerializerSettings();
            //jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //jss.NullValueHandling = NullValueHandling.Ignore;

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetAddressInfo", inputJson);
            AddressInfo addressinfo = (new JavaScriptSerializer()).Deserialize<AddressInfo>(json);

            return addressinfo;
        }


        //public static AddressInfo GetAddressInfo(string memberid)
        //{
        //    var input = new
        //    {
        //        MemberId = memberid.ToString()
        //    };

        //    //JsonSerializerSettings jss = new JsonSerializerSettings();
        //    //jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        //    string inputJson = JsonConvert.SerializeObject(input, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(gkhcApiUrl + "/GetAddressInfo", inputJson);
        //    //AddressInfo addressinfo = (new JavaScriptSerializer()).Deserialize<AddressInfo>(json);
        //    AddressInfo addressinfo = JsonConvert.DeserializeObject<AddressInfo>(json);
        //    return addressinfo;
        //}



        /// <summary>
        /// Returns a list of AddressInfo objects from Database via webapi
        /// </summary>
        public static List<AddressInfo> GetAddressInfoList()
        {
            var input = new
            {
                MemberId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetAddressInfoList", inputJson);
            List<AddressInfo> addressInfoList = (new JavaScriptSerializer()).Deserialize<List<AddressInfo>>(json);

            return addressInfoList;
        }
        
        /// <summary>
         /// Set an AddressInfo object from Database via web api
         /// </summary>
         /// <param name="AddressInfo">The addressInfo to be set</param>
         /// <returns>The newly set addressInfo</returns>
        public static AddressInfo SetAddressInfo(AddressInfo addressInfo)
        {
            var input = new
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
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetAddressInfo", inputJson);
            AddressInfo address = (new JavaScriptSerializer()).Deserialize<AddressInfo>(json);

            return address;
        }

        /// <summary>
        /// Returns a FamilyInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required FamilyInfo 
        public static FamilyInfo GetFamilyInfo(string memberid)
        {
            var input = new
            {
                MemberId = memberid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetFamilyInfo", inputJson);
            FamilyInfo familyinfo = (new JavaScriptSerializer()).Deserialize<FamilyInfo>(json);

            return familyinfo;
        }

        /// <summary>
        /// Returns a list of FamilyInfo objects from Database via webapi
        /// </summary>
        public static List<FamilyInfo> GetFamilyInfoList()
        {
            var input = new
            {
                MemberId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetFamilyInfoList", inputJson);
            List<FamilyInfo> familyInfoList = (new JavaScriptSerializer()).Deserialize<List<FamilyInfo>>(json);

            return familyInfoList;
        }

        /// <summary>
        /// Set an FamilyInfo object from Database via web api
        /// </summary>
        /// <param name="FamilyInfo">The familyInfo to be set</param>
        /// <returns>The newly set familyInfo</returns>
        public static FamilyInfo SetFamilyInfo(FamilyInfo familyInfo)
        {
            var input = new
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
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetFamilyInfo", inputJson);
            FamilyInfo fam = (new JavaScriptSerializer()).Deserialize<FamilyInfo>(json);

            return fam;
        }


        /// <summary>
        /// Returns a EducationAndJobInfo object from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required EducationAndJobInfo 
        public static EducationAndJobInfo GetEducationAndJobInfo(string memberid)
        {
            var input = new
            {
                MemberId = memberid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetEducationAndJobInfo", inputJson);
            EducationAndJobInfo educationandjobinfo = (new JavaScriptSerializer()).Deserialize<EducationAndJobInfo>(json);

            return educationandjobinfo;
        }

        /// <summary>
        /// Returns a list of Education and Job Info objects from Database via webapi
        /// </summary>
        public static List<EducationAndJobInfo> GetEducationAndJobInfoList()
        {
            var input = new
            {
                MemberId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetEducationAndJobInfoList", inputJson);
            List<EducationAndJobInfo> familyInfoList = (new JavaScriptSerializer()).Deserialize<List<EducationAndJobInfo>>(json);

            return familyInfoList;
        }

        /// <summary>
        /// Set an EducationAndJobInfo object from Database via web api
        /// </summary>
        /// <param name="EducationAndJobInfo">The educAndJobInfo to be set</param>
        /// <returns>The newly set educAndJobInfo</returns>
        public static EducationAndJobInfo SetEducationAndJobInfo(EducationAndJobInfo educJobInfo)
        {
            var input = new
            {
                MemberId = educJobInfo.MemberId,
                EducationLevelId = educJobInfo.EducationLevelId,
                FieldOfStudyId = educJobInfo.FieldOfStudyId,
                JobId = educJobInfo.JobId,
                JobTypeId = educJobInfo.JobTypeId,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetEducationAndJobInfo", inputJson);
            EducationAndJobInfo eduJob = (new JavaScriptSerializer()).Deserialize<EducationAndJobInfo>(json);

            return eduJob;
        }

        /// <summary>
        /// Returns a list of MemberService objects from Database via webapi
        /// </summary>
        /// <param name="memberid">The primary key of the required MemberService 
        public static List<MemberService> GetMemberServiceList(string memberid)
        {
            var input = new
            {
                MemberId = memberid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMemberServiceList", inputJson);
            List<MemberService> memServ = (new JavaScriptSerializer()).Deserialize<List<MemberService>>(json);

            return memServ;
        }

        /// <summary>
        /// Returns all MemberService objects from Database via webapi
        /// </summary>
        public static List<MemberService> GetAllMemberServices()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetAllMemberServices", inputJson);
            List<MemberService> memServ = (new JavaScriptSerializer()).Deserialize<List<MemberService>>(json);

            return memServ;
        }

        /// <summary>
        /// Returns a MemberService object from Database via webapi
        /// </summary>
        /// <param name="id">The primary key of the required MemberService 
        public static MemberService GetMemberServiceById(int id)
        {
            var input = new
            {
                Id = id
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMemberServiceById", inputJson);
            MemberService memServ = (new JavaScriptSerializer()).Deserialize<MemberService>(json);

            return memServ;
        }

        /// <summary>
        /// Set an MemberService object from Database via web api
        /// </summary>
        /// <param name="MemberService">The memberService to be set</param>
        /// <returns>The newly set memberService</returns>
        public static MemberService SetMemberService(MemberService memberService)
        {
            var input = new
            {
                Id = memberService.Id,
                MemberId = memberService.MemberId,
                ServiceTypeId = memberService.ServiceTypeId,
                ServiceId = memberService.ServiceId
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetMemberService", inputJson);
            MemberService serv = (new JavaScriptSerializer()).Deserialize<MemberService>(json);

            return serv;
        }

        /// <summary>
        /// Set an MemberService object from Database via web api
        /// </summary>
        /// <param name="MemberService">The memberService to be set</param>
        /// <returns>The newly set memberService</returns>
        public static void DeleteMemberService(string id)
        {
            var input = new
            {
                ServiceId = id,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/DeleteMemberService", inputJson);
        }

        /// <summary>
        /// Returns a Gender object from Database via webapi
        /// </summary>
        /// <param name="genderid">The primary key of the required Gender 
        public static Gender GetGender(int genderid)
        {
            var input = new
            {
                GenderId = genderid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetGender", inputJson);
            Gender gender = (new JavaScriptSerializer()).Deserialize<Gender>(json);

            return gender;
        }

        /// <summary>
        /// Returns a list of all Genders objects from Database via web api
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Gender> GetGenderList(bool pleaseselect = false)
        {
            var input = new
            {
                GenderId = "",
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetGenderList", inputJson);
            List<Gender> genders = (new JavaScriptSerializer()).Deserialize<List<Gender>>(json);

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
            var input = new
            {
                SubcityId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetSubcityList", inputJson);
            List<Subcity> subcity = (new JavaScriptSerializer()).Deserialize<List<Subcity>>(json);

            if (pleaseselect)
                subcity.Insert(0, new Subcity { SubcityName = "", SubcityId = 0 });

            return subcity;
        }

        /// <summary>
        /// Returns a list of EducationLevel objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<EducationLevel> GetEducationLevelList(bool pleaseselect = false)
        {
            var input = new
            {
                EducationLevelId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetEducationLevelList", inputJson);
            List<EducationLevel> educationlevel = (new JavaScriptSerializer()).Deserialize<List<EducationLevel>>(json);

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
            var input = new
            {
                FieldOfStudyId = fieldofstudyid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetFieldOfStudy", inputJson);
            FieldOfStudy fieldofstudy = (new JavaScriptSerializer()).Deserialize<FieldOfStudy>(json);

            return fieldofstudy;
        }

        /// <summary>
        /// Returns a list of FieldOfStudy objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<FieldOfStudy> GetFieldOfStudyList(bool pleaseselect = false)
        {
            var input = new
            {
                FieldOfStudyId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetFieldOfStudyList", inputJson);
            List<FieldOfStudy> fieldofstudy = (new JavaScriptSerializer()).Deserialize< List<FieldOfStudy>> (json);

            if (pleaseselect)
                fieldofstudy.Insert(0, new FieldOfStudy { FieldOfStudyName = "", FieldOfStudyId = 0 });

            return fieldofstudy.OrderBy(m => m.FieldOfStudyName).ToList(); // order by field of name
        }

        // <summary>
        /// Set a FieldOfStudy object from Database via web api
        /// </summary>
        /// <param name="FieldOfStudy">The field to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetFieldOfStudy(FieldOfStudy field)
        {
            var input = new
            {
                FieldOfStudyId = field.FieldOfStudyId,
                FieldOfStudyName = field.FieldOfStudyName,
                EnglishName = field.EnglishName,
                Remark = field.Remark
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetFieldOfStudy", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Set an FieldOfStudy object from Database via web api
        /// </summary>
        /// <param name="FieldOfStudy">The FieldOfStudy to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteFieldOfStudy(int id)
        {
            var input = new
            {
                FieldOfStudyId = id,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/DeleteFieldOfStudy", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a list of HouseOwnershipType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<HouseOwnershipType> GetHouseOwnershipTypeList(bool pleaseselect = false)
        {
            var input = new
            {
                HouseOwnershipTypeId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetHouseOwnershipTypeList", inputJson);
            List<HouseOwnershipType> houseownershiptype = (new JavaScriptSerializer()).Deserialize< List<HouseOwnershipType>> (json);

            if (pleaseselect)
                houseownershiptype.Insert(0, new HouseOwnershipType { HouseOwnershipTypeName = "", HouseOwnershipTypeId = 0 });

            return houseownershiptype; // order by house ownership type name;
        }

        /// <summary>
        /// Returns a Job object from Database via webapi
        /// </summary>
        /// <param name="jobid">The primary key of the required Job 
        public static Job GetJob(int jobid)
        {
            var input = new
            {
                JobId = jobid.ToString()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetJob", inputJson);
            Job job = (new JavaScriptSerializer()).Deserialize<Job>(json);

            return job;
        }


        /// <summary>
        /// Returns a list of Job objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Job> GetJobList(bool pleaseselect = false)
        {
            var input = new
            {
                JobId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetJobList", inputJson);
            List<Job> job = (new JavaScriptSerializer()).Deserialize<List<Job>>(json);

            if (pleaseselect)
                job.Insert(0, new Job { JobName = "", JobId = 0 });

            return job.OrderBy(m => m.JobName).ToList(); // order by job name;
        }

        // <summary>
        /// Set a Job object from Database via web api
        /// </summary>
        /// <param name="Job">The job to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int SetJob(Job job)
        {
            var input = new
            {
                JobId = job.JobId,
                JobName = job.JobName,
                Remark = job.Remark
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/SetJob", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Set an Job object from Database via web api
        /// </summary>
        /// <param name="Job">The Job to be set</param>
        /// <returns>The status id of the operation</returns>
        public static int DeleteJob(int id)
        {
            var input = new
            {
                JobId = id,
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/DeleteJob", inputJson);
            ErrorViewModel err = (new JavaScriptSerializer()).Deserialize<ErrorViewModel>(json);

            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a list of JobType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<JobType> GetJobTypeList(bool pleaseselect = false)
        {
            var input = new
            {
                JobTypeId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetJobTypeList", inputJson);
            List<JobType> jobtype = (new JavaScriptSerializer()).Deserialize<List<JobType>>(json);

            if (pleaseselect)
                jobtype.Insert(0, new JobType { JobTypeName = "", JobTypeId = 0 });

            return jobtype;
        }

        /// <summary>
        /// Returns a list of MaritalStatus objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<MaritalStatus> GetMaritalStatusList(bool pleaseselect = false)
        {
            var input = new
            {
                MaritalStatusId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMaritalStatusList", inputJson);
            List<MaritalStatus> maritalstatus = (new JavaScriptSerializer()).Deserialize< List<MaritalStatus>> (json);

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
            var input = new
            {
                MembershipMeansId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMembershipMeansList", inputJson);
            List<MembershipMeans> membershipmeans = (new JavaScriptSerializer()).Deserialize<List<MembershipMeans>>(json);

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
            var input = new
            {
                MonthId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMonthList", inputJson);
            List<Month> month = (new JavaScriptSerializer()).Deserialize<List<Month>>(json);

            if (pleaseselect)
                month.Insert(0, new Month { MonthName = "", MonthId = 0 });

            return month;
        }

        /// <summary>
        /// Returns a list of Service objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<Service> GetServiceList(bool pleaseselect = false)
        {
            var input = new
            {
                ServiceId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetServiceList", inputJson);
            List<Service> service = (new JavaScriptSerializer()).Deserialize<List<Service>>(json);

            if (pleaseselect)
                service.Insert(0, new Service { ServiceName = "", ServiceId = 0 });

            return service.OrderBy(m => m.ServiceName).ToList(); // order by service name;
        }

        /// <summary>
        /// Returns a list of ServiceType objects from Database via webapi
        /// </summary>
        /// <param name="unselected">If true, the "--Please Select--" item will be added at the top of the list
        public static List<ServiceType> GetServiceTypeList(bool pleaseselect = false)
        {
            var input = new
            {
                ServiceTypeId = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetServiceTypeList", inputJson);
            List<ServiceType> servicetype = (new JavaScriptSerializer()).Deserialize<List<ServiceType>>(json);

            if (pleaseselect)
                servicetype.Insert(0, new ServiceType { ServiceTypeName = "", ServiceTypeId = 0 });

            return servicetype;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given education level objects from Database via webapi
        /// </summary>
        public static List<EducationLevelByGroup> GetEducationLevelByGroup()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetEducationLevelByGroup", inputJson);
            List<EducationLevelByGroup> edu = (new JavaScriptSerializer()).Deserialize<List<EducationLevelByGroup>>(json);

            return edu;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given marital status objects from Database via webapi
        /// </summary>
        public static List<MaritalStatusByGroup> GetMaritalStatusByGroup()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMaritalStatusByGroup", inputJson);
            List<MaritalStatusByGroup> status = (new JavaScriptSerializer()).Deserialize<List<MaritalStatusByGroup>>(json);

            return status;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given membership means objects from Database via webapi
        /// </summary>
        public static List<MembershipMeansByGroup> GetMembershipMeansByGroup()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetMembershipMeansByGroup", inputJson);
            List<MembershipMeansByGroup> means = (new JavaScriptSerializer()).Deserialize<List<MembershipMeansByGroup>>(json);

            return means;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given subcity objects from Database via webapi
        /// </summary>
        public static List<SubcityByGroup> GetSubcityByGroup()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetSubcityByGroup", inputJson);
            List<SubcityByGroup> subcity = (new JavaScriptSerializer()).Deserialize<List<SubcityByGroup>>(json);

            return subcity;
        }

        /// <summary>
        /// Returns a list of members and the corresponding number of members with a given gender objects from Database via webapi
        /// </summary>
        public static List<GenderByGroup> GetGenderByGroup()
        {
            var input = new
            {
                Id = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(gkhcApiUrl + "/GetGenderByGroup", inputJson);
            List<GenderByGroup> gender = (new JavaScriptSerializer()).Deserialize<List<GenderByGroup>>(json);

            return gender;
        }
    }
}