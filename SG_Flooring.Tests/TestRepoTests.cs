using NUnit.Framework;
using SG_Flooring.BLL;
using SG_Flooring.Models;
using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Tests
{
    [TestFixture]
    public class TestRepoTests
    {
        [TestCase("01/01/2001", 1, true)] // Found
        [TestCase("01/01/2001", 2, true)] // Found
        [TestCase("01/01/2001", 5, false)] // Not Found -- Order number out of range
        [TestCase("05/05/2005", 1, false)] // Not Found -- No orders for this date
        public void TestOrderExistsInRepo(string date, int orderNumber, bool exResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            GetOrderResponse response = manager.LookupOrder(date, orderNumber);

            Assert.AreEqual(response.Success, exResult);
        }

        [Test]
        public void TestOrdersGetAndSave()
        {
            OrderManager manager = OrderManagerFactory.Create();
            // Get Order
            Order oldOrder = manager.LookupOrder("01/01/2001", 1).Order;
            // Store old field for validation
            decimal oldTotal = oldOrder.Total;
            // Update total and save
            oldOrder.Total = 555.55M;
            manager.SaveOrder(oldOrder);
            decimal newOrderTotal = manager.LookupOrder("01/01/2001", 1).Order.Total;

            // Test that the order originally pulled does not match updated order total, and that
            //  the new total was updated accordingly
            Assert.AreEqual(555.55M, newOrderTotal);
            Assert.AreNotEqual(oldTotal, newOrderTotal);           
        }

        [Test]
        public void CanDeleteOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();
            // Get Order
            Order oldOrder = manager.LookupOrder("01/01/2001", 1).Order;
            // Remove Order
            manager.DeleteOrder(oldOrder);
            Order newOrder = manager.LookupOrder("01/01/2001", 1).Order;

            Assert.AreEqual(oldOrder.Number, 1); // Original order in memory, since deleted from repo
            Assert.IsNull(newOrder); // Second fetch of order after deleted from repo
        }
    }
}
