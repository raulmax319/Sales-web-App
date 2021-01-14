using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models.ViewModels {
    public class SellerFormViewModel {
        public Seller seller { get; set; }
        public ICollection<Department> departments { get; set; }
    }
}