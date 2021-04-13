using System;
using CableConnector.ViewModels;

namespace CableConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleGrid g = new ConsoleGrid();

            Console.WriteLine("Enter Grid Row/Column Size (int): ");
            string size = Console.ReadLine();
            while (size.ToLower() != "e")
            {
                if (size == "")
                {
                    g = new ConsoleGrid();
                    g.Draw();
                }
                else if(size.ToLower() == "v")
                {
                    g.Evaluate();
                    g.Draw();
                }
                else
                {
                    g = new ConsoleGrid(Convert.ToInt32(size));
                    g.Draw();
                }
                Console.WriteLine("\n\nInput new size to Generate new Grid/CableTiles\n[E to Exit | V to Evaluate Next Tiles]");
                size = Console.ReadLine();
            }
        }
    }
}
