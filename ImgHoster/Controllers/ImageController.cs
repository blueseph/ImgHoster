using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImgHoster.Models;
using ImgHoster.Models.Data;
using ImgHoster.Static_Classes;
using log4net;
using log4net.Core;

namespace ImgHoster.Controllers
{
    public class ImageController : Controller
    {
        // GET: /images/{idString}
        [Route("images/{idString}")]
        public ActionResult Show(string idString)
        {
            using (var context = new ImgHostContext())
            {
                long id = Base62.FromBase(idString);

                var requestedImage = context.Images.FirstOrDefault(i => i.Id == id);

                //no result? return error page
                if (requestedImage != null) return View(requestedImage);

                return View("Error", new Error { Message = "Sorry, we couldn't find the requested image!"});
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0)
                return View("Error", new Error { Message = "There was a problem uploading your file, please try again later."});

            if (file.ContentLength > 3145728)
                return View("Error", new Error {Message = "Sorry, file size cannot be over 3MB!"});

            //generate unique filename
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var serverPath = Path.Combine(Server.MapPath("~/uploads"), fileName);

            var img = new HostedImage
            {
                FileName = fileName,
                FullServerPath = serverPath
            };
            try
            {
                using (var context = new ImgHostContext())
                {
                    context.Images.AddOrUpdate(img);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                return View("Error", new Error { Message = e.InnerException.Message });
            }
                
            file.SaveAs(serverPath);

            //pass to /images/encodedname to display the image after upload
            //return RedirectToRoute("ShowImageRoute", new { idString = img.EncodedName });
            return Redirect(string.Format("~/images/{0}", img.EncodedName));
        }
    }
}