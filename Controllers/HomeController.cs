using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace Lab2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        this.db = db;
        _logger = logger;
    }

public IActionResult Index(string q = "", int catid = 0)
{
    ViewBag.Q = q;
    ViewBag.catid = catid;
    ViewBag.cats = db.Catalogs.ToList();

    var pl = catid == 0 ? db.Products : db.Products.Where(p => p.CatlogId == catid);
    if (q != "")
        return View(pl.Include(i => i.Catalog).Where(p => p.Title.Contains(q)).ToList());
    return View(pl.Include(i => i.Catalog).ToList());
}

public IActionResult IndexCatalog()
{
    return View(db.Catalogs.ToList());
}

    [HttpGet]
    public IActionResult Add(int catid)
    {
        return View(new ProductModel() { CatlogId = catid });
    }
    [HttpPost]
    public IActionResult Add(ProductModel prod)
    {
        if (ModelState.IsValid)
        {
            db.Products.Add(prod);
            db.SaveChanges();
            return RedirectToAction("index", new { catid = prod.CatlogId });
        }
        return View(prod);
    }


    [HttpGet]
    public IActionResult AddCatalog()
    {
        return View(new CatalogModel() {  });
    }



[HttpGet]
public IActionResult Edit(int id)
{
    return View(db.Products.FirstOrDefault(p=>p.Id == id));
}
[HttpPost]
public IActionResult Edit(ProductModel prod)
{
    var p = db.Products.FirstOrDefault(p=>p.Id == prod.Id);
    if (ModelState.IsValid)
    {
        p.Title = prod.Title;
        p.CatlogId = prod.CatlogId;
        db.SaveChanges();
        return RedirectToAction("index", new { catid = prod.CatlogId });
    }
    return View(prod);
}

[HttpGet]
public IActionResult Delete(int id)
{
    var p = db.Products.FirstOrDefault(i => i.Id == id);
    return p == null ? RedirectToAction("index") : View(p);
}
[HttpPost]
public IActionResult Delete(int id, bool ok)
{
    var p = db.Products.FirstOrDefault(i => i.Id == id);
    if (p != null && ok)
    {
        // p.Title = "delete " + p.Title;
        db.Products.Remove(p);
        db.SaveChanges();
    }
    return RedirectToAction("index");
}



    public IActionResult Seed()
    {
        if (db.Products.FirstOrDefault() == null)
        {
            db.Catalogs.Add(new CatalogModel()
            {
                Title = "RTV",
                Products = new List<ProductModel>(){
                    new ProductModel(){Title="Telewizor 20'"},
                    new ProductModel(){Title="Telewizor 50'"}
                }
            });

            var cat = new CatalogModel()
            {
                Title = "AGD"
            };
            db.Catalogs.Add(cat);
            db.Products.Add(new ProductModel() { Title = "Lodowka", Catalog = cat });
            db.Products.Add(new ProductModel() { Title = "Pralka", Catalog = cat });
            db.SaveChanges();
        }

        // return Json(new {prod=db.Products.ToList(), cat=db.Catalogs.ToList()});
        return Json(db.Products.ToList());
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
