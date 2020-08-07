using SelcukETicaret.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelcukETicaret.Controllers
{
    public class iController : Controller
    {
        ETicaretEntities context;
        public iController()
        {
            context = new ETicaretEntities();
        }
        // GET: i
        public ActionResult Index()
        {
            return View();
        }
    }
}
