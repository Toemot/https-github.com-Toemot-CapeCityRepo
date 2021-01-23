using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace Understanding_CSharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var orderProcessor = new OrderProcessor(new ShippingCalculator());

            var order = new Order { DatePlaced = DateTime.Now, TotalPrice = 100f};

            orderProcessor.Process(order);
        }
    }
}
