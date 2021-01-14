using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models {
    public class Seller {
        public int id { get; private set; }
        public string name { get; set; }
        public string email { get; set; }
        public DateTime birthDate { get; set; }
        public double baseSalary { get; set; }
        public Department department { get; set; }
        public int departmentId { get; set; }
        public ICollection<SalesRecord> sales { get; set; } = new List<SalesRecord>();

        public Seller() {
        }

        public Seller(int id, string name, string email, DateTime birthD, double baseSalary, Department dep) {
            this.id = id;
            this.name = name;
            this.email = email;
            this.birthDate = birthD;
            this.baseSalary = baseSalary;
            this.department = dep;
        }

        public void addSales(SalesRecord sr) => sales.Add(sr);

        public void removeSales(SalesRecord sr) => sales.Remove(sr);

        public double totalSales(DateTime initial, DateTime final) => sales.Where(
                s => s.date >= initial && s.date <= final)
                    .Sum(s => s.amount);
   
    }
}