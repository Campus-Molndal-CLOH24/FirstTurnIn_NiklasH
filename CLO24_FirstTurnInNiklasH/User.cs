using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLO24_FirstTurnInNiklasH
{
    internal class User
    {
        internal static void Admin()
        {
            Console.WriteLine("This is a placeholder for a method to set rights for the administator");
            Console.WriteLine("Maybe easieast way to do this would be to return a (private?) set bool-value (visible: true?) to the Program.Menu()-method?");
        }

        internal static void Guest()
        {
            Console.WriteLine("This is a placeholder for a method to set rights for the guest");
            Console.WriteLine("Maybe easieast way to do this would be to return a (private?) set bool-value (visible: false?) to the Program.Menu()-method?");
        }
    }
}
