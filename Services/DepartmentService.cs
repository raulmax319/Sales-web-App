using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services {
    public class DepartmentService {
        
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context) {
            this._context = context;
        }

        public async Task<List<Department>> findAllAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}