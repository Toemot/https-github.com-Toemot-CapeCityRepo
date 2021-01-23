using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Weather();
            Console.ReadLine();
        }

        public static void Weather() 
        {
            Console.WriteLine("Welcome to the Weather Service console app. Please select an option from the menu:");
            Console.WriteLine("1. Choose City");
            Console.WriteLine("2. Exit");

            DecisionMaker decisionMaker;

            do
            {
                decisionMaker = new DecisionMaker(Console.ReadLine());
                if (!string.IsNullOrWhiteSpace(decisionMaker.exception))
                {
                    Console.WriteLine(decisionMaker.exception);
                }
            } while (!decisionMaker.valid);
            if (decisionMaker.exit)
                return;

            Console.WriteLine("We currently only support Cape Town's weather");
            Console.WriteLine("1. Get Cape Town's current weather");
            Console.WriteLine("2. Exit");

            //Decision Two

            do
            {
                decisionMaker = new DecisionMaker(Console.ReadLine());
                if (!string.IsNullOrWhiteSpace(decisionMaker.exception))
                {
                    Console.WriteLine(decisionMaker.exception);
                }
            } while (!decisionMaker.valid);
            if (decisionMaker.exit)
                return;
            
            var url = "http://api.openweathermap.org/data/2.5/weather?q=Cape%20Town&appid=108b11a0259601a3f430dfa5bc4957a4";
            var client = GetAsync(url).GetAwaiter().GetResult();
            var response = JsonConvert.DeserializeObject<dynamic>(client);

            Console.WriteLine("How would you like to view the data?");
            Console.WriteLine("1. Raw JSON");
            Console.WriteLine("2. Formatted");
            Console.WriteLine("3. Nicely formatted and only displaying data interesting to the public");

            //do
            //{
            //    decisionMaker = new DecisionMaker(Console.ReadLine());
            //    if (!string.IsNullOrWhiteSpace(decisionMaker.exception))
            //    {
            //        Console.WriteLine(decisionMaker.exception);
            //    }
            //} while (!decisionMaker.valid);
            //if (decisionMaker.exit)
            //    return;

            var value = 0;
            var isInt = false;

            do
            {
                isInt = int.TryParse(Console.ReadLine(), out value);
            } while (!isInt && (value > 0 || value <= 3));
            
            switch (value)
            {
                case 1:
                    Console.WriteLine(client);
                    break;
                case 2:
                    Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
                    break;
                case 3:
                    Console.WriteLine($"Name:{response.name}, Weather description:" +
                        $"{response.weather[0].description}, Weather temp:{response.main.temp}");
                    break;

            }
        }

        public static async Task<string> GetAsync(string url) 
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => (content));
        }
    }
    public class DecisionMaker
    {
        public bool exit;
        public bool proceed;
        public bool valid;
        public string exception = "";

        private int _number = Int32.MinValue;
        public DecisionMaker(string number)
        {
            var test = int.TryParse(number, out int decision);
            proceed = decision == 1;
            exit = decision == 2;
            valid = test && (proceed || exit);

            if (!valid)
            {
                exception = "Enter a valid response";
            }
            
            _number = test ? decision : _number;
        }
    }
}
