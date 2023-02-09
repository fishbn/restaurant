namespace Checkout.DTO
{
  public class Order
  {
    public Guid Id { get; private set; }
    public int Starter { get; set; } = 0;
    public int Main { get; set; } = 0;
    public int Drink { get; set; } = 0; 
    public int DrinkBefore19 { get; set; } = 0;

    public Order()
    {
      Id = Guid.NewGuid();
    }

    public double Calculate()
    {
      double priceWithoutDiscount = CalculatePrice();
      double discount = CalculateDiscount(priceWithoutDiscount);
      return priceWithoutDiscount + discount;
    }

    private double CalculatePrice()
    {
      double price = (Starter * 4.00) + (Main * 7.00) + (Drink * 2.50) + (DrinkBefore19 * (2.50 - (2.50 * 0.30)));
      return Math.Round(price, 2);
    }

    private double CalculateDiscount(double priceWithoutDiscount)
    {
      double discount = priceWithoutDiscount * 0.10;
      return Math.Round(discount, 2);
    }

    public override string ToString()
    {
      return $"Order: # {Id}" + Environment.NewLine +
             $"(£4.00) Starters: {Starter}" + Environment.NewLine +
             $"(£7.00) Mains: {Main}" + Environment.NewLine +
             $"(£2.25) Drinks (%): {DrinkBefore19}" + Environment.NewLine +
             $"(£2.50) Drinks: {Drink}" + Environment.NewLine +
             $"(10%) Service: £{CalculateDiscount(CalculatePrice())}" + Environment.NewLine +
             $"TOTAL: £{Calculate()}" + Environment.NewLine;
    }
  }
}
