using System;

namespace A8_MediaSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("---------------------");
            Console.WriteLine("|  Media Library v3 |");
            Console.WriteLine("---------------------");
            // Repository additions go here
            Menu.grabRepositories();
            do
            {
               Menu.runMenu();
            }while(!Menu.endProgram);
            Console.WriteLine("Thanks for using Media Library v3!");
        }
    }
}
