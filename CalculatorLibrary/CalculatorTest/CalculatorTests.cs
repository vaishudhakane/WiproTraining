using NUnit.Framework;
using CalculatorLibrary;
using System;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_TwoPositiveNumbers_ReturnsSum()
        {
            // Arrange
            double a = 10;
            double b = 20;

            // Act
            double result = calculator.Add(a, b);

            // Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        public void Subtract_TwoPositiveNumbers_ReturnsDifference()
        {
            // Arrange
            double a = 20;
            double b = 10;

            // Act
            double result = calculator.Subtract(a, b);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Multiply_TwoPositiveNumbers_ReturnsProduct()
        {
            // Arrange
            double a = 10;
            double b = 20;

            // Act
            double result = calculator.Multiply(a, b);

            // Assert
            Assert.AreEqual(200, result);
        }

        [Test]
        public void Divide_TwoPositiveNumbers_ReturnsQuotient()
        {
            // Arrange
            double a = 20;
            double b = 10;

            // Act
            double result = calculator.Divide(a, b);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Divide_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            double a = 10;
            double b = 0;

            // Act and Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(a, b));
        }

        [Test]
        public void Add_Zero_ReturnsOriginalNumber()
        {
            // Arrange
            double a = 10;
            double b = 0;

            // Act
            double result = calculator.Add(a, b);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Subtract_Zero_ReturnsOriginalNumber()
        {
            // Arrange
            double a = 10;
            double b = 0;

            // Act
            double result = calculator.Subtract(a, b);

            // Assert
            Assert.AreEqual(10, result);
        }
    }
}