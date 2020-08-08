using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyETicaret.DB;

namespace UdemyETicaret.Controllers
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
            var viewModel = new Models.i.IndexModel()
            {
                Products = context.Products.ToList()
            };
            return View(viewModel);
        }
    }
}