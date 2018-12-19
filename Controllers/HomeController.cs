using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using prodandcat.Models;

namespace prodandcat.Controllers
{
    public class HomeController : Controller
    {
        private prodandcatContext dbContext;

        public HomeController(prodandcatContext context)
        {
            dbContext = context;
        }


        [HttpGet("")]
        [HttpGet("Products")]
        public IActionResult Products()
        {
            List<Product> Products = dbContext.Products.ToList();
            ViewBag.Products = Products;
            return View();
        }

        [HttpPost("Products")]
        public IActionResult NewProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                List<Product> Products = dbContext.Products.ToList();
                ViewBag.Products = Products;
                return View("Products");
            }
        }

        [HttpGet("Categories")]
        public IActionResult Categories()
        {
            List<Category> Categories = dbContext.Categories.ToList();
            ViewBag.Categories = Categories;
            return View();
        }

        [HttpPost("Categories")]
        public IActionResult NewCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            else
            {
                List<Category> Categories = dbContext.Categories.ToList();
                ViewBag.Categories = Categories;
                return View("Categories");
            }
        }

        [HttpGet("Products/{ProdId}")]
        public IActionResult ProductId(int ProdId)
        {
            Product Product = dbContext.Products.SingleOrDefault(prod => prod.ProductId == ProdId);
            var Categories = dbContext.Products.Include(a => a.Associations).ThenInclude(cat => cat.Categories).Where(cat => cat.ProductId == ProdId).ToList();
            List<Category> getRid = new List<Category>();
            foreach (var x in Categories)
            {
                foreach (var y in x.Associations)
                {
                    Category nope = dbContext.Categories.Where(c => c.CategoryId == y.Categories.CategoryId).FirstOrDefault();
                    getRid.Add(nope);
                }
            }
            List<Category> NonCategories = dbContext.Categories.Except(getRid).ToList();
            ViewBag.Product = Product;
            ViewBag.Categories = Categories;
            ViewBag.NonCategories = NonCategories;
            return View();
        }

        [HttpGet("Categories/{CatId}")]
        public IActionResult CategoryId(int CatId)
        {
            Category Category = dbContext.Categories.SingleOrDefault(cat => cat.CategoryId == CatId);
            var Products = dbContext.Categories.Include(a => a.Associations).ThenInclude(prod => prod.Products).Where(prod => prod.CategoryId == CatId).ToList();
            List<Product> getRid = new List<Product>();
            foreach (var x in Products)
            {
                foreach (var y in x.Associations)
                {
                    Product nope = dbContext.Products.Where(c => c.ProductId == y.Products.ProductId).FirstOrDefault();
                    getRid.Add(nope);
                }
            }
            List<Product> NonProducts = dbContext.Products.Except(getRid).ToList();
            ViewBag.Category = Category;
            ViewBag.Products = Products;
            ViewBag.NonProducts = NonProducts;
            return View();
        }

        [HttpPost("ProductAssociation")]
        public IActionResult ProductAssociation(Associations association)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(association);
                dbContext.SaveChanges();
                return Redirect("/Products/" + association.ProductId);
            }
            else
            {
                return Redirect("/Products/" + association.ProductId);
            }
        }

        [HttpPost("CategoryAssociation")]
        public IActionResult CategoryAssociation(Associations association)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(association);
                dbContext.SaveChanges();
                return Redirect("/Categories/" + association.CategoryId);
            }
            else
            {
                return Redirect("/Categories/" + association.CategoryId);
            }
        }
    }
}