namespace Checkout.Data;

public static class TestData
{
  public static IEnumerable<object[]> Scenario1()
  {
    yield return new object[] { 4, 4, 4, DateTime.Now.TimeOfDay, 56.1 };
  }

  public static IEnumerable<object[]> Scenario2()
  {
    yield return new object[] { 1, 2, 2, new TimeSpan(18, 59, 0), 23.65, 44.55 };
  }

  public static IEnumerable<object[]> Scenario3()
  {
    yield return new object[] { 4, 4, 4, new TimeSpan(18, 0, 0), 56.1, 42.07};
  }
}