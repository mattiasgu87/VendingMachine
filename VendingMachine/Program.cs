using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Model.VendingMachine vm = new Model.VendingMachine();

            vm.Run();
        }
    }
}
