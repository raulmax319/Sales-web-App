using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;


namespace SalesWebMvc.Controllers {
    public class SalesRecordsController : Controller {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService) {
            this._salesRecordService = salesRecordService;
        }
        
        public IActionResult Index() => View();

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate) {
            if(!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);
            if(!maxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minData"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxData"] = minDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.findByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate) {
            if(!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);
            if(!maxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minData"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxData"] = minDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.findByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}