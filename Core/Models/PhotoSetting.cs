using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace carzz.Core.Models
{
    public class PhotoSetting
    {
        public int Max_Size { get; set; }

        public string[] Accepted_Types { get; set; }

        public bool IsAccepted(string fileName)
        {
            return Accepted_Types.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}
