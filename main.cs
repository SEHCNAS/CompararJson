// Program.cs
using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the path for the first JSON file:");
            string path1 = Console.ReadLine();
            string json1 = File.ReadAllText(path1);

            Console.WriteLine("Enter the path for the second JSON file:");
            string path2 = Console.ReadLine();
            string json2 = File.ReadAllText(path2);

            CompararJson comparer = new CompararJson();
            string result = comparer.CompararJsons(json1, json2);

            Console.WriteLine("\nDifferences between JSONs:");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
