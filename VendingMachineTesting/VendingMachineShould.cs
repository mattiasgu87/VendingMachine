using System;
using Xunit;
using VendingMachine;
using System.Collections.Generic;

namespace VendingMachineTesting
{
    public class VendingMachineShould
    {
        [Theory]
        [InlineData(1, true, 1)]
        [InlineData(5, true, 5)]
        [InlineData(10, true, 10)]
        [InlineData(20, true, 20)]
        [InlineData(50, true, 50)]
        [InlineData(100, true, 100)]
        [InlineData(500, true, 500)]
        [InlineData(1000, true, 1000)]
        [InlineData(0, false, 0)]
        [InlineData(111, false, 0)]
        [InlineData(-1, false, 0)]
        public void InsertMoney(int money, bool expectedResult, int expectedMoney)
        {
            VendingMachine.Model.VendingMachine sut = new VendingMachine.Model.VendingMachine();

            Assert.Equal(expectedResult, sut.InsertMoney(money));
            Assert.Equal(expectedMoney, sut.Money);
        }

        [Theory]
        [InlineData(1, new int[] { 1, 0, 0, 0, 0, 0, 0, 0 })]
        public void ReturnMoney(int insertedMoney, int[] change)
        {
            VendingMachine.Model.VendingMachine sut = new VendingMachine.Model.VendingMachine();

            sut.InsertMoney(insertedMoney);

            Assert.Equal(change, sut.EndTransaction());
        }

        [Theory]
        [InlineData(new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(new int[] { 2, 1, 1, 3, 1, 4, 1, 0 }, new int[] { 2, 1, 0, 1, 0, 0, 0, 1 })] //1027
        [InlineData(new int[] { 2, 1, 0, 1, 0, 0, 0, 1 }, new int[] { 2, 1, 0, 1, 0, 0, 0, 1 })] //1027
        [InlineData(new int[] { 1, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0, 0, 0, 0, 0 })]
        public void ReturnMoney2(int[] insertedMoney, int[] change)
        {
            VendingMachine.Model.VendingMachine sut = new VendingMachine.Model.VendingMachine();

            for (int i = 0; i < insertedMoney.Length; i++)
            {
                for (int j = 1; j <= insertedMoney[i]; j++)
                {
                    sut.InsertMoney(sut.MoneyDenominations[i]);
                }
            }

            int[] test = sut.EndTransaction();

            Assert.Equal(change, test);
        }

        [Fact]
        public void ReturnCopiedCollection()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();

            Dictionary<string, VendingMachine.Model.Product> sutStorage = sutVM.ShowAll();

            sutStorage.Clear();

            sutStorage = sutVM.ShowAll();

            VendingMachine.Model.Toy sutCar = new VendingMachine.Model.Toy("Car", "Basic four-wheeled toy car", 200);

            Assert.Equal("Car", sutCar.Name);
            Assert.Equal(200, sutCar.Price);
            Assert.Equal("Basic four-wheeled toy car", sutCar.Info);
        }
        
        [Fact]
        public void NotBuyProductWhenBroke()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();

            VendingMachine.Model.Product sutProduct;

            Assert.False(sutVM.Purchase("1", out sutProduct));
        }

        [Fact]
        public void BuyProduct()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();
            sutVM.InsertMoney(1000);

            VendingMachine.Model.Product sutProduct;

            Assert.True(sutVM.Purchase("1", out sutProduct));
        }

        [Fact]
        public void ConsumeMoney()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();
            sutVM.InsertMoney(1000);

            VendingMachine.Model.Product sutProduct;

            sutVM.Purchase("1", out sutProduct);

            Assert.Equal(980, sutVM.Money);
        }
    }
}
