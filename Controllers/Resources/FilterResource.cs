﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace carzz.Controllers.Resources
{
    public class FilterResource
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? Year { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
    }
}
