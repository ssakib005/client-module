using Authentication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.SiteInformations.Response
{
    public class SiteInformationsList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FunctionalLocationId { get; set; }
        public List<string> FunctionalLocationList { get; set; }
    }
}
