using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public abstract class Product
    {
        public string Name { get; }

        public string Info { get; }
        public int Price { get; }

        public Product(string name, string info, int price)
        {
            this.Name = name;
            Info = info;
            this.Price = price;
        }

        public abstract string Examine();

        public abstract string Use();
    }
}
