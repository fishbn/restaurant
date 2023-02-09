using Checkout.DTO;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Checkout
{
  public class Table
  {
    private readonly ITestOutputHelper _logger;
    private readonly Order _order;

    public Table(ITestOutputHelper logger)
    {
       _order = new Order();
       _logger = logger;
    }

    public void CreateOrder(int starters, int mains, int drinks, TimeSpan time = default)
    {
      if (time == default)
        time = DateTime.Now.TimeOfDay;

      _order.Starter = starters;
      _order.Main = mains;

      if (time < TimeSpan.FromHours(19))
        _order.DrinkBefore19 = drinks;
      else _order.Drink = drinks;
    }

    public void UpdateOrder(int starters, int mains, int drinks, TimeSpan time = default)
    {
      _order.Starter += starters;
      _order.Main += mains;

      if (time < TimeSpan.FromHours(19))
        _order.DrinkBefore19 += drinks;
      else _order.Drink += drinks;
    }

    public void CancelOrder(int starters = 0, int mains = 0, int drinks = 0, int drinksBefore19 = 0)
    {
      _order.Starter = _order.Starter > 0 ? _order.Starter - starters : 0;
      _order.Main = _order.Main > 0 ? _order.Main - mains : 0;
      _order.DrinkBefore19 = _order.DrinkBefore19 > 0 ? _order.DrinkBefore19 - drinksBefore19 : 0;
      _order.Drink = _order.Drink > 0 ? _order.Drink - drinks : 0 ;
    }

    public double Checkout()
    {
      _logger.WriteLine(_order.ToString());
      return _order.Calculate();
    }
  }
}
