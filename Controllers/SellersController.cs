using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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

        public IActionResult Delete(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });

            var obj = _sellerService.findById(id.Value);
            
            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });;

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _sellerService.remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });;

            var obj = _sellerService.findById(id.Value);
            
            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });;

            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });;

            var obj = _sellerService.findById(id.Value);

            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });;

            List<Department> depts = _departmentService.findAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { seller = obj, departments = depts };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            if(id != seller.id) return RedirectToAction(nameof(Error), new { message = "ID mismatch" });;

            try {
                _sellerService.update(seller);

                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException err) {
                return RedirectToAction(nameof(Error), new { message = err.Message });;
            }
        }

        public IActionResult Error(string msg) {
            var viewModel = new ErrorViewModel {
                Message = msg,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}