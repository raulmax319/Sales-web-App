using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services {
    public class SellerService {
        
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) {
            this._context = context;
        }

        public List<Seller> findAll() => _context.seller.ToList();

        public void insert(Seller obj) {
            obj.department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}