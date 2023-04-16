using Authentication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.MineInformations.Response
{
    public class MineInformationsList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<string> SiteInformationIds { get; set; }
        public List<SiteInformation> SiteInformationList { get; set; }
    }
}
