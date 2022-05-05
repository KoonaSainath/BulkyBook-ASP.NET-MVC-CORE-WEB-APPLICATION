using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
