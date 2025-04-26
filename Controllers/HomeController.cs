using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Humanizer;
using System.Threading.Tasks;

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
        ViewBag.Tags = db.Tags.ToList();

        var pl = catid == 0 ? db.Products : db.Products.Where(p => p.CatlogId == catid);
        if (q != "")
            // l.Include(i => i.Catalog) -> wypełnia referencje Catalog obiektami inaczej jest null (odatkowe selekty i mapowiania do bazy danych)
            return View(pl.Include(i => i.Catalog).Where(p => p.Title.Contains(q) || p.Tags.Contains(q)).ToList());
        return View(pl.Include(i => i.Catalog).ToList());
    }

    public IActionResult IndexCatalog()
    {
        return View(db.Catalogs.ToList());
    }

    [HttpGet]
    public IActionResult Add(int catid, int id = 0)
    {

        var prod = db.Products.FirstOrDefault(i => i.Id == id) ?? new ProductModel() { CatlogId = catid };
        return View(prod);
    }
    [HttpPost]
    public async Task<IActionResult> Add(ProductModel prod)
    {
        if (ModelState.IsValid)
        {
            if (prod.Id == 0)
            {
                db.Products.Add(prod);
            }
            else
            {
                var tmp = db.Products.FirstOrDefault(i => i.Id == prod.Id);
                if (tmp == null)
                    return NotFound();
                tmp.Price = prod.Price;
                tmp.Title = prod.Title;
                tmp.Description = prod.Description;
                tmp.Tags = prod.Tags;
                //tagi
                var tags = (prod.Tags ?? "").Split(",", StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                var db_tags = db.Tags.Where(i => tags.Contains(i.Title)).Select(t=>t.Title);
                db.Tags.AddRange(tags.Except(db_tags).Select(i => new TagModel() { Title = i }));
            }
            db.SaveChanges();
            return RedirectToAction("index", new { catid = prod.CatlogId });
        }
        return View(prod);
    }


    [HttpGet]
    public IActionResult AddCatalog()
    {
        return View(new CatalogModel() { });
    }

    [HttpPost]
    public IActionResult AddCatalog(CatalogModel catalog)
    {
        if (ModelState.IsValid)
        {
            db.Catalogs.Add(catalog);
            db.SaveChanges();
            return RedirectToAction("index", new { catid = catalog.Id });
        }
        return View(catalog);
    }



    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(db.Products.FirstOrDefault(p => p.Id == id));
    }
    [HttpPost]
    public IActionResult Edit(ProductModel prod)
    {
        var p = db.Products.FirstOrDefault(p => p.Id == prod.Id);
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
