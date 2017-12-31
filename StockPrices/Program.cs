using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using StockPrices.Model;
using System.Globalization;

namespace StockPrices
{
    class Program
    {
        // Declaring file path for input json file and output json file
        private const string filePath = @"./../../App_Data/{0}";

        static void Main(string[] args)
        {
            try
            {
                using (StreamReader readfile = File.OpenText(String.Format(filePath, "input.json")))
                {
                    // Reading from the JSON file
                    JsonSerializer serializer = new JsonSerializer();

                    // Deserializing the data from JSON file
                    Stock[] stock = (Stock[])serializer.Deserialize(readfile, typeof(Stock[]));

                    // Applying LINQ query to filter the top 3 stock prices
                    var result = stock.OrderByDescending(r => r.price) // Arranging in descending order i.e. The top 3 stock prices
                        .Take(3)  // Take only top 3 results
                        .ToList();

                    // Writing Output to a file "output.json"
                    File.WriteAllText(String.Format(filePath, "output.json"), JsonConvert.SerializeObject(result));

                    // Showing output on the console as Top Bike Names used by number of families
                    Console.WriteLine("****** TOP 3 STOCK PRICES ARE AS FOLLOWS *******");
                    foreach (var item in result)
                    {
                        Console.WriteLine("Price: {0}, Timestamp: {1}", item.price, item.timestamp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey(); // Holding console screen till a key is pressed
        }
    }
}
