using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STDatabase;
namespace SpaceGame
{
    public class Economy
    {
        public IDatabase dbs = new Database();
        #region fields
        // the information passed from the database to create an object of the class to udate monoblate the database
        private int money; // money of the user 
        private int startMoney;// the amount of money when game starts


        private int resource;////  resorces Id
        private int resourceIdQuantity; // for this resorce we pass to monpulate
        private int totalNumberOfResorces; // total amount of resorces in the cargo
       
        private int spaceShipId; // the current ship the player has
        private int spaceShipIdLevelID; // the level of the current ship
        private int spaceShipIdCargo; // it not needed I think the level and the cargo are a same
        private int capacity;   // of the cargo 
       
        #endregion
        #region proporty
        // getter and settier method 
        public int Money { get { return money; } set { money = value; } }
        public int TotalNumberOfResorces { get { return totalNumberOfResorces; } set {totalNumberOfResorces = value; } }
        public int StartMoney { get { return startMoney; } set { startMoney = value; } }
        public int SpaceShipId { get { return spaceShipId; } set { spaceShipId = value; } }
        public int SpaceShipIdLevelID { get { return spaceShipIdLevelID; } set { spaceShipIdLevelID = value; } }
        public int SpaceShipIdCargo { get { return spaceShipIdCargo; } set { spaceShipIdCargo = value; } }
        public int ResourceIdQuantity { get { return resourceIdQuantity; } set { resourceIdQuantity = value; } }
        public int Capacity { get { return capacity;} set { capacity = value; } }
        public int Resource { get { return resource;} set{resource = value ;} }
        #endregion
        #region method
        public void AddToMoney(int new_amount) // to increase the money when the sell happen 
        {
            money += new_amount;
        }

        public bool DecrementMoney(int new_amount)// to decrease the money when buying or ubgrading  new_amont = PRICE
        {  
            if (money - new_amount >= 0)
            {
                money -= new_amount;
                return true;
            }
            else { return false; };
        }

        public void AddResorce(int resorceIDPrice) 
            // INCREASE THE QUANTITY OF THE RESORCE+ DECREASE THE MONEY
           //+ UPDATE THE NUMBERIF RESORCE HOLD IN CARGO
        {
            if (this.Money >= resorceIDPrice && this.Capacity > this.totalNumberOfResorces)
            {
                this.Money -= resorceIDPrice;
                this.Resource ++;
                this.TotalNumberOfResorces++;

            }
            // else { Console.WriteLine("you dont have enough money or not have enogh space"); }
        }

        public void removeoneResorce(int resorceIDPrice)
        {
           // if (this.Capacity > this.TotalNumberOfResorces) ///
           /// {
                this.Money +=  resorceIDPrice ;
                this.ResourceIdQuantity --; ///
                this.TotalNumberOfResorces--;
           // }
            // else { Console.WriteLine("you dont have enough money"); }
        }

        public float Profit() // Calculate the % of the profit or loss    can be a negative number higt score player saved in database
        {
            return this.StartMoney / this.Money * 100;
        }

        public void ShipUpGrede(int SpaceShipIDCargo, int LevelID, int ShipPricee)
        {
            if (this.Money >= ShipPricee)
            {
                this.SpaceShipIdCargo = SpaceShipIDCargo;
                this.SpaceShipIdLevelID = LevelID ;
                this.Money -= ShipPricee;
            }
            // else { Console.WriteLine("you dont have enough money"); }
           
        }

        public void ShipChange(int SpaceShipID, int LevelID, int ShipPricee)
        {
            if (this.Money >= ShipPricee)
            {
                this.SpaceShipId = SpaceShipID; // shipid.
                this.SpaceShipIdLevelID = LevelID;
                this.Money -= ShipPricee;
                //the resorce updated in the database
            }
            // else { Console.WriteLine("you dont have enough money"); }

        }
       /*
       int userit;
       List<Usersata> user =dbs.getUser() ;
       for (int i  = 0 ; i < users.Count ; i++)
        {  
            if (user.[i]= "Sam"){useit = user.[i].Name}
        }
       */
        #endregion

    }
}
