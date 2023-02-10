using Checkout.DB;
using Checkout.Enums;
using Checkout.Models;
using Xunit.Abstractions;

namespace Checkout.Entity
{
  public class Table
  {
        private readonly Database _db;
        private readonly ITestOutputHelper _logger;
        private readonly Order _order;

        public Table(ITestOutputHelper logger)
        {
            _db = new Database(logger);
            _order = new Order();
            _logger = logger;
        }

        public int CreateOrder(int starters, int mains, int drinks, TimeSpan time = default)
        {
            if (time == default)
                time = DateTime.Now.TimeOfDay;

            _order.Starter = starters;
            _order.Main = mains;

            if (time < TimeSpan.FromHours(19))
                _order.DrinkBefore19 = drinks;
            else _order.Drink = drinks;

            return _order.Id = _db.CreateOrder(_order);
        }

        public void AddToOrder(int starters, int mains, int drinks, TimeSpan time = default)
        {
            _order.Starter += starters;
            _order.Main += mains;

            if (time < TimeSpan.FromHours(19))
                _order.DrinkBefore19 += drinks;
            else _order.Drink += drinks;

            _db.UpdateOrder(_order);
        }

        public void RemoveFromOrder(int starters = 0, int mains = 0, int drinks = 0, int drinksBefore19 = 0)
        {
            _order.Starter = _order.Starter > 0 ? _order.Starter - starters : 0;
            _order.Main = _order.Main > 0 ? _order.Main - mains : 0;
            _order.DrinkBefore19 = _order.DrinkBefore19 > 0 ? _order.DrinkBefore19 - drinksBefore19 : 0;
            _order.Drink = _order.Drink > 0 ? _order.Drink - drinks : 0;

            _db.UpdateOrder(_order);
        }

        public double Checkout()
        {
            _logger.WriteLine(_order.ToString());
            return _order.Calculate();
        }

        public void DeleteOrderById(int id)
        {
            _db.DeleteOrder(id);
        }

        public Dictionary<ItemType, int> GetCurrentTotalNumberOfItems()
        {
            _db.GetOrder();

            return new Dictionary<ItemType, int>
            {
              { ItemType.Starters , _order.Starter },
              { ItemType.Mains , _order.Main },
              { ItemType.Drinks , _order.Drink },
              { ItemType.DrinksBefore19 , _order.DrinkBefore19 }
            };
        }
    }
}
