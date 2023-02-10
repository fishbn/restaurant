using Checkout.Data;
using Checkout.Entity;
using Checkout.Enums;
using FluentAssertions;
using Xunit.Abstractions;
using static Checkout.Enums.ItemType;

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
          Dictionary<ItemType, int> items = table.GetCurrentTotalNumberOfItems();
          
          totalPrice.Should().Be(expected);
          items[Starters].Should().Be(starters);
          items[Mains].Should().Be(mains);

          if (time < TimeSpan.FromHours(19))
            items[DrinksBefore19].Should().Be(drinks);
          else
            items[Drinks].Should().Be(drinks);
        }

        [Theory]
        [MemberData(nameof(TestData.Scenario2), MemberType = typeof(TestData))]
        public void Test_Should_ManageDifferentTimeZonesOrder(int starters, int mains, int drinks, TimeSpan time, double expected1, double expected2)
        {
          // Arrange
          const int additionalMains = 2;
          const int additionalDrinks = 2;
          TimeSpan additionalOrderTime = new TimeSpan(19, 0, 0);

          Table table = new(_logger);
          
          // Act
          table.CreateOrder(starters, mains, drinks, time);
          double totalPrice = table.Checkout();

          // Assert
          Dictionary<ItemType, int> items = table.GetCurrentTotalNumberOfItems();
          
          totalPrice.Should().Be(expected1);
          items[Starters].Should().Be(starters);
          items[Mains].Should().Be(mains);
          items[DrinksBefore19].Should().Be(drinks);
          items[Drinks].Should().Be(0);

          // ReAct
          table.UpdateOrder(0, additionalMains, additionalDrinks, additionalOrderTime);
          double updatedTotalPrice = table.Checkout();

          // ReAssert
          items = table.GetCurrentTotalNumberOfItems();

          updatedTotalPrice.Should().Be(expected2);
          items[Starters].Should().Be(starters);
          items[Mains].Should().Be(mains + additionalMains);
          items[DrinksBefore19].Should().Be(drinks);
          items[Drinks].Should().Be(additionalDrinks);
        }

        [Theory]
        [MemberData(nameof(TestData.Scenario3), MemberType = typeof(TestData))]
        public void Test_Should_ManageOrderWithFurtherCancellation(int starters, int mains, int drinks, TimeSpan time, double expected1, double expected2)
        {
          const int cancellationStarters = 1;
          const int cancellationMains = 1;
          const int cancellationDrinks = 1;

          // Arrange
          Table table = new(_logger);
          
          // Act
          table.CreateOrder(starters, mains, drinks, time);
          double totalPrice = table.Checkout();

          // Assert
          Dictionary<ItemType, int> items = table.GetCurrentTotalNumberOfItems();

          totalPrice.Should().Be(expected1);
          items[Starters].Should().Be(starters);
          items[Mains].Should().Be(mains);
          items[DrinksBefore19].Should().Be(drinks);
          items[Drinks].Should().Be(0);

          // ReAct
          table.CancelOrder(cancellationStarters, cancellationMains, drinksBefore19: cancellationDrinks);
          double updatedTotalPrice = table.Checkout();

          // ReAssert
          items = table.GetCurrentTotalNumberOfItems();

          updatedTotalPrice.Should().Be(expected2);
          items[Starters].Should().Be(starters - cancellationStarters);
          items[Mains].Should().Be(mains - cancellationMains);
          items[DrinksBefore19].Should().Be(drinks - cancellationDrinks);
          items[Drinks].Should().Be(0);
        }
    }
}