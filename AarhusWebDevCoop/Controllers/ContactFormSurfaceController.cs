using AarhusWebDevCoop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;


namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }


        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }


            using (SmtpClient smtp = new SmtpClient())
            {

                TempData["success"] = true;
                // create message
                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Message");

                comment.SetValue("senderName", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("message", model.Message);

                //Save
                Services.ContentService.Save(comment);

            }
            return RedirectToCurrentUmbracoPage();
        }

    }
}