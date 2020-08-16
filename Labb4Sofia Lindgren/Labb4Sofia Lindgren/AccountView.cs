using System;

namespace Labb4Sofia_Lindgren.Data
{
    class AccountView
    {
        public Action AddAccount, PrintAllAccounts, PhotoApproval;

        public void MainMenu()
        {
            ConsoleKey inputMainMenu;

            do
            {
                Console.Clear();

                Console.WriteLine("*** Welcome to Account Portal ***\n\n" +
                    "Choose an option:\n" +
                    "1. Register account\n" +
                    "2. Print accounts\n" +
                    "3. Approve photos\n" +
                    "4. Close application\n");

                inputMainMenu = Console.ReadKey(true).Key;
                switch (inputMainMenu)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddAccount();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        PrintAllAccounts();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        PhotoApproval();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        break;
                    default:
                        break;
                }
            } while ((inputMainMenu != ConsoleKey.D4));
        }

       
    }
}
