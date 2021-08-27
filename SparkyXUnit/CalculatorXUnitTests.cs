using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CalculatorXUnitTests
    {
        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var result = calculator.AddNumbers(1, 2);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var isOdd = calculator.IsOddNumber(10);

            //Assert
            Assert.False(isOdd);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(13)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var isOdd = calculator.IsOddNumber(a);

            //Assert
            Assert.True(isOdd);
        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int a, bool expectedResult)
        {
            Calculator calculator = new Calculator();
            var result = calculator.IsOddNumber(a);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)]    //15.9
        //[InlineData(5.43, 10.53)]  //15.96
        //[InlineData(5.49, 10.59)]  //16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            double result = calculator.AddNumbersDouble(a, b);

            //Assert
            Assert.Equal(15.9, result, 0);
        }

        [Fact]
        public void OddRanger_InputMinAndMax_ReturnsValidOddNumberRange()
        {
            Calculator calc = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            List<int> result = calc.GetOddRange(5, 10);

            Assert.Equal(expectedOddRange, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
            Assert.DoesNotContain(6, result);
            Assert.Equal(result.OrderBy(u=>u), result);
            //Assert.That(result, Is.Unique);
        }

    }
}
