using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core.Models;
using carzz.Persistence;
using Microsoft.EntityFrameworkCore;

namespace carzz.Controllers
{
    public class MakesController:Controller
    {
        private readonly CarzzDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(CarzzDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakesAsync()
        {
            var makes= await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}
