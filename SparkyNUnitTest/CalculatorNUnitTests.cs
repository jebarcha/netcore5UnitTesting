using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var result = calculator.AddNumbers(1, 2);

            //Assert
            Assert.AreEqual(3, result);
        }
        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var isOdd = calculator.IsOddNumber(10);

            //Assert
            Assert.That(isOdd, Is.EqualTo(false));
            //Assert.IsFalse(isOdd);
            //Assert.AreEqual(false, isOdd);
        }
        [Test]
        [TestCase(11)]
        [TestCase(13)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            var isOdd = calculator.IsOddNumber(a);

            //Assert
            Assert.That(isOdd, Is.EqualTo(true));
            Assert.IsTrue(isOdd);
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calculator = new Calculator();
            var isOdd = calculator.IsOddNumber(a);
            return calculator.IsOddNumber(a);
        }

        [Test]
        [TestCase(5.4, 10.5)]  //15.9
        [TestCase(5.43, 10.53)]  //15.96
        [TestCase(5.49, 10.59)]  //16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            double result = calculator.AddNumbersDouble(a, b);

            //Assert
            Assert.AreEqual(15.9, result, .2);
        }

        [Test]
        public void OddRanger_InputMinAndMax_ReturnsValidOddNumberRange()
        {
            Calculator calc = new();
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            List<int> result = calc.GetOddRange(5, 10);

            Assert.That(result, Is.EquivalentTo(expectedOddRange));
            //Assert.AreEqual(expectedOddRange, result);
            //Assert.Contains(7, result);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Does.Contain(7));
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));
            Assert.That(result,  Is.Ordered);
            Assert.That(result, Is.Unique);
        }

    }
}
