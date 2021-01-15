using System;
using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models {
    public class SalesRecord {
        public int id { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double amount { get; set; }
        public SalesStatus status { get; set; }
        public Seller seller { get; set; }

        public SalesRecord() {
        }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller) {
            this.id = id;
            this.date = date;
            this.amount = amount;
            this.status = status;
            this.seller = seller;
        }
    }
}