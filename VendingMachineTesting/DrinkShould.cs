using Xunit;
using VendingMachine.Model;

namespace VendingMachineTesting
{
    public class DrinkShould
    {
        [Fact]
        public void BeUsed()
        {
            Drink Fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);

            string expectedResult = "Drinking Fanta glugg glugg..";

            Assert.Equal(expectedResult, Fanta.Use());
        }

        [Fact]
        public void BeExamined()
        {
            Drink Fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);

            string expectedResult = "Fanta: price: " + 20 + " info: " + "Orange flavoured soda" + "\n carbonated: " + "True" + "\n centiliters: " + 33;

            Assert.Equal(expectedResult, Fanta.Examine());
        }
    }
}