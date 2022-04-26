using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class VendingMachine : IVending
    {
        private bool IsRunning = false;
        private int money;
        public int Money { get { return money; } }     
        public readonly int[] MoneyDenominations;
        private Dictionary<string, Product> storage= new Dictionary<string, Product>();

        public VendingMachine()
        {
            MoneyDenominations = new int[8] {1, 5, 10, 20, 50, 100, 500, 1000};
            money = 0;
            this.Fill();
        }

        public int[] EndTransaction()
        {
            int[] moneyBack = MakeChange(money);
            money = 0;

            return moneyBack;
        }

        public bool InsertMoney(int insertedMoney)
        {
            for(int i = 0; i< MoneyDenominations.Length; i++)
            {
                if(insertedMoney == MoneyDenominations[i])
                {
                    money += insertedMoney;
                    return true;
                }
            }
            return false;
        }

        public void Fill()
        {
            //Drinks
            Drink fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);
            storage.Add("1", fanta);
            Drink springWater = new Drink("Spring Water", "Refreshing spring water, no bubbles", 14, false, 35);
            storage.Add("2", springWater);

            //Sandwiches
            Sandwich clubSandwich = new Sandwich("Club Sandwich", "Sandwich with grilled chicken and lettuce under bacon", 55);
            storage.Add("21", clubSandwich);
            Sandwich tunaSanwich = new Sandwich("Tuna Sandwich", "Sandwich with tuna, cheese and spices", 50);
            storage.Add("22", tunaSanwich);

            //Toys
            Toy car = new Toy("Car", "Basic four-wheeled toy car", 200);
            storage.Add("31", car);
            Toy bunny = new Toy("Toy bunny", "A white, soft toy bunny", 150);
            storage.Add("32", bunny);
        }

        public bool Purchase(string id, out Product product)
        {
            product = null;

            if(this.storage.ContainsKey(id))
            {
                if (this.storage[id].Price <= this.money)
                {
                    product = this.storage[id];
                    money -= this.storage[id].Price;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool Purchase(string id, out Product product, out string message)
        {
            product = null;
            message = null;

            if (this.storage.ContainsKey(id))
            {
                if (this.storage[id].Price <= this.money)
                {
                    product = this.storage[id];
                    money -= this.storage[id].Price;
                    return true;
                }
                else
                {
                    message = "Not enough money to buy product!";
                    return false;
                }                  
            }
            else
            {
                message = "No product found with id: " + id;
                return false;
            }
        }

        public Dictionary<string, Product> ShowAll()
        {
            Dictionary<string, Product> copyStorage = new Dictionary<string, Product>(this.storage);

            return copyStorage;
        }

        public int[] MakeChange(int target)
        {
            int size = this.MoneyDenominations.Length;
            int[] counts = new int[size];
            Array.Fill(counts, 0);

            int remainder = target;
            int bill = this.MoneyDenominations.Length - 1;
            while (remainder > 0)
            {
                counts[bill] = remainder / this.MoneyDenominations[bill];
                remainder -= counts[bill] * this.MoneyDenominations[bill];
                bill--;
            }

            return counts;
        }
    }
}
