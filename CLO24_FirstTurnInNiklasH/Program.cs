
namespace CLO24_FirstTurnInNiklasH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShopLogin();

            ShopEnd();
        }

        private static void ShopLogin()
        {
            int loginChoice;
            bool validLoginChoice = false;  // initial value is false so we can loop until the user inputs a valid choice in the login menu
                                            // if we were using a database we would use sessions instead of a bool above
            bool adminRights = false;       // initial value is false, the user is a guest, admin will be true if the user inputs the correct password
                                            // on shutdown the program will reset the bool to false since we initalize it here

            while (!validLoginChoice)
            {
                Console.WriteLine("Welcome to the Enthusiasts CD shop!");
                Console.WriteLine("Are you a Guest or Administrator? To access the Admin-menu you will need to enter a password:");
                Console.WriteLine("");
                Console.WriteLine("1. Log in as Guest");
                Console.WriteLine("2. Log in as Administrator");
                Console.WriteLine("3. Exit the program");

                string? loginInput = Console.ReadLine(); // receive user menu choice

                // below: we chose TryParse instead of Int32 to avoid exceptions, making it safer for user input - and null is simply "false", easy to handle
                if (int.TryParse(loginInput, out loginChoice) && (loginChoice == 1 || loginChoice == 2 || loginChoice == 3 ))
                {
                    validLoginChoice = true; // if user choise matches the menu, we can exit the loop

                    if (loginChoice == 1)
                    {
                        adminRights = false;
                    }
                    else if (loginChoice == 2)
                    {
                        Console.WriteLine("Please enter the Admin password:");
                        Console.WriteLine("To testrun this program, adminpass is: Skywalker");
                        string? passwordInput = Console.ReadLine();

                        if (passwordInput == "Skywalker")
                        {
                            adminRights = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid password. Logging in as Guest.");
                            adminRights = false;
                        }
                    }
                    else if (loginChoice == 3)
                    {
                        ShopEnd();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2 or 3.");
                }
            }

            ShopMenu(adminRights);
        }

        private static void ShopMenu(bool adminRights)
        {
            // here we will have a menu mechanic in place to navigate the user through the program
            // vital 1: admin can add, remove, search and list CDs
            // vital 2: guest can only search and list CDs
            // vital 3: make sure the admin/guest can return to the menu after a search and maintain their rights!
        }

        private static void ShopEnd()
        {
            // here we will have a method to reset the bool to false (unless we initialize the program with a bool:false? before the menu is run)
            Console.WriteLine("");
            Console.WriteLine("Thanks for visiting the shop! Welcome back anytime");
            Console.WriteLine("Please press any key:");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
