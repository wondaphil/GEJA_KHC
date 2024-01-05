using GEJA_KHC_MAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class DbViewModel
    {
        /// <summary>
        /// URL of the web application
        /// </summary>
        //static string webApplicationUrl = "http://192.168.1.20:777";
        static string webApplicationUrl = "http://192.168.43.17:777";
        //static string webApplicationUrl = "http://192.168.42.132:777";
        //static string webApplicationUrl = "http://192.168.43.17:65274";

        public static string GetWebApplicationUrl()
        {
            return webApplicationUrl;
        }

        /// <summary>
        /// Returns a Member object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Member> GetMember(string memberid)
        {
            
            var input = new { MemberId = memberid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var member = new Member();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMember") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(member);
            }

            if (responseMessage.IsSuccessStatusCode)
                member = await responseMessage.Content.ReadFromJsonAsync<Member>();

            return await Task.FromResult(member);
        }

        /// <summary>
        /// Returns a list of all Member objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Member>> GetMemberList(int groupid)
        {
            var input = new { GroupId = groupid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var memberList = new List<Member>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMemberList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(memberList);
            }

            if (responseMessage.IsSuccessStatusCode)
                memberList = await responseMessage.Content.ReadFromJsonAsync<List<Member>>();

            return await Task.FromResult(memberList);
        }

        /// <summary>
        /// Returns a list of all Member objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Member>> GetAllMembers()
        {
            var input = new { GroupId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var memberList = new List<Member>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetAllMembers") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(memberList);
            }

            if (responseMessage.IsSuccessStatusCode)
                memberList = await responseMessage.Content.ReadFromJsonAsync<List<Member>>();

            return await Task.FromResult(memberList);
        }

        /// <summary>
        /// Set a Member object from Database via web api
        /// </summary>
        /// <param name="member">The member to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetMember(Member member)
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
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetMember") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Delete an Member object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Member to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> DeleteMember(string id)
        {
            var input = new
            {
                MemberId = id,
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/DeleteMember") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a Group object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Group> GetGroup(int groupid)
        {
            var input = new { GroupId = groupid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var group = new Group();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetGroup") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(group);
            }

            if (responseMessage.IsSuccessStatusCode)
                group = await responseMessage.Content.ReadFromJsonAsync<Group>();

            return await Task.FromResult(group);
        }
        
        /// <summary>
         /// Returns a list of all Group objects from Database via web api
         /// </summary>
         /// <returns></returns>
        public static async Task<IEnumerable<Group>> GetGroupList()
        {
            var input = new { GroupId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var groupList = new List<Group>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetGroupList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                //return await Task.FromResult(groupList);
            }

            if (responseMessage.IsSuccessStatusCode)
                groupList = await responseMessage.Content.ReadFromJsonAsync<List<Group>>();
            
            return await Task.FromResult(groupList);
        }

        /// <summary>
        /// Set a Group object from Database via web api
        /// </summary>
        /// <param name="group">The group to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetGroup(Group group)
        {
            var input = new
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                Pastor = group.Pastor,
                Remark = group.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetGroup") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Set an Group object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Group to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> DeleteGroup(int id)
        {
            var input = new
            {
                GroupId = id,
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/DeleteGroup") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a Gender object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Gender> GetGender(int genderid)
        {
            var input = new { GenderId = genderid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var gender = new Gender();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetGender") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(gender);
            }

            if (responseMessage.IsSuccessStatusCode)
                gender = await responseMessage.Content.ReadFromJsonAsync<Gender>();

            return await Task.FromResult(gender);
        }

        /// <summary>
        /// Returns a list of all Gender objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Gender>> GetGenderList()
        {
            var input = new { GenderId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var genderList = new List<Gender>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetGenderList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(genderList);
            }

            if (responseMessage.IsSuccessStatusCode)
                genderList = await responseMessage.Content.ReadFromJsonAsync<List<Gender>>();

            return await Task.FromResult(genderList);
        }

        /// <summary>
        /// Returns a Month object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Month> GetMonth(int groupid)
        {
            var input = new { MonthId = groupid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var group = new Month();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMonth") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(group);
            }

            if (responseMessage.IsSuccessStatusCode)
                group = await responseMessage.Content.ReadFromJsonAsync<Month>();

            return await Task.FromResult(group);
        }

        /// <summary>
        /// Returns a list of all Month objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Month>> GetMonthList()
        {
            var input = new { MonthId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var monthList = new List<Month>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMonthList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(monthList);
            }

            if (responseMessage.IsSuccessStatusCode)
                monthList = await responseMessage.Content.ReadFromJsonAsync<List<Month>>();

            return await Task.FromResult(monthList);
        }

        /// <summary>
        /// Returns a AddressInfo object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<AddressInfo> GetAddressInfo(string memberid)
        {
            var input = new { MemberId = memberid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var address = new AddressInfo();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetAddressInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(address);
            }

            if (responseMessage.IsSuccessStatusCode)
                address = await responseMessage.Content.ReadFromJsonAsync<AddressInfo>();

            return await Task.FromResult(address);
        }

        /// <summary>
        /// Returns a list of Address Info objects from Database via webapi
        /// </summary>
        public static async Task<IEnumerable<AddressInfo>> GetAddressInfoList()
        {
            var input = new { MemberId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var addressList = new List<AddressInfo>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetAddressInfoList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(addressList);
            }

            if (responseMessage.IsSuccessStatusCode)
                addressList = await responseMessage.Content.ReadFromJsonAsync<List<AddressInfo>>();

            return await Task.FromResult(addressList);
        }

        /// <summary>
        /// Set a AddressInfo object from Database via web api
        /// </summary>
        /// <param name="addressInfo">The address to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetAddressInfo(AddressInfo addressInfo)
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
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetAddressInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a Subcity object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Subcity> GetSubcity(int subcityid)
        {
            var input = new { SubcityId = subcityid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var subcity = new Subcity();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetSubcity") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(subcity);
            }

            if (responseMessage.IsSuccessStatusCode)
                subcity = await responseMessage.Content.ReadFromJsonAsync<Subcity>();

            return await Task.FromResult(subcity);
        }

        /// <summary>
        /// Returns a list of all Subcity objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Subcity>> GetSubcityList()
        {
            var input = new { SubcityId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var subcityList = new List<Subcity>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetSubcityList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(subcityList);
            }

            if (responseMessage.IsSuccessStatusCode)
                subcityList = await responseMessage.Content.ReadFromJsonAsync<List<Subcity>>();

            return await Task.FromResult(subcityList);
        }

        /// <summary>
        /// Returns a HouseOwnershipType object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<HouseOwnershipType> GetHouseOwnershipType(int ownershipid)
        {
            var input = new { HouseOwnershipTypeId = ownershipid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var ownership = new HouseOwnershipType();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetHouseOwnershipType") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(ownership);
            }

            if (responseMessage.IsSuccessStatusCode)
                ownership = await responseMessage.Content.ReadFromJsonAsync<HouseOwnershipType>();

            return await Task.FromResult(ownership);
        }

        /// <summary>
        /// Returns a list of all HouseOwnershipType objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<HouseOwnershipType>> GetHouseOwnershipTypeList()
        {
            var input = new { HouseOwnershipTypeId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var ownershipList = new List<HouseOwnershipType>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetHouseOwnershipTypeList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(ownershipList);
            }

            if (responseMessage.IsSuccessStatusCode)
                ownershipList = await responseMessage.Content.ReadFromJsonAsync<List<HouseOwnershipType>>();

            return await Task.FromResult(ownershipList);
        }

        /// <summary>
        /// Returns a FamilyInfo object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<FamilyInfo> GetFamilyInfo(string memberid)
        {
            var input = new { MemberId = memberid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var family = new FamilyInfo();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetFamilyInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(family);
            }

            if (responseMessage.IsSuccessStatusCode)
                family = await responseMessage.Content.ReadFromJsonAsync<FamilyInfo>();

            return await Task.FromResult(family);
        }

        /// <summary>
        /// Returns a list of Family Info objects from Database via webapi
        /// </summary>
        public static async Task<IEnumerable<FamilyInfo>> GetFamilyInfoList()
        {
            var input = new { MemberId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var familyList = new List<FamilyInfo>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetFamilyInfoList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(familyList);
            }

            if (responseMessage.IsSuccessStatusCode)
                familyList = await responseMessage.Content.ReadFromJsonAsync<List<FamilyInfo>>();

            return await Task.FromResult(familyList);
        }

        /// <summary>
        /// Set a FamilyInfo object from Database via web api
        /// </summary>
        /// <param name="familyInfo">The family to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetFamilyInfo(FamilyInfo familyInfo)
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
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetFamilyInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a MaritalStatus object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<MaritalStatus> GetMaritalStatus(int statusid)
        {
            var input = new { MaritalStatusId = statusid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var status = new MaritalStatus();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMaritalStatus") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(status);
            }

            if (responseMessage.IsSuccessStatusCode)
                status = await responseMessage.Content.ReadFromJsonAsync<MaritalStatus>();

            return await Task.FromResult(status);
        }

        /// <summary>
        /// Returns a list of all MaritalStatus objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<MaritalStatus>> GetMaritalStatusList()
        {
            var input = new { MaritalStatusId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var statusList = new List<MaritalStatus>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMaritalStatusList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(statusList);
            }

            if (responseMessage.IsSuccessStatusCode)
                statusList = await responseMessage.Content.ReadFromJsonAsync<List<MaritalStatus>>();

            return await Task.FromResult(statusList);
        }

        /// <summary>
        /// Returns a EducationAndJobInfo object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<EducationAndJobInfo> GetEducationAndJobInfo(string memberid)
        {
            var input = new { MemberId = memberid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var eduJob = new EducationAndJobInfo();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetEducationAndJobInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(eduJob);
            }

            if (responseMessage.IsSuccessStatusCode)
                eduJob = await responseMessage.Content.ReadFromJsonAsync<EducationAndJobInfo>();

            return await Task.FromResult(eduJob);
        }

        /// <summary>
        /// Returns a list of Education and Job Info objects from Database via webapi
        /// </summary>
        public static async Task<IEnumerable<EducationAndJobInfo>> GetEducationAndJobInfoList()
        {
            var input = new { MemberId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var eduJobList = new List<EducationAndJobInfo>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetEducationAndJobInfoList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(eduJobList);
            }

            if (responseMessage.IsSuccessStatusCode)
                eduJobList = await responseMessage.Content.ReadFromJsonAsync<List<EducationAndJobInfo>>();

            return await Task.FromResult(eduJobList);
        }

        /// <summary>
        /// Set a EducationAndJobInfo object from Database via web api
        /// </summary>
        /// <param name="educJobInfo">The eduJob to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetEducationAndJobInfo(EducationAndJobInfo educJobInfo)
        {
            var input = new
            {
                MemberId = educJobInfo.MemberId,
                EducationLevelId = educJobInfo.EducationLevelId,
                FieldOfStudyId = educJobInfo.FieldOfStudyId,
                JobId = educJobInfo.JobId,
                JobTypeId = educJobInfo.JobTypeId
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetEducationAndJobInfo") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a EducationLevel object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<EducationLevel> GetEducationLevel(int educationid)
        {
            var input = new { EducationLevelId = educationid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var education = new EducationLevel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetEducationLevel") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(education);
            }

            if (responseMessage.IsSuccessStatusCode)
                education = await responseMessage.Content.ReadFromJsonAsync<EducationLevel>();

            return await Task.FromResult(education);
        }

        /// <summary>
        /// Returns a list of all EducationLevel objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<EducationLevel>> GetEducationLevelList()
        {
            var input = new { EducationLevelId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var educationList = new List<EducationLevel>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetEducationLevelList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(educationList);
            }

            if (responseMessage.IsSuccessStatusCode)
                educationList = await responseMessage.Content.ReadFromJsonAsync<List<EducationLevel>>();

            return await Task.FromResult(educationList);
        }

        /// <summary>
        /// Set a EducationLevel object from Database via web api
        /// </summary>
        /// <param name="education">The education to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetEducationLevel(EducationLevel education)
        {
            var input = new
            {
                EducationLevelId = education.EducationLevelId,
                EducationLevelName = education.EducationLevelName,
                Remark = education.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetEducationLevel") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a FieldOfStudy object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<FieldOfStudy> GetFieldOfStudy(int fieldid)
        {
            var input = new { FieldOfStudyId = fieldid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var field = new FieldOfStudy();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetFieldOfStudy") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(field);
            }

            if (responseMessage.IsSuccessStatusCode)
                field = await responseMessage.Content.ReadFromJsonAsync<FieldOfStudy>();

            return await Task.FromResult(field);
        }

        /// <summary>
        /// Returns a list of all FieldOfStudy objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<FieldOfStudy>> GetFieldOfStudyList()
        {
            var input = new { FieldOfStudyId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var fieldList = new List<FieldOfStudy>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetFieldOfStudyList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(fieldList);
            }

            if (responseMessage.IsSuccessStatusCode)
                fieldList = await responseMessage.Content.ReadFromJsonAsync<List<FieldOfStudy>>();

            return await Task.FromResult(fieldList);
        }

        /// <summary>
        /// Set a FieldOfStudy object from Database via web api
        /// </summary>
        /// <param name="field">The field to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetFieldOfStudy(FieldOfStudy field)
        {
            var input = new
            {
                FieldOfStudyId = field.FieldOfStudyId,
                FieldOfStudyName = field.FieldOfStudyName,
                EnglishName = field.EnglishName,
                Remark = field.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetFieldOfStudy") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a Job object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Job> GetJob(int jobid)
        {
            var input = new { JobId = jobid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var job = new Job();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetJob") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(job);
            }

            if (responseMessage.IsSuccessStatusCode)
                job = await responseMessage.Content.ReadFromJsonAsync<Job>();

            return await Task.FromResult(job);
        }

        /// <summary>
        /// Returns a list of all Job objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Job>> GetJobList()
        {
            var input = new { JobId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var jobList = new List<Job>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetJobList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(jobList);
            }

            if (responseMessage.IsSuccessStatusCode)
                jobList = await responseMessage.Content.ReadFromJsonAsync<List<Job>>();

            return await Task.FromResult(jobList);
        }

        /// <summary>
        /// Set a Job object from Database via web api
        /// </summary>
        /// <param name="job">The job to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetJob(Job job)
        {
            var input = new
            {
                JobId = job.JobId,
                JobName = job.JobName,
                Remark = job.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetJob") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a JobType object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<JobType> GetJobType(int jobTypeid)
        {
            var input = new { JobTypeId = jobTypeid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var jobType = new JobType();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetJobType") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(jobType);
            }

            if (responseMessage.IsSuccessStatusCode)
                jobType = await responseMessage.Content.ReadFromJsonAsync<JobType>();

            return await Task.FromResult(jobType);
        }

        /// <summary>
        /// Returns a list of all JobType objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<JobType>> GetJobTypeList()
        {
            var input = new { JobTypeId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var jobTypeList = new List<JobType>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetJobTypeList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(jobTypeList);
            }

            if (responseMessage.IsSuccessStatusCode)
                jobTypeList = await responseMessage.Content.ReadFromJsonAsync<List<JobType>>();

            return await Task.FromResult(jobTypeList);
        }

        /// <summary>
        /// Returns a MembershipMeans object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<MembershipMeans> GetMembershipMeans(int meansid)
        {
            var input = new { MembershipMeansId = meansid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var means = new MembershipMeans();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMembershipMeans") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(means);
            }

            if (responseMessage.IsSuccessStatusCode)
                means = await responseMessage.Content.ReadFromJsonAsync<MembershipMeans>();

            return await Task.FromResult(means);
        }

        /// <summary>
        /// Returns a list of all MembershipMeans objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<MembershipMeans>> GetMembershipMeansList()
        {
            var input = new { MembershipMeansId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var meansList = new List<MembershipMeans>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMembershipMeansList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(meansList);
            }

            if (responseMessage.IsSuccessStatusCode)
                meansList = await responseMessage.Content.ReadFromJsonAsync<List<MembershipMeans>>();

            return await Task.FromResult(meansList);
        }

        /// <summary>
        /// Returns a MemberPhoto object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<MemberPhoto> GetMemberPhoto(string memberid)
        {
            var input = new { MemberId = memberid };
            string inputJson = JsonConvert.SerializeObject(input);
            var photo = new MemberPhoto();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMemberPhoto") };
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
        /// Set a MemberPhoto object from Database via web api
        /// </summary>
        /// <param name="photo">The photo to be set</param>
        /// <returns>The status id of the operation</returns>
        //public static async Task<int> SetMemberPhoto(MemberPhoto photo)
        //{
        //    var input = new
        //    {
        //        MemberId = photo.MemberId,
        //        PhotoFilePath = photo.PhotoFilePath,
        //        Remark = photo.Remark
        //    };
        //    string inputJson = JsonConvert.SerializeObject(input);
        //    ErrorViewModel err = new ErrorViewModel();

        //    HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetMemberPhoto") };
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

        public static async Task<int> SetMemberPhoto(MemberPhoto photo)
        {
            var input = new
            {
                MemberId = photo.MemberId,
                PhotoFilePath = photo.PhotoFilePath,
                Photo = photo.Photo,
                Remark = photo.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetMemberPhoto") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a list of all MemberService objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<MemberService>> GetAllMemberServices()
        {
            var input = new { MemberId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var memberServiceList = new List<MemberService>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetAllMemberServices") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(memberServiceList);
            }

            if (responseMessage.IsSuccessStatusCode)
                memberServiceList = await responseMessage.Content.ReadFromJsonAsync<List<MemberService>>();

            return await Task.FromResult(memberServiceList);
        }

        /// <summary>
        /// Returns a list of all MemberService objects from Database via web api
        /// </summary>
        /// <param name="memberid">The primary key of the required MemberService </param>
        /// <returns></returns>
        public static async Task<IEnumerable<MemberService>> GetMemberServiceList(string memberid)
        {
            var input = new { MemberId = memberid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var memberServiceList = new List<MemberService>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMemberServiceList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(memberServiceList);
            }

            if (responseMessage.IsSuccessStatusCode)
                memberServiceList = await responseMessage.Content.ReadFromJsonAsync<List<MemberService>>();

            return await Task.FromResult(memberServiceList);
        }

        /// <summary>
        /// Returns a MemberService object from Database via webapi
        /// </summary>
        /// <param name="id">The primary key of the required MemberService </param>
        public static async Task<MemberService> GetMemberServiceById(int id)
        {

            var input = new { Id = id.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var memberService = new MemberService();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetMemberServiceById") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(memberService);
            }

            if (responseMessage.IsSuccessStatusCode)
                memberService = await responseMessage.Content.ReadFromJsonAsync<MemberService>();

            return await Task.FromResult(memberService);
        }

        /// <summary>
        /// Set a MemberService object from Database via web api
        /// </summary>
        /// <param name="memberService">The memberService to be set</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> SetMemberService(MemberService memberService)
        {
            var input = new
            {
                Id = memberService.Id,
                MemberId = memberService.MemberId,
                ServiceTypeId = memberService.ServiceTypeId,
                ServiceId = memberService.ServiceId
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetMemberService") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Delete an MemberService object from Database via web api
        /// </summary>
        /// <param name="id">The id of the MemberService to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> DeleteMemberService(int id)
        {
            var input = new
            {
                Id = id.ToString(),
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/DeleteMemberService") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Returns a ServiceType object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<ServiceType> GetServiceType(int serviceTypeid)
        {
            var input = new { ServiceTypeId = serviceTypeid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var serviceType = new ServiceType();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetServiceType") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(serviceType);
            }

            if (responseMessage.IsSuccessStatusCode)
                serviceType = await responseMessage.Content.ReadFromJsonAsync<ServiceType>();

            return await Task.FromResult(serviceType);
        }

        /// <summary>
        /// Returns a list of all ServiceType objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<ServiceType>> GetServiceTypeList()
        {
            var input = new { ServiceTypeId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var serviceTypeList = new List<ServiceType>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetServiceTypeList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(serviceTypeList);
            }

            if (responseMessage.IsSuccessStatusCode)
                serviceTypeList = await responseMessage.Content.ReadFromJsonAsync<List<ServiceType>>();

            return await Task.FromResult(serviceTypeList);
        }

        /// <summary>
        /// Returns a Service object from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<Service> GetService(int serviceid)
        {
            var input = new { ServiceId = serviceid.ToString() };
            string inputJson = JsonConvert.SerializeObject(input);
            var service = new Service();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetService") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(service);
            }

            if (responseMessage.IsSuccessStatusCode)
                service = await responseMessage.Content.ReadFromJsonAsync<Service>();

            return await Task.FromResult(service);
        }

        /// <summary>
        /// Returns a list of all Service objects from Database via web api
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Service>> GetServiceList()
        {
            var input = new { ServiceId = "" };
            string inputJson = JsonConvert.SerializeObject(input);
            var serviceList = new List<Service>();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/GetServiceList") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(serviceList);
            }

            if (responseMessage.IsSuccessStatusCode)
                serviceList = await responseMessage.Content.ReadFromJsonAsync<List<Service>>();

            return await Task.FromResult(serviceList);
        }
        
        /// <summary>
         /// Set a Service object from Database via web api
         /// </summary>
         /// <param name="service">The service to be set</param>
         /// <returns>The status id of the operation</returns>
        public static async Task<int> SetService(Service service)
        {
            var input = new
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Remark = service.Remark
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/SetService") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }

        /// <summary>
        /// Delete an Service object from Database via web api
        /// </summary>
        /// <param name="id">The id of the Service to be deleted</param>
        /// <returns>The status id of the operation</returns>
        public static async Task<int> DeleteService(string id)
        {
            var input = new
            {
                ServiceId = id,
            };
            string inputJson = JsonConvert.SerializeObject(input);
            ErrorViewModel err = new ErrorViewModel();

            HttpClient client = new HttpClient() { BaseAddress = new Uri(WebApiURL.Address + "/api/GejaKhcAPI/DeleteService") };
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            try
            {
                responseMessage = await client.PostAsync("", content).ConfigureAwait(false);
            }
            catch (Exception)
            {
                err = await Task.FromResult(new ErrorViewModel());
                return err.ExceptionNumber;
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                err = await responseMessage.Content.ReadFromJsonAsync<ErrorViewModel>();
                return err.ExceptionNumber;
            }

            err = await Task.FromResult(err);
            return err.ExceptionNumber;
        }
    }
}
