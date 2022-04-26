using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class Sandwich : Product
    {
        public bool IsGlutenFree { get; set; }
        public int WeightInGrams { get; set; }

        public Sandwich(string name, string info, int price) : base(name, info, price)
        {
            IsGlutenFree = false;
            WeightInGrams = 90;
        }

        public Sandwich(string name, string info, int price, bool isGlutenFree, int weightInGrams) : base(name, info, price)
        {
            IsGlutenFree = isGlutenFree;
            WeightInGrams = weightInGrams;
        }

        public override string Examine()
        {
            return this.Name + ": price: " + this.Price + " info: " + Info + "\n gluten free: " + IsGlutenFree + "\n weight: " + WeightInGrams + "g";
        }

        public override string Use()
        {
            return "Eating " + this.Name + " crunch crunch..";
        }
    }
}
