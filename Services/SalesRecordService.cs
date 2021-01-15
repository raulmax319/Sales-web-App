using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services {
    public class SalesRecordService {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context) {
            this._context = context;
        }

        public async Task<List<SalesRecord>> findByDateAsync(DateTime? minDate, DateTime? maxDate) {
            var result = from obj in _context.salesRecord select obj;

            if(minDate.HasValue) 
                result = result.Where(x => x.date >= minDate.Value);

            if(maxDate.HasValue)
                result = result.Where(x => x.date <= maxDate.Value);
            
            return await result
                .Include(x => x.seller)
                .Include(x => x.seller.department)
                .OrderByDescending(x => x.date)
                .ToListAsync();
        }
    }
}