using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Model;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingConsoleController VMC = new VendingConsoleController();

            VMC.Run();
        }
    }
}
