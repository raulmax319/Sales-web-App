using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers {
    public class DepartmentsController : Controller {

        public IActionResult Index() {
            List<Department> list = new List<Department>();
            list.Add(new Department { id = 1, name = "Eletronics" });
            list.Add(new Department { id = 2, name = "Fashion" });
            list.Add(new Department { id = 3, name = "RH" });

            return View(list);
        }
    }
}