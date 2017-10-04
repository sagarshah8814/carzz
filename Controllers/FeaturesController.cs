using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core.Models;
using carzz.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace carzz.Controllers
{
    public class FeaturesController:Controller
    {
        private readonly CarzzDbContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(CarzzDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();
            return _mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
        }
    }
}
