using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLO24_FirstTurnInNiklasH
{
    class ShopControl
    {
        internal static void ShopLogin()
        {
            int loginChoice;
            bool validLoginChoice = false;  // Initial value is false so we can loop until the user inputs a valid choice in the login menu
                                            // Ff we were using a database we would use sessions instead of a bool above, but we make it simple now

            bool adminRights = false;       // Initial value is false, the user is a guest, admin will be true if the user inputs the correct password
                                            // On shutdown the program will reset the bool to false since we initalize it here

            while (!validLoginChoice)
            {
                Console.WriteLine("Welcome to the Enthusiasts CD shop!");
                Console.WriteLine("Are you a Guest or Administrator? To access the Admin-menu you will need to enter a password:");
                Console.WriteLine("\n1. Log in as Guest"); // \n is a newline character, saves us a row of code
                Console.WriteLine("2. Log in as Administrator");
                Console.WriteLine("3. Exit the program");

                string? loginInput = Console.ReadLine(); // Receive user menu choice

                // Below: we chose TryParse instead of Int32 to avoid exceptions, making it safer for user input - and null is simply "false", easy to handle
                if (int.TryParse(loginInput, out loginChoice) && (loginChoice == 1 || loginChoice == 2 || loginChoice == 3))
                {
                    validLoginChoice = true; // If user choise matches the menu, we can exit the loop

                    if (loginChoice == 1)
                    {
                        adminRights = false; // 1 is guest login, no admin rights
                    }
                    else if (loginChoice == 2) // 2 is admin login, run an admin rights check below
                    {
                        Console.WriteLine("Please enter the Admin password.");
                        Console.WriteLine("Cheat! To testrun this program, adminpass is: Skywalker");
                        string? passwordInput = Console.ReadLine();

                        if (passwordInput == "Skywalker")
                        {
                            adminRights = true; // If adminpass is rightpass, the user gets admin rights
                        }
                        else
                        {
                            Console.WriteLine("Invalid password. Logging in as Guest."); // If failed adminpass, default to guest login
                            adminRights = false;
                        }
                    }
                    else if (loginChoice == 3) // 3 is exit, run the ShopEnd method
                    {
                        ShopEnd();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2 or 3."); // If user input is not 1, 2 or 3, we loop back to the ShopLogin menu
                }
            }

            ShopMenu(adminRights); // adminRights value is passed to the ShopMenu method
        }

        private static void ShopMenu(bool adminRights)
        {
            while (true) // Loop through the menu until the user chose to exit out of it
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("1. Search for a CD");
                Console.WriteLine("2. List all CDs");
                Console.WriteLine("3. Exit the program");

                if (adminRights) // Below options is only visible if adminRights is true! == user passed the admin login
                {
                    Console.WriteLine("4. Add a CD");
                    Console.WriteLine("5. Remove a CD");
                }

                string? menuInput = Console.ReadLine(); // Important! put this after the admin-only code, else we cannot take that input!

                if (int.TryParse(menuInput, out int menuChoice)) // Try parse the input to an int, output to menuChoice
                {
                    switch (menuChoice) // switch-case is a very easy way of maintaining menu choices; if no case, it uses default!
                    {
                        case 1:
                            CollectionOfCDs.SearchCD(); // Call search method
                            break;
                        case 2:
                            CollectionOfCDs.ListCD(); // Call list method
                            break;
                        case 3:
                            ShopEnd(); // Call End method
                            break;
                        case 4 when adminRights:
                            // If the user is an admin, and the input is 4
                            CollectionOfCDs.AddCD(); // Call add method
                            break;
                        case 5 when adminRights:
                            // If the user is an admin, and the input is 5
                            CollectionOfCDs.RemoveCD(); // Call remove method
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private static void ShopEnd()
        {
            while (true)
            {
                Console.WriteLine("\nAre you sure you want to log out?");
                Console.WriteLine("1. = Log out");
                Console.WriteLine("2. = Return to the login-menu");

                string? logoutInput = Console.ReadLine();

                if (int.TryParse(logoutInput, out int logoutChoice)) // Try parse the input to an int, output to logoutChoice
                {
                    switch (logoutChoice)
                    {
                        case 1: // If input was 1
                            Console.WriteLine("\nThanks for visiting the shop! Welcome back anytime");
                            Console.WriteLine("Please press any key");
                            Console.ReadKey();
                            Environment.Exit(0); // Shut down the program
                            break;
                        case 2: // If input was 2
                            Console.WriteLine("\nHeading back to the login-menu. Press any key.");
                            Console.ReadKey();
                            ShopLogin();
                            return;
                        default: // If input was not 1 or 2
                            Console.WriteLine("Invalid choice. Press 1 or 2, please."); // Invalid input, loop back to the ShopEnd menu
                            break;
                    }
                }
            }
        }
    }
}
