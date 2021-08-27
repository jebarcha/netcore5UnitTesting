using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CustomerNUnitTests
    {
        private Customer customer;
        [SetUp]
        public void Setup()
        {
            customer = new Customer();
        }

        [Test]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange
            //var customer = new Customer();

            //Act
            customer.GreetAndCombineNames("Jesus", "Barajas");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(customer.GreetMessage, "Hello, Jesus Barajas");
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Jesus Barajas"));
                Assert.That(customer.GreetMessage, Does.Contain("jesus barajas").IgnoreCase);   //note:it is case sensitive
                Assert.That(customer.GreetMessage, Does.StartWith("Hello"));
                Assert.That(customer.GreetMessage, Does.EndWith("Barajas"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));  //use a regular expression
            });

        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            //Arrange
            //var customer = new Customer();

            //Act

            //Assert
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.IsNotNull(customer.GreetMessage);
            Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Barajas"));
            Assert.AreEqual("Empty First Name", exceptionDetails.Message);

            Assert.That(() => customer.GreetAndCombineNames("", "Barajas"),
                Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));


            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Barajas"));

            Assert.That(() => customer.GreetAndCombineNames("", "Barajas"),
                Throws.ArgumentException);
        }

        [Test]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerWithMoresThan100Order_ReturPlatinumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }
    }
}
