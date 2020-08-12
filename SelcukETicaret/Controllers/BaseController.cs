using SelcukETicaret.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SelcukETicaret.Controllers
{
    public class BaseController : Controller
    {
        protected ETicaretDBEntities context { get; private set; }
        public BaseController()
        {
            context = new ETicaretDBEntities();
            ViewBag.MenuCategories = context.Categories.Where(x => x.Parent_Id == null).ToList();
        }
        protected DB.Members CurentUser()
        {
            if (Session["LogonUser"] == null) return null;
            return (DB.Members)Session["LogonUser"];
        }
        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null) return 0;
            return ((DB.Members)Session["LogonUser"]).Id;
        }
    }
}


