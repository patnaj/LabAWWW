using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab2.Controllers
{
    // [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private Lab2.Data.ApplicationDbContext Db { get; }
        private CartModel cart = null;

        public CartController(ILogger<CartController> logger, Lab2.Data.ApplicationDbContext db)
        {
            this.Db = db;
            _logger = logger;


            cart = db.Carts.Include(a => a.Products).ThenInclude(b => b.Product).FirstOrDefault();
            if (cart == null)
            {
                cart = new Models.CartModel();
                db.Carts.Add(cart);
            }


        }

        public IActionResult Index()
        {

            return View(cart.Products);
        }

        [HttpPost]
        public IActionResult Add(int id, int amount = 1)
        {
            var item = cart.Products.FirstOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var product = Db.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                    return NotFound();
                cart.Products.Add(new CartItemModel() { Product = product });
            }
            item.Amount += amount;
            Db.SaveChanges();
            return Redirect("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = cart.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            cart.Products.Remove(product);
            Db.SaveChanges();
            return Redirect("index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}