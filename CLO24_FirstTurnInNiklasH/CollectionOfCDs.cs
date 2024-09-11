using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.IO; // for file handling
using Newtonsoft.Json; // needed for JSON serialization (Project -> Manage NuGet Packages -> Add Newtonsoft.Json)

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
            Console.WriteLine("\nEnter CD title, artist or genre to search:");
            string searchTerm = Console.ReadLine()?.ToLower(); // Read the search term from the user and convert it to lowercase

            foreach (var cd in cdCollection) // Loop through all CDs in the collection
            {
                if (cd.Title.ToLower().Contains(searchTerm) || cd.Artist.ToLower().Contains(searchTerm) || cd.Genre.ToLower().Contains(searchTerm))
                {
                    Console.WriteLine($"{cd.Title} by {cd.Artist}, in the {cd.Genre} released in {cd.Year}.");
                    Console.WriteLine($"We have {cd.Quantity} copies of that CD in the store right now.");
                    // If the search term is found in the title, artist or genre, print the CD
                }
            }
        }

    private static void ListCD()
        {
            Console.WriteLine("Current CD Collection:");
            foreach (var cd in cdCollection)
            {
                Console.WriteLine($"Title: {cd.Title}, Artist: {cd.Artist}, Release year: {cd.Year}, Genre: {cd.Genre}, Quantities: {cd.Quantity}.");
            }
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
