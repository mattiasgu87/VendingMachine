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
    }
}