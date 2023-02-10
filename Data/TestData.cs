namespace Checkout.Data;

public static class TestData
{
  public static IEnumerable<object[]> Scenario1()
  {
    TimeSpan time = DateTime.Now.TimeOfDay;
    double expected = time < TimeSpan.FromHours(19) ? 55.4 : 58.4;
    yield return new object[] { 4, 4, 4, time, expected };
  }

  public static IEnumerable<object[]> Scenario2()
  {
    yield return new object[] { 1, 2, 2, new TimeSpan(18, 59, 0), 23.8, 43.2 };
  }

  public static IEnumerable<object[]> Scenario3()
  {
    yield return new object[] { 4, 4, 4, new TimeSpan(18, 0, 0), 55.4, 41.3};
  }
}