using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageHost.Models;
using ImageHost.Models.Data;
using ImageHost.Static_Classes;
using log4net;
using log4net.Core;

namespace ImageHost.Controllers
{
    public class ImageController : Controller
    {
        private ILog _logger = LogManager.GetLogger("ImgHoster");
        private DbWrapper dbw;

        public ImageController()
        {
            try
            {
                dbw = new DbWrapper(new ImgHostContext());
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
            if (file.ContentLength <= 0) return View("Error", new Error { Message = "There was a problem uploading your file, please try again later."});

            
            var fileName = Path.GetFileName(file.FileName);
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