using Newtonsoft.Json; // needed for JSON serialization (Project -> Manage NuGet Packages -> Add Newtonsoft.Json)

namespace CLO24_FirstTurnInNiklasH
{
    internal class CollectionOfCDs
    {
        private static List<CD> cdCollection = new List<CD>();
        // private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cdcollection.json");
        // Above: we use Path.Combine to create a file path that works on all systems, and we use AppDomain to get the base directory of the application
        private static string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "cdcollection.json");
        // We adjusted the previous path with this one, as the application tend to store the file in the wrong location. This path should work better.
        // The difference between this and above: now we can use the same location as the program would use for Executable (.exe). That solved the problem.

        static CollectionOfCDs() // Constructor to load the CD collection from file
        {
            LoadCDCollection();
        }

        private static void LoadCDCollection() // Method to load the CD collection from file, this is so we don't have to re-use code when searching/adding etc
        {
            if (File.Exists(filePath)) // If the file exists, we read the JSON and deserialize it into a list of CDs
            {
                string? json = File.ReadAllText(filePath);
                cdCollection = JsonConvert.DeserializeObject<List<CD>>(json) ?? new List<CD>();
            }
            else
            {
                Console.WriteLine("No CD collection found, starting with an empty list.");
            }
        }

        private static void SaveCDCollection() // Method to save the CD collection to file, this is so we don't have to re-use code when adding/removing etc
        {
            try
            {
                string json = JsonConvert.SerializeObject(cdCollection, Newtonsoft.Json.Formatting.Indented);
                // Serialize the list of CDs into JSON and indent it for readability. Needed json-package from NuGet! See documentation.md
                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine("Saving CD collection to file .. "); // Debug message we use as verification that the code takes us here
                    File.WriteAllText(filePath, json);
                    // Console.WriteLine("CD collection saved successfully."); // debug message used to verify as the file was saved/updated/written to
                    // Console.WriteLine($"File path used: {filePath}"); // Used this file path for debugging - and found the file is stored in the wrong location!
                }
            }
            catch (Exception exSave)
            {
                Console.WriteLine($"Error saving CD collection: {exSave.Message}"); // if it fails to save, catch prints what error we get
            }
        }

        internal static void SearchCD() // Method to search for a CD in the collection
        {
            Console.WriteLine("\nEnter CD title, artist or genre to search:");
            string? searchTerm = Console.ReadLine()?.ToLower(); // Read the search term from the user and convert it to lowercase

            if (!string.IsNullOrEmpty(searchTerm)) // Check if the search term is not null or empty
            {
                var matchingCDs = cdCollection.Where(cd => // Use LINQ to filter the collection based on the search term
                (cd.Title ?? "").ToLower().Contains(searchTerm) == true ||
                (cd.Artist ?? "").ToLower().Contains(searchTerm) == true ||
                cd.Year.ToString().ToLower().Contains(searchTerm) == true)  // converting year to string so we can use ?? "" to avoid null reference exception
                .OrderBy(cd => cd.Artist)
                .ThenBy(cd => cd.Title)
                .ThenBy(cd => cd.Year); // Order the results by artist, then title, then year

                // Important note regarding the code above: When we changed mechanics for the Search to LINQ,
                // we also added .Where, which ensures that ALL matches are found, so we can print them all, not just the first one.

                if (matchingCDs.Any())
                {
                    foreach (var cd in matchingCDs) // Loop through the matching CDs and print them
                    {
                        Console.WriteLine($"{cd.Title} by {cd.Artist}, in the genre {cd.Genre} released in {cd.Year}.");
                        Console.WriteLine($"We have {cd.Quantity} copies of that CD in the store right now.");
                    }
                }
                else
                {
                    Console.WriteLine("No matching CDs found.");
                }
            }
            else
            {
                Console.WriteLine("Search term cannot be empty");
            }
        }

        internal static void ListCD() // Method to list all CDs in the collection
        {
            Console.WriteLine("Current CD Collection:");
            
            // Sort the collection alphabetically by artist, then title, then by year
            var sortedCDs = cdCollection.OrderBy(cd => cd.Artist).ThenBy(cd => cd.Title).ThenBy(cd => cd.Year);

            foreach (var cd in cdCollection) // Loop through the collection and print each CD
            {
                Console.WriteLine($"Title: {cd.Title}, Artist: {cd.Artist}, Release year: {cd.Year}, Genre: {cd.Genre}, Quantities: {cd.Quantity}.");
            }
        }

        internal static void AddCD() // Method to add a CD to the collection
        {
            string? title, artist, genre;
            int year;

            do // Loop until we have valid input for title, artist, and genre
            {
                Console.WriteLine("Enter CD title:");
                title = Console.ReadLine();

                Console.WriteLine("Enter the artist:");
                artist = Console.ReadLine();

                Console.WriteLine("Enter the genre:");
                genre = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(genre))
                {
                    Console.WriteLine("Invalid input. Please provide valid values for title, artist, and genre.");
                    return; // Exit method if any input is invalid
                }
            }
            while (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(genre));

            while (true)
            {
                Console.WriteLine("Enter the release year:");
                if (int.TryParse(Console.ReadLine(), out year))
                    break; // Exit the loop if the input is a valid integer
            }

            // Initially we want to check if the CD already exists in the collection, so we can increase the quantity instead of adding a new CD
            // FirstOrDefault will return the first CD in the collection that matches the condition, or default (null) if no CD matches
            var existingCD = cdCollection.FirstOrDefault(cd =>
            cd.Title?.ToLower() == title.ToLower() && cd.Artist?.ToLower() == artist.ToLower());

            if (existingCD != null) // If the CD already exists in the collection
            {
                existingCD.Quantity += 1;  // Increase the quantity of the existing CD
                Console.WriteLine("CD already existed in the collection, quantity increased.");
            }
            else
            {
                // If it doesn't exist, we add the CD to the collection AND set a default quantity of 1! Important!
                cdCollection.Add(new CD { Title = title, Artist = artist, Genre = genre, Year = year, Quantity = 1 });
                Console.WriteLine("CD added successfully!");
            }

            SaveCDCollection(); // Use the SaveCDCollection method to save the changes to file
        }
        internal static void RemoveCD()
        {
            Console.WriteLine("Enter CD title or artist to remove:");
            // We will only accept title or artist as input, as we don't want to remove multiple CDs at once. By default we remove the first CD found.
            string? searchTerm = Console.ReadLine()?.ToLower();

            if (searchTerm != null) // check to avoid null reference exception
            {

                var cdToRemove = cdCollection.Find(cd =>
                    cd.Title?.ToLower().Contains(searchTerm) == true ||
                    cd.Artist?.ToLower().Contains(searchTerm) == true);
                // Find the first CD in the collection where the title or artist contains the search term, add it to the cdToRemove variable

                if (cdToRemove != null) // If we found a CD to remove (if cdToRemove is not null)
                {
                    cdCollection.Remove(cdToRemove); // Remove the CD from the collection variable
                    SaveCDCollection(); // use the SaveCDCollection method to save the changes to file
                    Console.WriteLine("CD removed successfully!");
                }
                else
                {
                    Console.WriteLine("CD not found.");
                }
            }
        }
    }

    internal class CD
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
    }
}
