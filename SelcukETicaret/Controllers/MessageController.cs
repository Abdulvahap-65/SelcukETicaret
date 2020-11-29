using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelcukETicaret.Controllers
{
    public class MessageController : BaseController
    {
        // GET: Message
        [HttpGet]
        public ActionResult i()
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            var currentId = CurrentUserId();
            Models.Message.iModels model = new Models.Message.iModels();
            model.Users = new List<SelectListItem>();
            var users = context.Members.Where(x => ((int)x.MemberType) > 0 && x.Id != currentId).ToList();
            model.Users = users.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = string.Format("{0} {1} ({2})", x.Name, x.Surname, x.MemberType.ToString())
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult SendMessage(Models.Message.SendMessageModel message)
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");

            DB.Messages mesaj = new DB.Messages()
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                IsRead = false,
                Subject = message.Subject,
               
            };
            var mRep = new DB.MessageReplies()
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                Member_Id = CurrentUserId(),
                Text = message.MessageBody
            };
            mesaj.MessageReplies.Add(mRep);
            context.Messages.Add(mesaj);
            context.SaveChanges();
            return RedirectToAction("i", "Message");

        }

    }


  
}



