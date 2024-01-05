using GEJA_KHC_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;

namespace GEJA_KHC_WEB.Areas.Admin.Controllers
{
    //Only Admins should have access
    [NoCache]
    [Authorize(Roles = "Admin")]
    public class FieldOfStudiesController : Controller
    {
        // GET: FieldOfStudies
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

            List<FieldOfStudy> field = DbViewModel.GetFieldOfStudyList();

            return View(field);
        }

        public JsonResult UpdateFieldOfStudy(FieldOfStudy serv)
        {
            int statusId;
            FieldOfStudy updatedFieldOfStudy = (from c in DbViewModel.GetFieldOfStudyList()
                                      where c.FieldOfStudyId == serv.FieldOfStudyId
                                      select c).FirstOrDefault();

            updatedFieldOfStudy.FieldOfStudyName = serv.FieldOfStudyName;
            updatedFieldOfStudy.EnglishName = serv.EnglishName;
            updatedFieldOfStudy.Remark = serv.Remark;

            statusId = DbViewModel.SetFieldOfStudy(updatedFieldOfStudy);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFieldOfStudy(int Id)
        {
            int statusId = 0;
            FieldOfStudy field = DbViewModel.GetFieldOfStudy(Id);

            if (field != null)
            {
                statusId = DbViewModel.DeleteFieldOfStudy(Id);
            }

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewFieldOfStudyPartial()
        {
            return PartialView();
        }

        public JsonResult AddNewFieldOfStudy([Bind(Include = "FieldOfStudyName,EnglishName,Remark")] FieldOfStudy field)
        {
            int statusId = DbViewModel.SetFieldOfStudy(field);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }
    }
}