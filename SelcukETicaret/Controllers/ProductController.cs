using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelcukETicaret.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult i()
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            else if (((int)(CurentUser().MemberType))<4)
            {
                return RedirectToAction("index", "i");
            }
            var products = context.Products.ToList();
            return View(products);
        }
    }
}