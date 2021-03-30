using System;

namespace CableConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Grid Row/Column Size (int): ");
            string size = Console.ReadLine();
            while (size.ToLower() != "e")
            {
                if (size == "")
                {
                    Grid g = new Grid();
                    g.Draw();
                }
                else
                {
                    Grid g = new Grid(Convert.ToInt32(size));
                    g.Draw();
                }
                Console.WriteLine("\n\nInput new size to Generate new Grid/CableTiles - E to Exit");
                size = Console.ReadLine();
            }
        }
    }
}
