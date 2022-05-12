using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpsertProduct()
        {
            ProductVM productVm = new ProductVM();
            productVm.Product = new Product();
            IEnumerable<SelectListItem> categoriesSelectListItems = unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            IEnumerable<SelectListItem> coverTypesSelectListItems = unitOfWork.CoverTypeRepository.GetAll()
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            //ViewBag.Categories = categoriesSelectListItems;
            //ViewData["CoverTypes"] = coverTypesSelectListItems;
            productVm.CategorySelectListItems = categoriesSelectListItems;
            productVm.CoverTypeSelectListItems = coverTypesSelectListItems;
            return View(productVm);
        }
        [HttpPost]
        public IActionResult UpsertProduct(ProductVM productVm)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
