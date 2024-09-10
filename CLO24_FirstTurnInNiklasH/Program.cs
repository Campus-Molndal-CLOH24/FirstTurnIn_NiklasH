
namespace CLO24_FirstTurnInNiklasH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // since we don't use a database: we will not use sessions. Instead we start off with bool:false (guest) and change to true (admin) if the user inputs the correct password
            // on shutdown the program will reset the bool to false
            ShopMenu();

            ShopEnd();
        }

        private static void ShopMenu()
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
