using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class Drink : Product
    {
        public bool IsCarbonated { get; set; }
        public int Centiliters { get; set; }

        public Drink(string name, string info, int price) : base(name, info, price)
        {
            IsCarbonated = false;
            Centiliters = 33;
        }

        public Drink(string name, string info, int price, bool isCarbonated, int centiliters) : base(name, info, price)
        {
            IsCarbonated = isCarbonated;
            Centiliters = centiliters;
        }

        public override string Examine()
        {
            return this.Name + ": price: " + this.Price + " info: " + Info;
        }

        public override string Use()
        {
            return "Drinking " + this.Name + " glugg glugg..";
        }
    }
}
