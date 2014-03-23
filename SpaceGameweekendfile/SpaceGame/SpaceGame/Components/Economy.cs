using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame
{
    public class Economy
    {
        private int money;
        private int TotalNumberOfResorces;
        private int StartMoney;
        private int SpaceShipId;
        private int SpaceShipIdLevelID;
        public int Money { get { return money; } set { Money = value; } }

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

        public void AddResorce(int ResorceIDPrice)
        {
            if (this.Money >= ResorceIDPrice)
            {
                this.Money -= ResorceIDPrice;
                this.resource ++;
                this.TotalNumberOfResorces++;

            }
            // else { Console.WriteLine("you dont have enough money"); }
        }

        public void removeoneResorce(int ResorceIDPrice)
        {
            if (this.Capacity >= this.TotalNumberOfResorces)
            {
                this.Money += ResorceIDPrice;
                this.ResourceIdQuantity ++;
                this.TotalNumberOfResorces--;
            }
        }

        public float Profit()
        {
            return this.StartMoney / this.Money;
        }

        public void ShipUpGrede(int SpaceShipID, int LevelID, int ShipPricee)
        {
            if (this.Money >= ShipPricee)
            {
                this.SpaceShipId = SpaceShipID;
                this.SpaceShipIdLevelID = LevelID ;
                this.Money -= ShipPricee;
            }
           
        }

        public void ShipChange(int SpaceShipID, int LevelID, int ShipPrice)
        {

            this.SpaceShipId = SpaceShipID;
            this.SpaceShipIdLevelID = LevelID;
            this.Money -= ShipPricee;
        }
    }
}
