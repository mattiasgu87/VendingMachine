using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class Toy : Product
    {
        public Toy(string name, string info, int price) : base(name, info, price)
        {

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
