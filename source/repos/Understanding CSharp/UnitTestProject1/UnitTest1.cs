using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Understanding_CSharp;

namespace UnitTestProject1
{
    [TestClass]
    public class OrderProcessorTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_IfOrderIsNotShipped_ThrowInvalidOperationException()
        {
            var orderProcessor = new OrderProcessor(new FakeShippingInterface());
            var order = new Order { Shipment = new Shipment()};
            orderProcessor.Process(order);

        }
        [TestMethod]
        public void Process_IfOrderIsShipped_ReturnShipment() 
        {
            var orderProcessor = new OrderProcessor(new FakeShippingInterface());
            var order = new Order();
            orderProcessor.Process(order);

            Assert.IsTrue(order.IsShipped);
            Assert.AreEqual(1, order.Shipment.Cost);
            Assert.AreEqual(DateTime.Today.AddDays(1), order.Shipment.ShippingDate);
        }
    }

    public class FakeShippingInterface : IShippingCalculator
    {
        public float CalculateShipping(Order order)
        {
            return 1;
        }
    }
}
