using Checkout.DTO;
using Newtonsoft.Json;

namespace Checkout
{
  public class Table
  {
    private string Json { get; set; }

    public Table()
    {
        
    }

    public void CreateOrder(int starters, int mains, int drinks, TimeSpan time = default)
    {
      var order = new Order
      {
        Starter = starters,
        Main = mains
      };

      if (time < TimeSpan.FromHours(19))
        order.DrinkBefore19 = drinks;
      else order.Drink = drinks;

      // Save
      Json = JsonConvert.SerializeObject(order);
    }

    public void UpdateOrder(int starters, int mains, int drinks, TimeSpan time)
    {
      Order? order = JsonConvert.DeserializeObject<Order>(Json);

      if (order != null)
      {
        order.Starter += starters;
        order.Main += mains;

        if (time < TimeSpan.FromHours(19))
          order.DrinkBefore19 += drinks;
        else order.Drink += drinks;
        
        Json = JsonConvert.SerializeObject(order);
      }
    }

    public void CancelOrder(int starters, int mains, int drinks)
    {
      Order? order = JsonConvert.DeserializeObject<Order>(Json);

      if (order != null)
      {
        order.Starter = order.Starter > 0 ? order.Starter - starters : 0;
        order.Main = order.Main > 0 ? order.Main - mains : 0;
        order.DrinkBefore19 = order.DrinkBefore19 > 0 ? order.DrinkBefore19 - drinks : 0;
        order.Drink = order.Drink > 0 ? order.Drink - drinks : 0;

        Json = JsonConvert.SerializeObject(order);
      }
    }

    public double Checkout()
    {
      Order? order = JsonConvert.DeserializeObject<Order>(Json);

      if (order == null)
        return -1;

      return order.Calculate();
    }
  }
}
