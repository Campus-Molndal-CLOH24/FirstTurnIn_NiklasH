namespace CLO24_FirstTurnInNiklasH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CDCollection cDCollection = new CDCollection();
            cDCollection.addCD(new CD("The Dark Side of the Moon", "Pink Floyd", 3));
            cDCollection.addCD(new CD("The Wall", "Pink Floyd", 5));
            cDCollection.addCD(new CD("Wish You Were Here", "Pink Floyd", 2));

            CD foundCD = cDCollection.searchCD("The Wall");

            Console.WriteLine($"Found this: {foundCD}");
            Console.ReadKey();
        }
    }
}
