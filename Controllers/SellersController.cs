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
        public async Task<IActionResult> Index() {
            var list = await _sellerService.findAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create() {
            var depts = await _departmentService.findAllAsync();
            var viewModel = new SellerFormViewModel { departments = depts };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) {
            if(!ModelState.IsValid) {
                var depts = await _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { seller = seller, departments = depts };
                return View(viewModel);
            }
            
            await _sellerService.insertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });

            var obj = await _sellerService.findByIdAsync(id.Value);
            
            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            try {
                await _sellerService.removeAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });

            var obj = await _sellerService.findByIdAsync(id.Value);
            
            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "ID not provided" });

            var obj = await _sellerService.findByIdAsync(id.Value);

            if(obj == null) return RedirectToAction(nameof(Error), new { message = "ID not found" });

            List<Department> depts = await _departmentService.findAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { seller = obj, departments = depts };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller) {
            if(!ModelState.IsValid) {
                var depts = await _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { seller = seller, departments = depts };
                return View(viewModel);
            }

            if(id != seller.id) return RedirectToAction(nameof(Error), new { message = "ID mismatch" });

            try {
                await _sellerService.updateAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException err) {
                return RedirectToAction(nameof(Error), new { message = err.Message });
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