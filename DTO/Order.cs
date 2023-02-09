using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DTO
{
  public class Order
  {
    public int Starter { get; set; }
    public int Main { get; set; }
    public int Drink { get; set; }
    public int DrinkBefore19 { get; set; }

    public double Calculate()
    {
      double price = (Starter * 4.00) + (Main * 7.00) + (Drink * 2.50) + (DrinkBefore19 * (2.50 - (2.50 * 0.30)));
      double finalPrice = price + (price * 0.10);
      return finalPrice;
    }
  }
}
