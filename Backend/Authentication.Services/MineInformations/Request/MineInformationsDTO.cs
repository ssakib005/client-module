﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.MineInformations.Request
{
    public class MineInformationsDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Image { get; set; }
        public List<string> SiteInformationIds { get; set; }
    }
}
