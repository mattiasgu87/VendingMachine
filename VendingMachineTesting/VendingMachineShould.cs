using System;
using Xunit;
using VendingMachine;
using System.Collections.Generic;
using VendingMachine.Model;

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

            Dictionary<string, Product> sutStorageExpected = sutVM.ShowAll();
            Dictionary<string, Product> sutStorageActual = sutVM.ShowAll();

            sutStorageActual.Clear();

            sutStorageActual = sutVM.ShowAll();

            Assert.Equal(sutStorageExpected, sutStorageActual);
        }

        [Fact]
        public void BeFilled()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();

            int sutProductsExpected = 6;
            int sutProductsActual = sutVM.ShowAll().Count;

            Assert.Equal(sutProductsExpected, sutProductsActual);
        }
        
        [Fact]
        public void NotBuyProductWhenBroke()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();

            Product sutProduct;

            Assert.False(sutVM.Purchase("1", out sutProduct));
        }

        [Fact]
        public void BuyProduct()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();
            sutVM.InsertMoney(1000);

            Product expectedProduct = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);
            Product sutProduct;

            Assert.True(sutVM.Purchase("1", out sutProduct));

            Assert.Equal(expectedProduct.Info, sutProduct.Info);
            Assert.Equal(expectedProduct.Name, sutProduct.Name);
            Assert.Equal(expectedProduct.Price, sutProduct.Price);
        }

        [Fact]
        public void GetProductOneWhenBought()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();
            sutVM.InsertMoney(1000);

            Product expectedProduct = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);
            Product sutProduct;

            sutVM.Purchase("1", out sutProduct);

            Assert.Equal(expectedProduct.Info, sutProduct.Info);
            Assert.Equal(expectedProduct.Name, sutProduct.Name);
            Assert.Equal(expectedProduct.Price, sutProduct.Price);
        }

        [Fact]
        public void ConsumeMoney()
        {
            VendingMachine.Model.VendingMachine sutVM = new VendingMachine.Model.VendingMachine();
            sutVM.InsertMoney(1000);

            Product sutProduct;

            sutVM.Purchase("1", out sutProduct);

            Assert.Equal(980, sutVM.Money);
        }
    }
}
