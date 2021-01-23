using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace tosin

{ 

    class Program 
    {
        static void Main(string[] args)
        {
            Weather();
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
                if (!string.IsNullOrEmpty(decisionMaker.exception))
                {
                    Console.WriteLine(decisionMaker.exception);
                }
            } while (!decisionMaker.Valid);

            if (decisionMaker.exit)
                return;

            Console.WriteLine("We currently support Cape Town Weather ...");
            Console.WriteLine("1. Get Cape Town's current weather? ");
            Console.WriteLine("2. Exit");

            DecisionMaker decisionMaker;
            do
            {
                decisionMaker = new DecisionMaker(Console.ReadLine());
                if (!string.IsNullOrEmpty(decisionMaker.exception))
                {
                    Console.WriteLine(decisionMaker.exception);
                }
            } while (!decisionMaker.Valid);

            if (decisionMaker.exit)
                return;

            var url = "http://api.openweathermap.org/data/2.5/weather?q=Cape%20Town&appid=108b11a0259601a3f430dfa5bc4957a4";
            var client = GetAsync(utl).GetAwaiter().GetResult();
            Console.WriteLine(client);

            var response = JsonConvert.DeserializeObject<dynamic>(client);

            Console.WriteLine("We currently support Cape Town Weather ...");
            Console.WriteLine("1. Get Cape Town's current weather? ");
            Console.WriteLine("2. Exit");
            Console.WriteLine("3. Exit");

            var value = 0;
            var isInt = false;
            do
            {
                isInt = int.TryParse(Console.ReadLine(), out value);

            } while (isInt && (!value > 0 || value <=3));
            switch (value)
            {
                case 1:
                    Console.WriteLine(client);
                    break;
                case 2:
                    Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
                    break;
                case 3:
                    Console.WriteLine($"Name:{response.name}");
                    Console.WriteLine($"Weather description:{response.weather[0].description}");
                    Console.WriteLine($"Weather temp:{response.main.temp}");
                    break;

            }

        }
        public static async Task<string> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => (content));
        }
    }
    public class DecisionMaker
    {
        public bool exit = false;
        public bool proceed = false;
        public bool Valid = false;
        public bool exception;
        private int _number = Int32.MinValue;

        public DecisionMaker(string number)
        {
            var test = int.TryParse(number, out int decision);
            proceed = decision == 1;
            exit = decision == 2;
            Valid = test && (proceed || exit);
            if (!Valid)
                exception = "Enter a valid number!";
            _number = test ? decision : _number;


        }
    }
}