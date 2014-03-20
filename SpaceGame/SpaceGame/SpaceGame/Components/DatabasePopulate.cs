using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STDatabase;
using System.IO;


namespace SpaceGame.Components
{
    class DatabasePopulate
    {
        /*
         * This is to try and populate the database when a new game starts. It is done here so that it can easily be removed or changed if need by as maybe
         * threaded if it slows down the process.
         */
        Database dbs = new Database();
        public void resourceadd()
        {
            var contents = File.ReadAllLines("resourceListCostDescript.csv");
            for (int i = 0; i < contents.Length; i++)
            {
                string[] lineSplit = contents[i].Split(',');
                dbs.NewResourceMedia(lineSplit[0], ToInt32(lineSplit[1]), lineSplit[2], 0, 0, lineSplit[3],0);
               

                // public void NewResourceMedia(string resource, int initialprice, string descript, int x_s, int y_s, string fileloc, int type)
            }
        }
    }
}
