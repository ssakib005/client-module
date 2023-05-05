using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Models
{
    public class SiteInformation : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public string FileName { get; set; }
        //public string FilePath { get; set; }
        public List<string> FunctionalLocationIds { get; set; }

    }
}
