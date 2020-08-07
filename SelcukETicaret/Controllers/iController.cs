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
        ETicaretDBEntities context;
        public iController()
        {
            context = new ETicaretDBEntities();
        }
        // GET: i
        public ActionResult Index(int? id)
        {

            IQueryable<DB.Products> products = context.Products;
            DB.Categories category = null;
            if (id.HasValue)
            {
                products = products.Where(x => x.Category_Id == id);
                category = context.Categories.FirstOrDefault(x=>x.Id==id);
            
            }
            var viewModel = new Models.i.IndexModel()
            {
                Products = context.Products.ToList(),
                Category=category
            };
            return View(viewModel);
        }
    }
}



//public ActionResult Index(int id)
//{
//    IQueryable<DB.Products> products = context.Products;
//    DB.Categories category = null;
//    if (id > 0)
//    {
//        products = products.Where(x => x.Category_Id == id);
//        category = context.Categories.FirstOrDefault(x => x.Id == id);
//    }
//    var viewModel = new Models.i.IndexModel()
//    {
//        Products = products.ToList(),
//        Category = category
//    };
//    return View(viewModel);
//}