using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = db.Categories;
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            //Custom validation (server-side)
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Please ensure that category name is different from display order");
                ModelState.AddModelError("DisplayOrder", "Please ensure that display order is different from category name");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
