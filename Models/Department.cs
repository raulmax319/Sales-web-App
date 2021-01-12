using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models {
    public class Department {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> sellers { get; set; } = new List<Seller>();

        public Department() {
        }

        public Department(int id, string name) {
            this.Id = id;
            this.Name = name;
        }

        public void addSeller(Seller seller) => sellers.Add(seller);

        public double totalSales(DateTime initial, DateTime final) => sellers.Sum(
            seller => seller.totalSales(initial, final));
    }
}