using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Options
{
    public class ApiConfigurationOptions
    {
        public MongoDbOptions MongoDb { get; set; }
        public JwtTokenOptions JwtToken { get; set; }

        

    }
}
