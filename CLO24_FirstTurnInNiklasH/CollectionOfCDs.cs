using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace CLO24_FirstTurnInNiklasH
{
    internal class CollectionOfCDs
    {
        private static List<CD> cdCollection = new List<CD>();
        private static string? filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cdcollection.json");
        // Above: we use Path.Combine to create a file path that works on all systems, and we use AppDomain to get the base directory of the application
        // Console.WriteLine($"Looking for file at: {filePath}"); // Debugging code for file path, can be used to test if the file is found

        static CollectionOfCDs() // Constructor to load the CD collection from file
        {
            LoadCDCollection();
        }

        private static void LoadCDCollection() // Method to load the CD collection from file, this is so we don't have to re-use code when searching/adding etc
        {
            if (File.Exists(filePath)) // If the file exists, we read the JSON and deserialize it into a list of CDs
            {
                string json = File.ReadAllText(filePath);
                cdCollection = JsonConvert.DeserializeObject<List<CD>>(json);
            }
            else
            {
                Console.WriteLine("No CD collection found, starting with an empty list.");
            }
        }

        private static void SaveCDCollection() // Method to save the CD collection to file, this is so we don't have to re-use code when adding/removing etc
        {
            string json = JsonConvert.SerializeObject(cdCollection, Formatting.Indented); // Serialize the list of CDs into JSON and indent it for readability
            File.WriteAllText(filePath, json);
        }

        private static void SearchCD()
        {
            // method to search for a CD in the collection
        }
        private static void ListCD()
        {
            // method to list all CDs in the collection
        }

        private static void AddCD()
        {
            // method to add a cd to the collection (list)
        }
        private static void RemoveCD()
        {
            // method to remove a cd from the collection (list)
        }

        // do we need a seprate method to access the list? so we don't re-use code when adding/removing?
    }
}
