using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab2.Controllers
{
    // [Route("[controller]")]
    [Authorize(Roles ="admin")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly Lab2.Data.ApplicationDbContext db;

        public OrderController(ILogger<OrderController> logger, Lab2.Data.ApplicationDbContext db)
        {
            this.db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            // return View(db.Carts.Include(i=>i.User).Include(i=>i.Products).ThenInclude(i=>i.Product).Where(i=>i.OrderDate  != null));
            return Json(db.Carts.Include(i=>i.User).Include(i=>i.Products).ThenInclude(i=>i.Product).Where(i=>i.OrderDate  != null));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}