using Checkout.Data;
using FluentAssertions;
using Xunit.Abstractions;

namespace Checkout.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _logger;

        public Tests(ITestOutputHelper logger)
        {
          _logger = logger;
        }

        [Theory]
        [MemberData(nameof(TestData.Scenario1), MemberType = typeof(TestData))]
        public void Test_Should_ManageDefaultOrder(int starters, int mains, int drinks, TimeSpan time, double expected)
        {
          // Arrange
          Table table = new(_logger);
          
          // Act
          table.CreateOrder(starters, mains, drinks, time);
          double totalPrice = table.Checkout();

          // Assert
          totalPrice.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestData.Scenario2), MemberType = typeof(TestData))]
        public void Test_Should_ManageDifferentTimeZonesOrder(int starters, int mains, int drinks, TimeSpan time, double expected1, double expected2)
        {
          // Arrange
          Table table = new(_logger);
          
          // Act
          table.CreateOrder(starters, mains, drinks, time);
          double totalPrice = table.Checkout();

          // Assert
          totalPrice.Should().Be(expected1);

          // ReAct
          table.UpdateOrder(0, 2, 2, new TimeSpan(19, 0, 0));
          double updatedTotalPrice = table.Checkout();

          // ReAssert
          updatedTotalPrice.Should().Be(expected2);
        }

        [Theory]
        [MemberData(nameof(TestData.Scenario3), MemberType = typeof(TestData))]
        public void Test_Should_ManageOrderWithFurtherCancellation(int starters, int mains, int drinks, TimeSpan time, double expected1, double expected2)
        {
          // Arrange
          Table table = new(_logger);
          
          // Act
          table.CreateOrder(starters, mains, drinks, time);
          double totalPrice = table.Checkout();

          // Assert
          totalPrice.Should().Be(expected1);

          // ReAct
          table.CancelOrder(1, 1, drinksBefore19: 1);
          double updatedTotalPrice = table.Checkout();

          // ReAssert
          updatedTotalPrice.Should().Be(expected2);
        }
    }
}