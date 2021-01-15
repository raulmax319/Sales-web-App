using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SalesWebMvc.Controllers {
    public class SalesRecordsController : Controller {
        
        public IActionResult Index() => View();

        public IActionResult simpleSearch() => View();

        public IActionResult groupSearch() => View();
    }
}