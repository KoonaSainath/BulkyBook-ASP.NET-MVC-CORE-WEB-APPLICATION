using BulkyBook.DataAccessLayer.Repository;
using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Utilities;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = unitOfWork.CategoryRepository.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            //Custom validation (server-side)
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Please ensure that category name is different from display order");
                ModelState.AddModelError("DisplayOrder", "Please ensure that display order is different from category name");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.AddItem(category);
                unitOfWork.Save();
                string toastrMessage = $"Created {category.Name} category successfully";
                TempData[Constants.TOASTR_SUCCESS] = toastrMessage;
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = unitOfWork.CategoryRepository.GetItemByExpression(c => c.Id == id);
            //Category categorySingle = db.Categories.SingleOrDefault(c => c.Id == id);
            //Category categoryFirst = db.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category name cannot be same as display order");
                ModelState.AddModelError("DisplayOrder", "Display order cannot be same as category name");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.UpdateCategory(category);
                unitOfWork.Save();
                string toastrMessage = $"Edited {category.Name} category successfully";
                TempData[Constants.TOASTR_SUCCESS] = toastrMessage;
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpGet, ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryGet(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = unitOfWork.CategoryRepository.GetItemByExpression(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategoryPost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = unitOfWork.CategoryRepository.GetItemByExpression(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            unitOfWork.CategoryRepository.RemoveItem(category);
            unitOfWork.Save();
            string toastrMessage = $"Deleted {category.Name} category successfully";
            TempData[Constants.TOASTR_SUCCESS] = toastrMessage;
            return RedirectToAction("Index", "Category");
        }
    }
}
