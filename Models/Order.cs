namespace Checkout.Models
{
  public class Order
  {
    public int Id { get; set; }
    public int Starter { get; set; } = 0;
    public int Main { get; set; } = 0;
    public int Drink { get; set; } = 0; 
    public int DrinkBefore19 { get; set; } = 0;

    public double Calculate()
    {
      double foodPriceWithoutDiscount = CalculateFoodPrice();
      double drinksPrice = CalculateDrinksPrice();
      double foodDiscount = CalculateDiscount(foodPriceWithoutDiscount);

      return foodPriceWithoutDiscount + foodDiscount + drinksPrice;
    }

    private double CalculateFoodPrice()
    {
      double foodPrice = Starter * 4.00 + Main * 7.00;
      return foodPrice;
    }

    private double CalculateDrinksPrice()
    {
      double drinksPrice = Drink * 2.50 + DrinkBefore19 * (2.50 - 2.50 * 0.30);
      return Math.Round(drinksPrice);
    }

    private double CalculateDiscount(double foodPriceWithoutDiscount)
    {
      double discount = foodPriceWithoutDiscount * 0.10;
      return Math.Round(discount, 2);
    }

    public override string ToString()
    {
      return $"Order: # {Id}" + Environment.NewLine +
             $"(£4.00) Starters: {Starter}" + Environment.NewLine +
             $"(£7.00) Mains: {Main}" + Environment.NewLine +
             $"(£2.25) Drinks (%): {DrinkBefore19}" + Environment.NewLine +
             $"(£2.50) Drinks: {Drink}" + Environment.NewLine +
             $"(10%) Service (FoodOnly): £{CalculateDiscount(CalculateFoodPrice())}" + Environment.NewLine +
             $"TOTAL: £{Calculate()}" + Environment.NewLine;
    }
  }
}
