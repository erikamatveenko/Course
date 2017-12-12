using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Utils
{
    public class DateTimeConverter
    {
        public static DateTime getFromString(string str)
        {
       
            try
            {
                String[] iStr = str.Split('-');
                return new DateTime(Int32.Parse(iStr[0]), Int32.Parse(iStr[1]), Int32.Parse(iStr[2]));
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }
         
    }
}
