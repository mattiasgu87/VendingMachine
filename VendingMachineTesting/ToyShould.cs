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

        [Fact]
        public void HaveAWorkingPrimaryConstructor()
        {
            Toy toy = new Toy("Ball", "A medium sized white ball", 75);

            bool expectedBatteryPowered = false;
            int expectedRequiredAge = 3;

            Assert.Equal(expectedBatteryPowered, toy.IsBatteryPowered);
            Assert.Equal(expectedRequiredAge, toy.RequiredAge);
        }

        [Fact]
        public void HaveAWorkingSecondaryConstructor()
        {
            Toy toy = new Toy("RC Race Car", "A fast, remote controlled race car", 400, true, 12);

            bool expectedBatteryPowered = true;
            int expectedRequiredAge = 12;

            Assert.Equal(expectedBatteryPowered, toy.IsBatteryPowered);
            Assert.Equal(expectedRequiredAge, toy.RequiredAge);
        }
    }
}