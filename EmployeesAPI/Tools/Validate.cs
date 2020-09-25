using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Tools
{
    public static class Validate
    {
        public static string EstablishmentYear(int year)
        {
            if (year >= 1900 && year <= DateTime.Now.Year)
            {
                return "OK";
            }
            else
            {
                return "Validation error: Field 'EstablishmentYear' should be within the range 1900 .. current year";
            }
        }
    }
}
