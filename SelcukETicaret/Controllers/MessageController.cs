using SelcukETicaret.Models.Message;
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
            #region -Select List Item-
            model.Users = new List<SelectListItem>();
            var users = context.Members.Where(x => ((int)x.MemberType) > 0 && x.Id != currentId).ToList();
            model.Users = users.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = string.Format("{0} {1} ({2})", x.Name, x.Surname, x.MemberType.ToString())
            }).ToList();
            #endregion
            #region -Mesaj Listesi-
            var mList = context.Messages.Where(x => x.ToMemberId == currentId || x.MessageReplies.Any(y=>y.Member_Id==currentId)).ToList();
            model.Messages = mList;
            #endregion
            return View(model);
        }
        [HttpGet]
        public ActionResult SendMessage(Models.Message.SendMessageModel message)
                             {
            if (IsLogon() == false) return RedirectToAction("index", "i");

            DB.Messages mesaj = new DB.Messages()
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                IsRead = false,
                Subject = message.Subject,
                ToMemberId = message.ToUserId,
               
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
            return RedirectToAction("i","Message");

        }
        [HttpGet]
        public ActionResult MessageReplies(string id)
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            MessageRepliesModel model = new MessageRepliesModel();
            var guid = new Guid(id);
            model.MReplies = context.MessageReplies.Where(X => X.MessageId == guid).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult MessageReplies(DB.MessageReplies message)
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            //MessageRepliesModel model = new MessageRepliesModel();
            message.AddedDate = DateTime.Now;
            message.Id = Guid.NewGuid();
            message.Member_Id = CurrentUserId();
            context.MessageReplies.Add(message);
            context.SaveChanges();
            return RedirectToAction("MessageReplies","Message",new {id=message.MessageId });
        }

    }


  
}



