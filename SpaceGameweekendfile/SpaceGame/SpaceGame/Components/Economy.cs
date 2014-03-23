using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame
{
    public class Economy
    {
        #region fields

        private int money;
        private int totalNumberOfResorces;
        private int startMoney;
        private int spaceShipId;
        private int spaceShipIdLevelID;
        private int spaceShipIdCargo;
        private int resourceIdQuantity;
        private int capacity;
        private int resource;
        #endregion
        #region proporty
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
        public void AddToMoney(int new_amount)
        {
            money += new_amount;
        }

        public bool DecrementMoney(int new_amount)
        {
            if (money - new_amount > 0)
            {
                money -= new_amount;
                return true;
            }
            else { return false; };
        }

        public void AddResorce(int resorceIDPrice)
        {
            if (this.Money >= resorceIDPrice)
            {
                this.Money -= resorceIDPrice;
                this.Resource ++;
                this.TotalNumberOfResorces++;

            }
            // else { Console.WriteLine("you dont have enough money"); }
        }

        public void removeoneResorce(int resorceIDPrice)
        {
            if (this.Capacity >= this.TotalNumberOfResorces)
            {
                this.Money +=  resorceIDPrice;
                this.ResourceIdQuantity ++;
                this.TotalNumberOfResorces--;
            }
            // else { Console.WriteLine("you dont have enough money"); }
        }

        public float Profit()
        {
            return this.StartMoney / this.Money;
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
                this.SpaceShipId = SpaceShipID;
                this.SpaceShipIdLevelID = LevelID;
                this.Money -= ShipPricee;    
            }
            // else { Console.WriteLine("you dont have enough money"); }

        }

        #endregion

    }
}
