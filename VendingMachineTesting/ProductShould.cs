using System;
using VendingMachine.Model;
using Xunit;

namespace VendingMachineTesting
{
    public class ProductShould
    {
        [Fact]
        public void BeUsed()
        {
            Product Fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);

            string expectedResult = "Drinking Fanta glugg glugg..";

            Assert.Equal(expectedResult, Fanta.Use());
        }

        [Fact]
        public void BeExamined()
        {
            Product Fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);

            string expectedResult = "Fanta: price: " + 20 + " info: " + "Orange flavoured soda" + "\n carbonated: " + "True" + "\n centiliters: " + 33;

            Assert.Equal(expectedResult, Fanta.Examine());
        }

        [Fact]
        public void HaveUsableProps()
        {
            Product Fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);

            int expectedPrice = 20;
            string expectedInfo = "Orange flavoured soda";
            string expectedName = "Fanta";

            Assert.Equal(expectedPrice, Fanta.Price);
            Assert.Equal(expectedInfo, Fanta.Info);
            Assert.Equal(expectedName, Fanta.Name);
        }
    }
}