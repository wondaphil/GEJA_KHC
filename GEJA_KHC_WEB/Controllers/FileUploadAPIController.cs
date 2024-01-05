using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GEJA_KHC_WEB.Controllers
{
    public class FileUploadAPIController : ApiController
    {
        [System.Web.Http.Route("api/FileUploadAPI/UploadImage")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage UploadImage()
        {
            HttpPostedFile postedImageFile;

            try
            {
                //Create the Directory if it does not exist
                string path = HttpContext.Current.Server.MapPath("~/photos/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Fetch the image File
                postedImageFile = HttpContext.Current.Request.Files[0];

                //Save the File
                postedImageFile.SaveAs(path + postedImageFile.FileName);
            }
            catch (Exception)
            {
                // Send "Internal Server Error" Response to Client
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            // Send "OK" Response to Client
            return Request.CreateResponse(HttpStatusCode.OK, postedImageFile.FileName);
        }
    }
}