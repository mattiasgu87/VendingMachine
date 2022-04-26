using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Model
{
    public class VendingMachine : IVending
    {
        public readonly int[] MoneyDenominations;

        private int money;
        public int Money { get { return money; } }

        private bool IsRunning = false;

        private Dictionary<string, Product> storage= new Dictionary<string, Product>();

        public VendingMachine()
        {
            MoneyDenominations = new int[8] {1, 5, 10, 20, 50, 100, 500, 1000};
            money = 0;
            this.Fill();
        }

        public void Run()
        {
            IsRunning = true;

            while(IsRunning)
            {
                IsRunning = RunMenu();
            }
        }

        public int[] EndTransaction()
        {
            int[] moneyBack = MakeChange(money);
            money = 0;

            return moneyBack;
        }

        public bool InsertMoney(int insertedMoney)
        {
            for(int i = 0; i< MoneyDenominations.Length; i++)
            {
                if(insertedMoney == MoneyDenominations[i])
                {
                    money += insertedMoney;
                    return true;
                }
            }

            return false;
        }

        public void Fill()
        {
            //Drinks
            Drink fanta = new Drink("Fanta", "Orange flavoured soda", 20, true, 33);
            storage.Add("1", fanta);
            Drink springWater = new Drink("Spring Water", "Refreshing spring water, no bubbles", 14, false, 35);
            storage.Add("2", springWater);

            //Sandwiches
            Sandwich clubSandwich = new Sandwich("Club Sandwich", "Sandwich with grilled chicken and lettuce under bacon", 55);
            storage.Add("21", clubSandwich);
            Sandwich tunaSanwich = new Sandwich("Tuna Sandwich", "Sandwich with tuna, cheese and spices", 50);
            storage.Add("22", tunaSanwich);

            //Toys
            Toy car = new Toy("Car", "Basic four-wheeled toy car", 200);
            storage.Add("31", car);
            Toy bunny = new Toy("Toy bunny", "A white, soft toy bunny", 150);
            storage.Add("32", bunny);
        }

        public bool Purchase(string id, out Product product)
        {
            product = null;

            if(this.storage.ContainsKey(id))
            {
                if (this.storage[id].Price <= this.money)
                {
                    product = this.storage[id];
                    money -= this.storage[id].Price;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool Purchase(string id, out Product product, out string message)
        {
            product = null;
            message = null;

            if (this.storage.ContainsKey(id))
            {
                if (this.storage[id].Price <= this.money)
                {
                    product = this.storage[id];
                    money -= this.storage[id].Price;
                    return true;
                }
                else
                {
                    message = "Not enough money to buy product!";
                    return false;
                }                  
            }
            else
            {
                message = "No product found with id: " + id;
                return false;
            }
        }

        public Dictionary<string, Product> ShowAll()
        {
            Dictionary<string, Product> copyStorage = new Dictionary<string, Product>(this.storage);

            return copyStorage;
        }

        public int[] MakeChange(int target)
        {
            int size = this.MoneyDenominations.Length;
            int[] counts = new int[size];
            Array.Fill(counts, 0);

            int remainder = target;
            int bill = this.MoneyDenominations.Length - 1;
            while (remainder > 0)
            {
                counts[bill] = remainder / this.MoneyDenominations[bill];
                remainder -= counts[bill] * this.MoneyDenominations[bill];
                bill--;
            }

            return counts;
        }

        public void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("************************");
            Console.WriteLine("***VendingMachine1000***");
            Console.WriteLine("************************");
            Console.WriteLine();

            Console.WriteLine("Products:");

            PrintProducts();

            Console.WriteLine();

            PrintChoices();

            Console.WriteLine("Current money inserted in machine: " + Money);

            Console.WriteLine();

            Console.Write("Select an option now: ");

        }

        public void PrintChoices()
        {
            Console.WriteLine("i: insert money into the vendingmachine");
            Console.WriteLine("e: end transaction");
            Console.WriteLine("b: buy an item from the vending machine");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("q: turn vendingmachine off");
            Console.ResetColor();
        }

        public void PrintProducts()
        {
            foreach (KeyValuePair<string, Product> pair in storage)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value.Name);
            }
        }

        public void PrintProductsWithInfo()
        {
            Console.WriteLine("Products availible: \n");

            foreach (KeyValuePair<string, Product> pair in storage)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value.Name);
                Console.WriteLine(pair.Value.Examine() + "\n");
            }
        }

        public void StartBuyProduct()
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

                    bool PurchaseResult = Purchase(pickedProduct, out product, out message);

                    if(PurchaseResult)
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

        public void StartInsertingMoney()
        {
            bool validDenomination = false;
            bool validInt = false;
            int money = 0;

            while (validDenomination == false)
            {

                Console.Clear();

                Console.WriteLine("Insert money, you can insert these denominations:");

                foreach (int denomination in MoneyDenominations)
                {
                    Console.Write(denomination + " ");
                }

                Console.WriteLine("\nq: quit to main menu\n");
                Console.Write("Insert money or quit: ");

                string pickedProduct = Console.ReadLine();

                if (pickedProduct == "q")
                    break;

                validInt = int.TryParse(pickedProduct, out money);
                if(validInt)
                {
                    validDenomination = InsertMoney(money);
                }
                
            }
        }

        public void StartEndingTransaction()
        {
            int[] money = EndTransaction();

            Console.Clear();
            Console.WriteLine("Returned money:");

            for(int i = 0; i < MoneyDenominations.Length; i++)
            {
                Console.WriteLine(MoneyDenominations[i] + ": " + money[i]);
            }

            Console.Write("Continue..");
            Console.ReadKey();
        }

        public bool RunMenu()
        {
            bool continueToRun = true;

            PrintMenu();

            char menuChoice;

            Char.TryParse(Console.ReadLine(), out menuChoice);

            switch(menuChoice)
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
    }
}
