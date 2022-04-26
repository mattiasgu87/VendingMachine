using System;
using VendingMachine.Model;
using Xunit;

namespace VendingMachineTesting
{
    public class ToyShould
    {
        [Fact]
        public void BeUsed()
        {
            Toy bunny = new Toy("Toy bunny", "A white, soft toy bunny", 150);

            string expectedResult = "Playing with " + "Toy bunny" + " wow! so much fun!";

            Assert.Equal(expectedResult, bunny.Use());
        }

        [Fact]
        public void BeExamined()
        {
            Toy bunny = new Toy("Toy bunny", "A white, soft toy bunny", 150);

            string expectedResult = "Toy bunny" + ": price: " + 150 + " info: " + "A white, soft toy bunny" + "\n battery powered: " + "False" + "\n required age: " + 3 + " years";

            Assert.Equal(expectedResult, bunny.Examine());
        }
    }
}