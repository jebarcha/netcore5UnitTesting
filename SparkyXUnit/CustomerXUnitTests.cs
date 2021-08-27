using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CustomerXUnitTests
    {
        private Customer customer;
        public CustomerXUnitTests()
        {
            customer = new Customer();
        }

        [Fact]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange
            //var customer = new Customer();

            //Act
            customer.GreetAndCombineNames("Jesus", "Barajas");

            //Assert
            Assert.Equal("Hello, Jesus Barajas", customer.GreetMessage);
            Assert.Contains("Jesus Barajas".ToLower(), customer.GreetMessage.ToLower());
            Assert.StartsWith("Hello", customer.GreetMessage);
            Assert.EndsWith("Barajas", customer.GreetMessage);
            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);  //use a regular expression
        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            //Arrange
            //var customer = new Customer();

            //Act

            //Assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.NotNull(customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Barajas"));
            Assert.Equal("Empty First Name", exceptionDetails.Message);

            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Barajas"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoresThan100Order_ReturPlatinumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.IsType<PlatinumCustomer>(result);
        }
    }
}
