using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.MCPLinks.Response
{
    public class MCPLinkList
    {
        public string Id { get; set; }
        public string MineInformationId { get; set; }
        public string MineInformationName { get; set; }
        public string SiteInformationId { get; set; }
        public string SiteInformationName { get; set; }
        public string MCPBoardId { get; set; }
        public string MCPBoardName { get; set; }
        public string Panel { get; set; }
        public string FunctionalLocationId { get; set; }
        public string FunctionalLocationName { get; set; }
        public string Link { get; set; }
    }
}
