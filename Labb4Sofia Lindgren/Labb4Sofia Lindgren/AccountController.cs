using System;
using System.Linq;

namespace Labb4Sofia_Lindgren.Data
{
    class AccountController
    {
        public Context accountContext;
        public AccountView accountView;
        public AccountController(Context accountContext)
        {
            this.accountContext = accountContext;
        }

        public void Run()
        {
            Console.WriteLine("*** Welcome ***\n\nLoading menu...");
            accountContext.Database.EnsureCreated();
            Initialize();

            accountView.MainMenu();

            Console.Clear();
            Console.WriteLine("*** Program closed ***");
        }

        public void Initialize()
        {
            accountView = new AccountView
            {
                AddAccount = AddAccount,
                PrintAllAccounts = PrintAllAccounts,
                PhotoApproval = PhotoApproval
            };
        }

        public void AddAccount()
        {
            string email;
            string notApprovedPhoto;
            int identity = accountContext.Account.Select(u => u.Id).Max();
            bool addAnotherAccount = true;

            while (addAnotherAccount)
            {
                Console.Clear();
                Console.WriteLine("*** Add Account ***\n");

                if (CheckEmail(out email))
                {
                    Console.Write("Photo url: ");
                    notApprovedPhoto = Console.ReadLine().Trim();
                    identity++;

                    accountContext.Account.Add(new Account
                    {
                        Id = identity,
                        Email = email,
                        NotApprovedPhoto = notApprovedPhoto
                    });
                    accountContext.SaveChanges();
                }
                Console.CursorVisible = false;
                if (email == "q")
                {
                    addAnotherAccount = false;
                }
                else
                {
                    Console.Write("Press any key to add more accounts, press \"q\" to go back.");
                    if (Console.ReadKey().Key == ConsoleKey.Q)
                    {
                        addAnotherAccount = false;
                    }
                }
            }
        }

        private bool CheckEmail(out string email)
        {
            string compareEmail;

            do
            {
                Console.Write("Email (enter \"q\" to go back): ");
                Console.CursorVisible = true;
                email = Console.ReadLine().Trim();
                compareEmail = email;
                if (email == "q")
                {
                    return false;
                }
                if (email == "")
                {
                    Console.WriteLine(" - Incorrect email!");
                }
                else if (accountContext.Account.AsEnumerable().Any(u => u.Email == compareEmail))
                {
                    Console.WriteLine(" - Email is already in use!");
                }
                else break;
            } while (email != "q");
            return true;
        }

        public void PrintAllAccounts()
        {
            Console.Clear();

            Console.WriteLine("*** Accounts ***\n" + "ID".PadRight(4) + "Email".PadRight(25) + "Photo");
            foreach (var account in accountContext.Account)
            {
                Console.WriteLine(account.Id.ToString().PadRight(4) + account.Email.PadRight(25) + account.ApprovedPhoto);
            }
            Console.Write("\n Press any key to go back.");
            Console.ReadKey(true);
        }

        public void PhotoApproval()
        {
            string inputString;
            int inputInt;
            var notApprovedUserPhotos = accountContext.Account.Where(u => u.NotApprovedPhoto != null).AsEnumerable();

            do
            {
                Console.Clear();

                Console.WriteLine("*** Photos to approve ***\n" +
                    "ID".PadRight(4) + "Email".PadRight(25) + "Photo");
                foreach (var u in notApprovedUserPhotos)
                {
                    Console.WriteLine(u.Id.ToString().PadRight(4) + u.NotApprovedPhoto);
                }

                Console.Write("\nInput an \"ID\" to approve the photo, enter q to go back: ");
                Console.CursorVisible = true;
                inputString = Console.ReadLine().Trim();
                Console.CursorVisible = false;
                inputInt = int.TryParse(inputString, out inputInt) ? inputInt : 0;

                if (inputString == "q")
                {
                    continue;
                }

                if (notApprovedUserPhotos.Any(u => u.Id == inputInt))
                {
                    var toApprove = notApprovedUserPhotos.Single(u => u.Id == inputInt);
                    var photo = toApprove.NotApprovedPhoto;
                    toApprove.ApprovedPhoto = photo;
                    toApprove.NotApprovedPhoto = null;
                    accountContext.SaveChanges();
                    Console.WriteLine($"Photo with ID: {inputInt} has been approved! Press any key.");
                    notApprovedUserPhotos = accountContext.Account.Where(u => u.NotApprovedPhoto != null).AsEnumerable();
                }
                else
                {
                    Console.WriteLine("Photo does not exist");
                }
                Console.ReadKey(true);
            } while (inputString != "q");
        }

    }
}
