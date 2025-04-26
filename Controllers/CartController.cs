using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab2.Controllers
{
    // [Route("[controller]")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private Lab2.Data.ApplicationDbContext Db { get; }

        private CartModel _cart = null;
        private CartModel cart
        {
            get
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (_cart == null)
                {
                    _cart = Db.Carts.Include(a => a.Products).ThenInclude(b => b.Product).FirstOrDefault(i => i.UserId == userid);
                    if (_cart == null)
                    {
                        _cart = new Models.CartModel() { UserId = userid };
                        Db.Carts.Add(_cart);
                    }
                }
                return _cart;
            }
        }

        public CartController(ILogger<CartController> logger, Lab2.Data.ApplicationDbContext db)
        {
            this.Db = db;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View(cart.Products);
        }

        [HttpGet]
        public IActionResult Address()
        {
            return View(cart);
        }

        [HttpPost]
        public IActionResult Address(CartModel cart)
        {
            if (ModelState.IsValid)
            {

                foreach (var p in this.cart.GetType().GetProperties())
                {
                    if (p.GetAccessors().FirstOrDefault()?.IsVirtual == false && !Attribute.IsDefined(p, typeof(KeyAttribute)))
                        p.SetValue(this.cart, p.GetValue(cart));
                }
                return RedirectToAction("Index", "Home");
            }
            return View(cart);
        }

        [HttpGet]
        public IActionResult Order(CartModel cart)
        {
            return View(cart);
        }

        [HttpPost]
        public IActionResult Order(bool ok)
        {
            Db.Carts.Remove(this.cart);
            return RedirectToAction("Index", "Home");
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
                item = new CartItemModel() { Product = product };
                cart.Products.Add(item);
            }
            item.Amount += amount;
            if (item.Amount < 0) item.Amount = 0;
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