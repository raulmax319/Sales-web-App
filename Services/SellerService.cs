using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services {
    public class SellerService {
        
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) {
            this._context = context;
        }

        public async Task<List<Seller>> findAllAsync() => await _context.seller.ToListAsync();

        public async Task insertAsync(Seller obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> findByIdAsync(int id) => await _context.seller.Include(o => o.department).FirstOrDefaultAsync(o => o.id == id);

        public async Task removeAsync(int id) {
            var obj = await _context.seller.FindAsync(id);
            _context.seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task updateAsync(Seller obj) {
            if(!await _context.seller.AnyAsync(x => x.id == obj.id)) throw new NotFoundException("Id not found");

            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}