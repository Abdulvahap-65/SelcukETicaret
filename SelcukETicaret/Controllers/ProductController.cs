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
        public ActionResult Edit(int id)
        {
            var product = context.Products.FirstOrDefault(x=>x.Id==id);
            var categories = context.Categories.Select(x => new SelectListItem() {
Text=x.Name,
Value=x.Id.ToString()
            }).ToList();
            ViewBag.Categories = categories;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(DB.Products product)
        {
            product.Description = string.Empty;//Ders 315'te ürünler sayfası boş geliyordu.Bu sorunnu gider.Giderince kontrol et.Ürnler>düzenle>Açıklama kısmı dolu gelecek bu durumda.O zaman dene işe yararsa bu satırı kaldır.
            product.ProductImageName = string.Empty;
            if (product.Id>0)
            {
                var dbProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);
                dbProduct.Category_Id = product.Category_Id;
                dbProduct.ModifiedDate = DateTime.Now;
                dbProduct.Description = product.Description;
                dbProduct.IsContinued = product.IsContinued;
                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.UnitsInStock = product.UnitsInStock;
            }
            else
            {
                product.AddedDate = DateTime.Now;
                context.Entry(product).State = System.Data.Entity.EntityState.Added;
            }
            context.SaveChanges();
            return RedirectToAction("i");
        }
    }
}