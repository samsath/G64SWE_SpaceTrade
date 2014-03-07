using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataBase
{
    public class DatabaseConnect
    {
        SqlConnection dbc;
        String dbConnect;
        
        public DatabaseConnect() 
        {
            String dbConnect = "Data Source=spacetradeDB.mdf";
            SqlConnection dbc = new SqlConnection(dbConnect);
        }

        public bool Connect()
        {
            //SQLiteConnection dbc = new SQLiteConnection(dbConnect);
            dbc.Open();
            ConnectionState state = dbc.State;
            if (state == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Close()
        {
            dbc.Close();
            ConnectionState state = dbc.State;
            if (state == ConnectionState.Closed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
