using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Models
{
    public class MCPLink : Entity
    {
        public string MineInformationId { get; set; }
        public string SiteInformationId { get; set; }
        public string MCPBoardId { get; set; }
        public string Panel { get; set; }
        public string FunctionalLocationId { get; set; }
        public string Link { get; set; }
    }
}
