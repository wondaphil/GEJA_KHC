using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB.Models;
using GEJA_KHC_WEB.ViewModels;
using System.Data.SqlClient;

namespace GEJA_KHC_WEB.Areas.Admin.Controllers
{
    //Only Admins should have access
    [NoCache]
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        // GET: Services
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

            List<Service> service = DbViewModel.GetServiceList();

            return View(service);
        }

        public JsonResult UpdateService(Service serv)
        {
            int statusId;
            Service updatedService = (from c in DbViewModel.GetServiceList()
                                      where c.ServiceId == serv.ServiceId
                                      select c).FirstOrDefault();

            updatedService.ServiceName = serv.ServiceName;
            updatedService.Remark = serv.Remark;

            statusId = DbViewModel.SetService(updatedService);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteService(int Id)
        {
            int statusId = 0;
            Service service = DbViewModel.GetService(Id);

            if (service != null)
            {
                statusId = DbViewModel.DeleteService(Id);
            }

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewServicePartial()
        {
            return PartialView();
        }

        public JsonResult AddNewService([Bind(Include = "ServiceName,Remark")] Service service)
        {
            int statusId = DbViewModel.SetService(service);

            return Json(new { Status = statusId }, JsonRequestBehavior.AllowGet);
        }
    }
}