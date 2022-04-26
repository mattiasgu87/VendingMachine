using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class Toy : Product
    {
        public bool IsBatteryPowered { get; set; }
        public ushort RequiredAge { get; set; }

        public Toy(string name, string info, int price) : base(name, info, price)
        {
            IsBatteryPowered = false;
            RequiredAge = 3;
        }

        public Toy(string name, string info, int price, bool isBatteryPowered, ushort requiredAge) : base(name, info, price)
        {
            IsBatteryPowered = isBatteryPowered;
            RequiredAge = requiredAge;
        }

        public override string Examine()
        {
            return this.Name + ": price: " + this.Price + " info: " + Info;
        }

        public override string Use()
        {
            return "Playing with " + this.Name + " wow! so much fun!";
        }
    }
}
