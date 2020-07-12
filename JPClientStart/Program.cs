using System;

namespace JPClientStart
{
    public class Program
    {
        private static int option;

        public static void Main()
        {
            Console.WriteLine("Welcome to Client Helper!");
            ShowOption();
            option = int.Parse(Console.ReadLine());
            ExecuteOption();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        private static void ShowOption()
        {
            Console.WriteLine("Select your option: ");
            Console.WriteLine("1. Change Language");
            Console.WriteLine("2. Copy File");

        }

        private static void ExecuteOption()
        {
            switch (option)
            {
                case 1:
                    LanguageChanger.Run();
                    break;
                case 2:
                    FileCopier.Run();
                    break;
            }
        }
    }
}


   