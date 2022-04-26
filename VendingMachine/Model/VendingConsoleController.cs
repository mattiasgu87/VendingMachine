using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    class VendingConsoleController
    {
        private bool IsRunning = false;
        private VendingMachine VendingMachineInstance;

        public VendingConsoleController()
        {
            VendingMachineInstance = new VendingMachine();
        }

        public void Run()
        {
            IsRunning = true;

            while (IsRunning)
            {
                IsRunning = RunMenu();
            }
        }

        private bool RunMenu()
        {
            bool continueToRun = true;

            PrintMenu();

            char menuChoice;

            Char.TryParse(Console.ReadLine(), out menuChoice);

            switch (menuChoice)
            {
                case 'i':
                    Console.WriteLine("Insert Money");
                    StartInsertingMoney();
                    break;

                case 'e':
                    Console.WriteLine("End transaction");
                    StartEndingTransaction();
                    break;

                case 'b':
                    Console.WriteLine("Buy product");
                    StartBuyProduct();
                    break;

                case 'q':
                    Console.WriteLine("Quit");
                    continueToRun = false;
                    break;

                default:
                    Console.WriteLine("\ninvalid menu choice!");
                    break;
            }
            return continueToRun;
        }

        private void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("***VendingMachine1000***");
            Console.ResetColor();
            Console.WriteLine("************************");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Products:");
            Console.ResetColor();

            PrintProducts();

            Console.WriteLine();

            PrintChoices();

            Console.WriteLine("\nCurrent money inserted in machine: " + this.VendingMachineInstance.Money);

            Console.WriteLine();

            Console.Write("Select an option now: ");
        }

        private void PrintChoices()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choices:");
            Console.ResetColor();
            Console.WriteLine("i: insert money into the vendingmachine");
            Console.WriteLine("e: end transaction");
            Console.WriteLine("b: buy an item from the vending machine");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("q: turn vendingmachine off");
            Console.ResetColor();
        }

        private void PrintProducts()
        {
            foreach (KeyValuePair<string, Product> pair in this.VendingMachineInstance.ShowAll())
            {
                Console.WriteLine(pair.Key + ": " + pair.Value.Name);
            }
        }

        private void PrintProductsWithInfo()
        {
            Console.WriteLine("Products availible: \n");

            foreach (KeyValuePair<string, Product> pair in this.VendingMachineInstance.ShowAll())
            {
                Console.WriteLine(pair.Key + ": " + pair.Value.Name);
                Console.WriteLine(pair.Value.Examine() + "\n");
            }
        }

        private void StartEndingTransaction()
        {
            
            int[] money = this.VendingMachineInstance.EndTransaction();

            Console.Clear();
            Console.WriteLine("Returned money:");

            for (int i = 0; i < this.VendingMachineInstance.MoneyDenominations.Length; i++)
            {
                Console.WriteLine(this.VendingMachineInstance.MoneyDenominations[i] + ": " + money[i]);
            }

            Console.Write("Continue..");
            Console.ReadKey();
        }

        private void StartInsertingMoney()
        {
            bool validDenomination = false;

            while (validDenomination == false)
            {
                Console.Clear();

                Console.WriteLine("Insert money, you can insert these denominations:");

                foreach (int denomination in this.VendingMachineInstance.MoneyDenominations)
                {
                    Console.Write(denomination + " ");
                }

                Console.WriteLine("\nq: quit to main menu\n");
                Console.Write("Insert money or quit: ");

                string input = Console.ReadLine();

                if (input == "q")
                    break;

                int money = 0;

                bool validInt = int.TryParse(input, out money);
                if (validInt)
                {
                    validDenomination = this.VendingMachineInstance.InsertMoney(money);
                    Console.WriteLine("You successfully inserted " + money + "!");
                    Console.Write("Continue..");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your deposit was not valid!");
                    Console.ResetColor();
                    Console.Write("Continue..");
                    Console.ReadKey();
                }
            }
        }

        private void StartBuyProduct()
        {
            bool QuitPurchasing = false;

            while (QuitPurchasing == false)
            {
                Console.Clear();
                PrintProductsWithInfo();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("q: quit to main menu\n");
                Console.ResetColor();

                Console.Write("Pick the product you want to buy:");

                string pickedProduct = Console.ReadLine();

                if (pickedProduct == "q")
                    break;
                else
                {
                    Product product;
                    string message;

                    bool PurchaseResult = this.VendingMachineInstance.Purchase(pickedProduct, out product, out message);

                    if (PurchaseResult)
                    {
                        Console.WriteLine(product.Use());
                        QuitPurchasing = true;
                        Console.Write("Press Enter to continue..");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(message);
                        Console.ResetColor();
                        Console.Write("Press Enter to continue..");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
