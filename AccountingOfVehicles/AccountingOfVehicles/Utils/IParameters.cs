using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Utils
{
   public interface IParameters
    {
        object GetParameters(int pageNumber);
    }
}
