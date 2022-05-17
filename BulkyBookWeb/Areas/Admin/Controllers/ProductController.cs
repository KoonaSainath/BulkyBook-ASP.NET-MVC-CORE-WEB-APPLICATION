using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utilities;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

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
        public IActionResult UpsertProduct(int id)
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

            if(id != 0)
            {
                //update
                productVm.Product = unitOfWork.ProductRepository.GetItemByExpression(p => p.Id == id);
            }
            return View(productVm);
        }
        [HttpPost]
        public IActionResult UpsertProduct(ProductVM productVm, IFormFile file)
        {
            ModelState.Remove("file");
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string fileFullName = fileName + extension;
                    string subFolderPath = @"\Images\Products\";
                    string location = wwwRootPath + subFolderPath + fileFullName;

                    if(productVm.Product.ImageUrl != null)
                    {
                        //deleting previous image as if imageurl is not null, it would be update.
                        productVm.Product.ImageUrl = productVm.Product.ImageUrl.TrimStart('\\');
                        string imageLocation = Path.Combine(wwwRootPath, productVm.Product.ImageUrl);
                        if (System.IO.File.Exists(imageLocation))
                        {
                            System.IO.File.Delete(imageLocation);
                        }
                    }

                    using (FileStream fileStream = new FileStream(location, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVm.Product.ImageUrl = subFolderPath + fileFullName;
                    if(productVm.Product.Id != 0)
                    {
                        unitOfWork.ProductRepository.UpSert(productVm.Product);
                        TempData[Constants.TOASTR_SUCCESS] = "Product updated";
                    }
                    else
                    {
                        unitOfWork.ProductRepository.AddItem(productVm.Product);
                        TempData[Constants.TOASTR_SUCCESS] = "Product created";
                    }
                    unitOfWork.Save();
                }
                if(file == null && productVm.Product.Id != 0)
                {
                    unitOfWork.ProductRepository.UpSert(productVm.Product);
                    TempData[Constants.TOASTR_SUCCESS] = "Product updated";
                    unitOfWork.Save();
                }
                return RedirectToAction("Index", "Product");

            }
            return View();
        }

        #region API Endpoints
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll("Category,CoverType");
            return Json(new { data = products });
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            Product product = unitOfWork.ProductRepository.GetItemByExpression(p => p.Id == id);
            if(product != null)
            {
                product.ImageUrl = product.ImageUrl.TrimStart('\\');
                string imageLocation = Path.Combine(webHostEnvironment.WebRootPath, product.ImageUrl);
                if (System.IO.File.Exists(imageLocation))
                {
                    System.IO.File.Delete(imageLocation);
                }
                unitOfWork.ProductRepository.RemoveItem(product);
                unitOfWork.Save();
                return Json(new { success = true, message = "Product deleted successfully" });
            }
            return Json(new { success = false, message = "Error while deleting the product" });
        }
        #endregion
    }
}
