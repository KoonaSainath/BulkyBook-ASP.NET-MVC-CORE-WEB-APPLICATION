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
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
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
        public IActionResult UpsertProduct(ProductVM productVm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string fileFullName = fileName + extension;
                    string subFolderPath = @"\Images\Products\";
                    string location = wwwRootPath + subFolderPath + fileFullName;

                    using (FileStream fileStream = new FileStream(location, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVm.Product.ImageUrl = subFolderPath + fileFullName;
                    unitOfWork.ProductRepository.AddItem(productVm.Product);
                    unitOfWork.Save();
                }
                return RedirectToAction("Index", "Product");

            }
            return View();
        }

        #region API Endpoints
        public IActionResult GetAllProducts()
        {
            IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll("Category,CoverType");
            return Json(new { data = products });
        }
        #endregion
    }
}
