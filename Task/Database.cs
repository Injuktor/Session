using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public static class Database
    {

        public static MySqlConnection Connection;

        public static void Init()
        {
            Connection = new MySqlConnection("server=192.168.2.4;database=user5_db;uid=user5;port=3311;password=start;");
            Connection.Open();
        }

    }
}
