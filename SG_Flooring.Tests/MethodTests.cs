using NUnit.Framework;
using SG_Flooring.BLL;
using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Tests
{
    [TestFixture]
    public class MethodTests
    {
        [TestCase("01/01/1901", true)] // Valid
        [TestCase("1/1/91", true)] // Valid
        [TestCase("1/01/91", true)] // Valid
        [TestCase("1/f/word", false)] // Invalid
        [TestCase("01012000", false)] // Invalid
        public void TestDateValidation(string date, bool exResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            CheckDateResponse response = manager.ValidateDate(date);

            Assert.AreEqual(response.Success, exResult);
        }
    }
}
