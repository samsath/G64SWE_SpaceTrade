using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STDatabase;

namespace DatabaseMain
{
    class Program
    {
        static void Main(string[] args)
        {
            Database dbs = new Database();
            dbs.Connect();
            dbs.Check();
            //dbs.Close();

            dbs.SetUser("sam", 1000);

            Console.WriteLine("Get User info :");

            List<Userdata> resultUser = dbs.getUser();
            for (int i = 0; i < resultUser.Count; i++)
            {
                Console.WriteLine(resultUser[i].User_id + resultUser[i].Session_id + resultUser[i].Name + resultUser[i].Money);
            }

            Console.WriteLine("Get Session info :");

            List<Userdata> resultSession = dbs.getSessionNum();
            for (int i = 0; i < resultSession.Count; i++)
            {
                Console.WriteLine(resultUser[i].User_id + resultUser[i].Session_id + resultUser[i].Name);
            }

            
            
            Console.ReadLine();
        }
    }
}
