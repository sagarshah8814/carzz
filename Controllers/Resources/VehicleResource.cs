using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace carzz.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Odometer { get; set; }
        public ContactResource Contact { get; set; }
        public int Price { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }

        public VehicleResource()
        {
            Features= new Collection<KeyValuePairResource>();
        }
    }
}
