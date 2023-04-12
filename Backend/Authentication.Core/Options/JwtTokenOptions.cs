using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Options
{
    public class JwtTokenOptions
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
        public int RefreshTokenMaximumLifeSpanDays { get; set; }
        public int AccessTokenExpirationMin { get; set; }

        public int RefreshTokenExpirationMin { get; set; }

    }
}
