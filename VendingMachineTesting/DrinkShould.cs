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

        [Fact]
        public void HaveAWorkingPrimaryConstructor()
        { 
            Drink Water = new Drink("Water", "Cheap tap water", 20);

            bool expectedCarbonated = false;
            int expectedCentiliters = 33;

            Assert.Equal(expectedCarbonated, Water.IsCarbonated);
            Assert.Equal(expectedCentiliters, Water.Centiliters);
        }

        [Fact]
        public void HaveAWorkingSecondaryConstructor()
        { 
            Drink Cola = new Drink("Cola", "Off-brand regular cola", 20, true, 50);

            bool expectedCarbonated = true;
            int expectedCentiliters = 50;

            Assert.Equal(expectedCarbonated, Cola.IsCarbonated);
            Assert.Equal(expectedCentiliters, Cola.Centiliters);
        }
    }
}