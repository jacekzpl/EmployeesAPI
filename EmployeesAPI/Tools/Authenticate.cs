using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EmployeesAPI.Tools
{
    public static class Authenticate
    {
        static string Username = "user";
        static string Password = "pass";

        public static bool AuthenticatedUser(string token)
        {
            User user = new User();

            try
            {
                var textAsBytes = System.Convert.FromBase64String(token.Replace("Basic", "").Trim());
                string tokenText = Encoding.UTF8.GetString(textAsBytes);

                string[] ss = tokenText.Split(':');
                user.Username = ss[0];
                user.Password = ss[1];

                if (user.Username == Username && user.Password == Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
