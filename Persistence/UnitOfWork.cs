using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carzz.Core;

namespace carzz.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarzzDbContext _context;

        public UnitOfWork(CarzzDbContext context)
        {
            _context = context;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
