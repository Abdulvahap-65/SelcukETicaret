using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UdemyETicaret.DB;
using UdemyETicaret.Models;
using UdemyETicaret.Models.i;

namespace UdemyETicaret.Controllers
{
    public class iController : BaseController
    {
        // GET: i
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            IQueryable<DB.Products> products = context.Products;
            DB.Categories category = null;
            if (id > 0)
            {
                products = products.Where(x => x.Category_Id == id);
                category = context.Categories.FirstOrDefault(x => x.Id == id);
            }
            var viewModel = new Models.i.IndexModel()
            {
                Products = products.ToList(),
                Category = category
            };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Product(int id = 0)
        {
            var pro = context.Products.FirstOrDefault(x => x.Id == id);

            if (pro == null) return RedirectToAction("index", "i");

            ProductModels model = new ProductModels()
            {
                Product = pro,
                Comments = pro.Comments.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Product(DB.Comments comment)
        {
            try
            {
                comment.Member_Id = base.CurrentUserId();
                comment.AddedDate = DateTime.Now;
                context.Comments.Add(comment);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.MyError = ex.Message;
            }
            return RedirectToAction("Product", "i");
        }
        [HttpGet]
        public ActionResult AddBasket(int id, bool remove = false)
        {
            List<Models.i.BasketModels> basket = null;
            if (Session["Basket"] == null)
            {
                basket = new List<Models.i.BasketModels>();
            }
            else
            {
                basket = (List<Models.i.BasketModels>)Session["Basket"];
            }

            if (basket.Any(x => x.Product.Id == id))
            {
                var pro = basket.FirstOrDefault(x => x.Product.Id == id);
                if (remove && pro.Count > 0)
                {
                    pro.Count -= 1;
                }
                else
                {
                    if (pro.Product.UnitsInStock > pro.Count)
                    {
                        pro.Count += 1;
                    }
                    else
                    {
                        ViewBag.MyError = "Yeterli Stok yok";
                    }
                }

            }
            else
            {
                var pro = context.Products.FirstOrDefault(x => x.Id == id);
                if (pro != null && pro.IsContinued)
                {
                    basket.Add(new Models.i.BasketModels()
                    {
                        Count = 1,
                        Product = pro
                    });
                }
                else if (pro != null && pro.IsContinued == false)
                {
                    ViewBag.MyError = "Bu ürünün satışı durduruldu.";
                }
            }
            basket.RemoveAll(x => x.Count < 1);
            Session["Basket"] = basket;

            return RedirectToAction("Basket", "i");
        }
        [HttpGet]
        public ActionResult Basket()
        {
            List<Models.i.BasketModels> model = (List<Models.i.BasketModels>)Session["Basket"];
            if (model == null)
            {
                model = new List<Models.i.BasketModels>();
            }

            ViewBag.TotalPrice = model.Select(x => x.Product.Price * x.Count).Sum();

            return View(model);
        }
        [HttpGet]
        public ActionResult RemoveBasket(int id)
        {
            List<Models.i.BasketModels> basket = (List<Models.i.BasketModels>)Session["Basket"];
            if (basket != null)
            {
                if (id > 0)
                {
                    basket.RemoveAll(x => x.Product.Id == id);
                }
                else if (id == 0)
                {
                    basket.Clear();
                }
                Session["Basket"] = basket;
            }
            return RedirectToAction("Basket", "i");
        }
    }
}