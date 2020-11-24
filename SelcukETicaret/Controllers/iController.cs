using SelcukETicaret.DB;
using SelcukETicaret.Models.i;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelcukETicaret.Models;


namespace SelcukETicaret.Controllers
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

            if (pro == null) return RedirectToAction("Index", "i");

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
                        TempData["MyError"]= "Yeterli Stok yok";
                    }
                }

            }
            else
            {
                var pro = context.Products.FirstOrDefault(x => x.Id == id);
                if (pro != null && pro.IsContinued && pro.UnitsInStock > 0)
                {
                    basket.Add(new Models.i.BasketModels()
                    {
                        Count = 1,
                        Product = pro
                    });
                }
                else if (pro != null && pro.IsContinued == false)
                {
                    TempData["MyError"] = "Bu ürünün satışı durduruldu.";
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
            if (base.IsLogon())
            {
                int currentId = CurrentUserId();
                ViewBag.CurrentAddresses = context.Addresses
                                            .Where(x => x.Member_Id == currentId)
                                            .Select(x => new SelectListItem()
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString()
                                            }).ToList();
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

        [HttpPost]
        public ActionResult Buy(string Address)
        {
            if (IsLogon())
            {
                try
                {
                    var basket = (List<Models.i.BasketModels>)Session["Basket"];
                    var guid = new Guid(Address);
                    var _address = context.Addresses.FirstOrDefault(x => x.Id == guid);
                    //Sipariş Verildi = SV
                    //Ödeme Bildirimi = OB
                    //Ödeme Onaylandı = OO

                    var order = new DB.Orders()
                    {
                        AddedDate = DateTime.Now,
                        Address = _address.AdresDescription,
                        Member_Id = CurrentUserId(),
                        Status = "SV",
                        Id=Guid.NewGuid()
                    };
                    //5
                    //ahmet 5
                    //mehmet 5
                    foreach (Models.i.BasketModels item in basket)
                    {
                        var oDetail = new DB.OrderDetails();
                        oDetail.AddedDate = DateTime.Now;
                        oDetail.Price = item.Product.Price * item.Count;
                        oDetail.Product_Id = item.Product.Id;
                        oDetail.Quantity = item.Count;
                        oDetail.Id = Guid.NewGuid();

                        order.OrderDetails.Add(oDetail);

                        var _product = context.Products.FirstOrDefault(x => x.Id == item.Product.Id);
                        if (_product != null && _product.UnitsInStock >= item.Count)
                        {
                            _product.UnitsInStock = _product.UnitsInStock - item.Count;
                        }
                        else
                        {
                            throw new Exception(string.Format("{0} ürünü için yeterli stok yoktur veya silinmiş bir ürünü almaya çalışıyorsunuz.", item.Product.Name));
                        }
                    }
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.MyError = ex.Message;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpGet]
        public ActionResult Buy()
        {
            if (IsLogon())
            {
                var currentId = CurrentUserId();
                var orders = context.Orders.Where(x => x.Member_Id == currentId);
                List<Models.i.BuyModels> model = new List<BuyModels>();
                foreach (var item in orders)
                {
                    var byModel = new BuyModels();
                    byModel.TotelPrice = item.OrderDetails.Sum(y=>y.Price);
                    byModel.OrderName = string.Join(",", item.OrderDetails.Select(y => y.Products.Name + "(" + y.Quantity + ")"));

                    model.Add(byModel);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


    }

}




