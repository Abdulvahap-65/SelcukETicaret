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
        protected bool IsLogon()
        {
            if (Session["LogonUser"] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Tüm alt kategorileri getirir.
        /// </summary>
        /// <param name="cat">Hangi kategorinin alt kategorilerini getirsin?</param>
        /// <returns></returns>
        protected List<Categories> GetChildCategories(Categories cat)
        {
            var result = new List<Categories>();
            result.AddRange(cat.Categories1);
            foreach (var item in cat.Categories1)
            {
                var list = GetChildCategories(item);
                result.AddRange(list);
            }
            return result;

        }
    }
}


