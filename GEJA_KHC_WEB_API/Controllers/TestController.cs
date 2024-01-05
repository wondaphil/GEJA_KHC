using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using GEJA_KHC_WEB_API.Models;
using static System.Collections.Specialized.BitVector32;

namespace GEJA_KHC_WEB_API.Controllers
{
    public class TestController : Controller
    {
        Geja_KHC_Entities db = new Geja_KHC_Entities();

        // GET: Members
        public ActionResult Index(string id = "")
        {
            return View();
        }

        //GET
        public ActionResult TestMemberPhoto([Optional] string memberid /* drop down value */)
        {
            if (memberid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPhoto memberPhoto = db.MemberPhoto.Find(memberid);
            TempData["MemberId"] = memberid;

            //If the member does not have a Photo entry, return a "New" view
            //But if the member already has a Photo entry, return an "Edit" view
            if (memberPhoto == null)
            {
                return View("TestMemberPhotoNew", memberPhoto);
            }

            return View("TestMemberPhoto", memberPhoto);
        }

        [HttpPost]
        public JsonResult TestMemberPhotoSave(MemberPhoto memberPhoto)
        {
            if (Request.Files.Count > 0)
            {
                //Upload file to server
                HttpPostedFileBase file = Request.Files[0];
                string fullpath = Server.MapPath(memberPhoto.PhotoFilePath); // Path including the file name
                string path = Server.MapPath(Path.GetDirectoryName(memberPhoto.PhotoFilePath)); // Path excluding the file name
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(fullpath);

                // Convert image file to bytes
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                memberPhoto.Photo = bytes;

                // Save media path, photo and remark to database
                MemberPhoto photo = db.MemberPhoto.Find(memberPhoto.MemberId);
                photo.PhotoFilePath = memberPhoto.PhotoFilePath;
                photo.Photo = memberPhoto.Photo;
                photo.Remark = memberPhoto.Remark;

                db.SaveChanges();
                
                ViewBag.Success = true;

            }

            return Json(memberPhoto);
        }

        public JsonResult TestMemberPhotoNew([Bind(Include = "MemberId,PhotoFilePath,Remark")] MemberPhoto memberPhoto)
        {
            // Save media path, photo and remark to database
            MemberPhoto photo = db.MemberPhoto.Find(memberPhoto.MemberId);
            photo.PhotoFilePath = memberPhoto.PhotoFilePath;
            photo.Photo = memberPhoto.Photo;
            photo.Remark = memberPhoto.Remark;

            db.SaveChanges();

            //Upload file to server
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string fullpath = Server.MapPath(memberPhoto.PhotoFilePath); // Path including the file name
                string path = Server.MapPath(Path.GetDirectoryName(memberPhoto.PhotoFilePath)); // Path excluding the file name
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(fullpath);
                ViewBag.Success = true;
            }

            return Json(memberPhoto);
        }
    }
}