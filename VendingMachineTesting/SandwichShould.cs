using System;
using VendingMachine.Model;
using Xunit;

namespace VendingMachineTesting
{
    public class SandwichShould
    {
        [Fact]
        public void BeUsed()
        {
            Sandwich clubSandwich = new Sandwich("Club Sandwich", "Sandwich with grilled chicken and lettuce under bacon", 55);

            string expectedResult = "Eating " + "Club Sandwich" + " crunch crunch..";

            Assert.Equal(expectedResult, clubSandwich.Use());
        }

        [Fact]
        public void BeExamined()
        {
            Sandwich clubSandwich = new Sandwich("Club Sandwich", "Sandwich with grilled chicken and lettuce under bacon", 55);

            string expectedResult = "Club Sandwich" + ": price: " + 55 + " info: " + "Sandwich with grilled chicken and lettuce under bacon" + "\n gluten free: " + "False" + "\n weight: " + 90 + "g";

            Assert.Equal(expectedResult, clubSandwich.Examine());
        }

        [Fact]
        public void HaveAWorkingPrimaryConstructor()
        {
            Sandwich sandwich = new Sandwich("Cheese sandwich", "Simple sandwich with cheese", 35);

            bool expectedGlutenfree = false;
            int expectedWeightInGrams = 90;

            Assert.Equal(expectedGlutenfree, sandwich.IsGlutenFree);
            Assert.Equal(expectedWeightInGrams, sandwich.WeightInGrams);
        }

        [Fact]
        public void HaveAWorkingSecondaryConstructor()
        {
            Sandwich sandwich = new Sandwich("Cucumber sandwich", "Vegan, glutenfree cucumber sandwich", 30, true, 80);

            bool expectedGlutenfree = true;
            int expectedWeightInGrams = 80;

            Assert.Equal(expectedGlutenfree, sandwich.IsGlutenFree);
            Assert.Equal(expectedWeightInGrams, sandwich.WeightInGrams);
        }
    }
}