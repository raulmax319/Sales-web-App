using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services {
    public class SellerService {
        
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) {
            this._context = context;
        }

        public List<Seller> findAll() => _context.seller.ToList();

        public void insert(Seller obj) {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller findById(int id) => _context.seller.Include(o => o.department).FirstOrDefault(o => o.id == id);

        public void remove(int id) {
            var obj = _context.seller.Find(id);
            _context.seller.Remove(obj);
            _context.SaveChanges();
        }

        public void update(Seller obj) {
            if(!_context.seller.Any(x => x.id == obj.id)) throw new NotFoundException("Id not found");

            try {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}