using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.MCPLinks.Request
{
    public class MCPLinkDTO
    {
        public string Id { get; set; }
        public string MineInformationId { get; set; }
        public string SiteInformationId { get; set; }
        public string MCPBoardId { get; set; }
        public string Panel { get; set; }
        public string FunctionalLocationId { get; set; }
        public string Link { get; set; }
    }
}
