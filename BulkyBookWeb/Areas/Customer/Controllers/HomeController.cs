using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll(includeTheseNavigationProperties: "Category,CoverType");
            return View(products);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Product product = unitOfWork.ProductRepository.GetItemByExpression(p => p.Id == id, includeTheseNavigationProperties: "Category,CoverType");
            ShoppingCart cart = new ShoppingCart()
            {
                Product = product
            };
            return View(cart);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}