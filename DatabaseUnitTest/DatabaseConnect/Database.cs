using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace DatabaseConnect
{

    public class Database
    {
        SQLiteConnection dbc;
        SQLiteCommand dbq;

        public void Connect()
        {
            dbc = new SQLiteConnection("data source = spaceTradeDatabse.sqlite");
            dbc.Open();
            dbq = new SQLiteCommand(dbc);
        }

        public void Close()
        {
            dbc.Close();
            
        }

        public void doesTableExists(string tname)
        {
            this.Connect();
            string checktable = "Select * From {0}" + tname;
            dbq.CommandText = checktable;
            dbq.ExecuteNonQuery();
            using (SQLiteDataReader cursor = dbq.ExecuteReader())
            {
                while (cursor.Read())
                {
                    return cursor["User_ID"] + " : " + cursor["Name"] + " : " + cursor["Money"];
                }
            }
            this.Close();
        }
    }


}
