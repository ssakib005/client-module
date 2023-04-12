using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.Interfaces
{
    public interface ISessionService
    {
        string GetUserId();
    }
}
