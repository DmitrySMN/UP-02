using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DemoEx
{
    static class Connection
    {
        private static string con = $"host=localhost;uid=root;pwd=root;database=db17;";
    
        public static string getConnectionString()
        {
            return con;
        }
    }
}
