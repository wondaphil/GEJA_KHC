using GEJA_KHC_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace GEJA_KHC_WEB.Areas.Admin.Controllers
{
    //Only Admins should have access
    [NoCache]
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult Index(StatusMessageId? messageId)
        {
            if (messageId != null)
            {
                string statusMSg = StatusMessageViewModel.GetMessage(messageId);
                List<StatusMessageId> successList = new List<StatusMessageId>()
                                                        {
                                                            StatusMessageId.AddDataSuccess,
                                                            StatusMessageId.UpdateDataSuccess,
                                                            StatusMessageId.DeleteDataSuccess
                                                        };

                ViewBag.StatusMessage = statusMSg;
                ViewBag.TextColor = (successList.Contains((StatusMessageId)messageId)) ? "text-success" : "text-danger";
            }
            List<Group> group = DbViewModel.GetGroupList();

            return View(group);
        }

        public JsonResult UpdateGroup(Group grp)
        {
            int statusId;
            Group updatedGroup = (from c in DbViewModel.GetGroupList()
                                  where c.GroupId == grp.GroupId
                                  select c).FirstOrDefault();

            updatedGroup.GroupName = grp.GroupName;
            updatedGroup.Pastor = grp.Pastor;
            updatedGroup.Remark = grp.Remark;

            statusId = DbViewModel.SetGroup(updatedGroup);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGroup(int Id)
        {
            int statusId = 0;
            Group group = DbViewModel.GetGroup(Id);
            
            if (group != null)
            {
                statusId = DbViewModel.DeleteGroup(Id);
             }

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewGroupPartial()
        {
            return PartialView();
        }

        public JsonResult AddNewGroup([Bind(Include = "GroupName,Pastor,Remark")] Group group)
        {
            int statusId = DbViewModel.SetGroup(group);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }
    }
}