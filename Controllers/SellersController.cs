using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService ss, DepartmentService ds) {
            this._sellerService = ss;
            this._departmentService = ds;
        }
        public IActionResult Index() {
            var list = _sellerService.findAll();
            return View(list);
        }

        public IActionResult Create() {
            var depts = _departmentService.findAll();
            var viewModel = new SellerFormViewModel { departments = depts };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            _sellerService.insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}