namespace Checkout.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var table1 = new Table();
            var table2 = new Table();
            var table3 = new Table();

            table1.CreateOrder(4, 4, 4, DateTime.Now.TimeOfDay);
            
            table2.CreateOrder(1, 2, 2, new TimeSpan(18, 59, 0));
            table2.UpdateOrder(0, 2, 2, new TimeSpan(19,0,0));

            table3.CreateOrder(4, 4, 4, new TimeSpan(18,0,0));
            double currentPrice3 = table3.Checkout();
            table3.CancelOrder(1, 1, 1);
            
            currentPrice3 = table3.Checkout();
            double currentPrice1 = table1.Checkout();
            double currentPrice2 = table2.Checkout();
            
        }
    }
}