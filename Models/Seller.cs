using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models {
    public class Seller {
        public int id { get; private set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime birthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required]
        [Range(100.0, 50000.0)]
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