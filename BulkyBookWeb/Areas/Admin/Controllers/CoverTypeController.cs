using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utilities;
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
            IEnumerable<CoverType> coverTypes = unitOfWork.CoverTypeRepository.GetAll();
            return View(coverTypes);
        }
        [HttpGet]
        public IActionResult CreateCoverType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCoverType(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverTypeRepository.AddItem(coverType);
                unitOfWork.Save();
                TempData[Constants.TOASTR_SUCCESS] = "Cover type created";
                return RedirectToAction("Index", "CoverType");
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditCoverType(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            CoverType coverType = unitOfWork.CoverTypeRepository.GetItemByExpression(c => c.Id == id);
            if(coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }
        [HttpPost]
        public IActionResult EditCoverType(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverTypeRepository.UpdateCoverType(coverType);
                unitOfWork.Save();
                TempData[Constants.TOASTR_SUCCESS] = "Cover type updated";
                return RedirectToAction("Index", "CoverType");
            }
            return View();
        }
        [HttpGet]
        public IActionResult DeleteCoverType(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            CoverType coverType = unitOfWork.CoverTypeRepository.GetItemByExpression(c => c.Id == id);
            if(coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }
        [HttpPost]
        public IActionResult DeleteCoverType(CoverType coverType)
        {
            unitOfWork.CoverTypeRepository.RemoveItem(coverType);
            unitOfWork.Save();
            TempData[Constants.TOASTR_SUCCESS] = "Cover type deleted";
            return RedirectToAction("Index", "CoverType");
        }
    }
}
