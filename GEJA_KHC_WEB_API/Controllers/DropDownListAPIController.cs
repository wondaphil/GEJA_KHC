using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using GEJA_KHC_WEB_API.Models;
using GEJA_KHC_WEB_API.ViewModels;

namespace GEJA_KHC_WEB_API.Controllers
{
    public class DropDownListAPIController : ApiController
    {
        private Geja_KHC_Entities db = new Geja_KHC_Entities();

        /// <summary>
        /// Returns a drop down list of Group
        /// </summary>
        [System.Web.Http.Route("api/DropDownListAPI/GetGroupsDDL")]
        [System.Web.Http.HttpPost]
        public List<DropDownListModel> GetGroupsDDL()
        {
            return ListViewModel.GetGroupList();
        }

        /// <summary>
        /// Returns a drop down list of Members
        /// </summary>
        /// <param name="group">A Group object with Id which encompasses the members
        [System.Web.Http.Route("api/DropDownListAPI/GetMembersDDL")]
        [System.Web.Http.HttpPost]
        public List<DropDownListModel> GetMembersDDL(Group group)
        {
            //return this.Json(new SelectList(ListViewModel.GetMemberNameList(groupid).ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            return ListViewModel.GetMemberNameList(group.GroupId);
        }
    }
}