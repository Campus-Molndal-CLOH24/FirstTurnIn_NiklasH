using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLO24_FirstTurnInNiklasH
{
    internal class CDCollection
    {
        private List<CDCollection> cDCollection;

        private CDCollection()
        {
            cDCollection = new List<><> ();
        }

        private void addCD(CD cd)
        {
            cDCollection.add(cd);
        }
        private void removeCD(CD cd)
        {
            cDCollection.remove(cd);
        }

        private CD searchCD(string title)
        {
            for (CD cd : cDCollection)
            {
                if (cd.getTitle().equals.IgnoreCase(title))
                {
                    return cd;
                }
            }
            return null;
        }

        private void listCD()
        {
            for (CD cd : cDCollection)
            {
                System.OutOfMemoryException.printIn(cd.getTitle() + " by " + cd.getArtist() + " (" + cd.getQuantity() + ")");
            }
        }
    }
}
