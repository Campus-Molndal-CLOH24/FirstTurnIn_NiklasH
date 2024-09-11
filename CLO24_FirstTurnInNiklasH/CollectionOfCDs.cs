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
                string? json = File.ReadAllText(filePath);
                cdCollection = JsonConvert.DeserializeObject<List<CD>>(json);
            }
            else
            {
                Console.WriteLine("No CD collection found, starting with an empty list.");
            }
        }

        private static void SaveCDCollection() // Method to save the CD collection to file, this is so we don't have to re-use code when adding/removing etc
        {
            string? json = JsonConvert.SerializeObject(cdCollection, Formatting.Indented); // Serialize the list of CDs into JSON and indent it for readability. Needed json-package!
            File.WriteAllText(filePath, json);
        }

        private static void SearchCD() // Method to search for a CD in the collection
        {
            Console.WriteLine("\nEnter CD title, artist or genre to search:");
            string? searchTerm = Console.ReadLine()?.ToLower(); // Read the search term from the user and convert it to lowercase

            foreach (var cd in cdCollection) // Loop through all CDs in the collection
            {
                if (cd.Title.ToLower().Contains(searchTerm) || cd.Artist.ToLower().Contains(searchTerm) || cd.Genre.ToLower().Contains(searchTerm)) // if searchTerm is met, print the CD
                // Consider CompareString (separate method, neater and re-use of code) instead of ToLower (could be sensitive for some languages)
                // TO DO: It could return more than one title, if our searchTerm for example is brief like "net" and we have "internet" and ".NET"! Fix!
                {
                    Console.WriteLine($"{cd.Title} by {cd.Artist}, in the {cd.Genre} released in {cd.Year}.");
                    Console.WriteLine($"We have {cd.Quantity} copies of that CD in the store right now.");
                }
            }
        }

        private static void ListCD() // Method to list all CDs in the collection
        {
            Console.WriteLine("Current CD Collection:");
            foreach (var cd in cdCollection) // Loop through the collection and print each CD
            {
                Console.WriteLine($"Title: {cd.Title}, Artist: {cd.Artist}, Release year: {cd.Year}, Genre: {cd.Genre}, Quantities: {cd.Quantity}.");
            }
        }

        private static void AddCD() // Method to add a CD to the collection
        {
            // TO DO: IMPORTANT!
            // Add a check to see if the CD already exists in the collection, if it does, increase the quantity instead of adding a new CD
            // Add a validation check to make sure the input is valid! (year, quantity etc)

            Console.WriteLine("Enter CD title:");
            string? title = Console.ReadLine();

            Console.WriteLine("Enter the artist:");
            string? artist = Console.ReadLine();

            Console.WriteLine("Enter the genre:");
            string? genre = Console.ReadLine();

            Console.WriteLine("Enter the release year:");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                // We take the input from above, then add it to the cdCollection list, then use SaveCDCollection to save it to file
                cdCollection.Add(new CD { Title = title, Artist = artist, Genre = genre, Year = year });
                SaveCDCollection();
                Console.WriteLine("CD added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid year entered.");
                // TO DO: This needs to be expanded. We should probably loop back to the start of the method if the input is invalid.
            }
        }

        private static void RemoveCD()
        {
            Console.WriteLine("Enter CD title or artist to remove:");
            // We will only accept title or artist as input, as we don't want to remove multiple CDs at once. By default we remove the first CD found.
            string? searchTerm = Console.ReadLine()?.ToLower();

            var cdToRemove = cdCollection.Find(cd => cd.Title.ToLower().Contains(searchTerm) || cd.Artist.ToLower().Contains(searchTerm));
            // Find the first CD in the collection where the title or artist contains the search term, add it to the cdToRemove variable

            if (cdToRemove != null) // If we found a CD to remove (if cdToRemove is not null)
            {
                cdCollection.Remove(cdToRemove); // Remove the CD from the collection variable
                SaveCDCollection(); // use the SaveCDCollection method to save the changes to file
                Console.WriteLine("CD removed succesfully!");
            }
            else
            {
                Console.WriteLine("CD not found.");
            }
        }
    }

    internal record CD(string Title, string Artist, string Genre, int Year, int Quantity = 1); // Record for CD, with a default quantity of 1

    internal class CD
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
    }
}
