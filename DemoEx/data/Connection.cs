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
        private static string con = $"host=10.207.106.12;uid=user17;pwd=rx45;database=db17;";
    
        public static string getConnectionString()
        {
            return con;
        }
    }
}
