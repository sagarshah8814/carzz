using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace carzz.Extensions
{
   public interface ISortingObjects
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
    }
}
