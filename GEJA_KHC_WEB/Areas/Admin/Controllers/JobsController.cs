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
    public class JobsController : Controller
    {
        // GET: Jobs
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

            List<Job> job = DbViewModel.GetJobList();

            return View(job);
        }

        public JsonResult UpdateJob(Job serv)
        {
            int statusId;
            Job updatedJob = (from c in DbViewModel.GetJobList()
                                      where c.JobId == serv.JobId
                                      select c).FirstOrDefault();

            updatedJob.JobName = serv.JobName;
            updatedJob.Remark = serv.Remark;

            statusId = DbViewModel.SetJob(updatedJob);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteJob(int Id)
        {
            int statusId = 0;
            Job job = DbViewModel.GetJob(Id);

            if (job != null)
            {
                statusId = DbViewModel.DeleteJob(Id);
            }

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewJobPartial()
        {
            return PartialView();
        }

        public JsonResult AddNewJob([Bind(Include = "JobName,Remark")] Job job)
        {
            int statusId = DbViewModel.SetJob(job);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }
    }
}