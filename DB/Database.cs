using System.Data.SQLite;
using Checkout.Models;
using Xunit.Abstractions;

namespace Checkout.DB;

public class Database
{
  private readonly ITestOutputHelper _logger;
  private SQLiteConnection connection;

  public Database(ITestOutputHelper logger)
  {
    _logger = logger;
    InitializeSqLiteDatabase();
  }

  private void InitializeSqLiteDatabase()
  {
    connection = new SQLiteConnection("Data Source=example.db;Version=3;New=True;Compress=True;");
    connection.Open();

    using SQLiteCommand command = new SQLiteCommand(
      "CREATE TABLE IF NOT EXISTS orders (" +
      "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
      "starters INTEGER DEFAULT 0, " +
      "mains INTEGER DEFAULT 0, " +
      "drinksBefore19 INTEGER DEFAULT 0, " +
      "drinks INTEGER DEFAULT 0);"
      , connection);
    command.ExecuteNonQuery();
  }

  public int CreateOrder(Order order)
  {
    using var command = new SQLiteCommand("INSERT INTO orders (starters, mains, drinksBefore19, drinks) VALUES (@Starters, @Mains, @DrinksBefore19, @Drinks);", connection);
    command.Parameters.AddWithValue("@Starters", order.Starter);
    command.Parameters.AddWithValue("@Mains", order.Main);
    command.Parameters.AddWithValue("@DrinksBefore19", order.DrinkBefore19);
    command.Parameters.AddWithValue("@Drinks", order.Drink);
    command.ExecuteNonQuery();

    using SQLiteCommand getIdCommand = new SQLiteCommand("SELECT last_insert_rowid();", connection);
    int id = Convert.ToInt32(getIdCommand.ExecuteScalar());
    Console.WriteLine("Inserted order with ID: " + id);
    return id;
  }

  public void GetOrder()
  {
    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM orders;", connection))
    {
      using (SQLiteDataReader reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          _logger.WriteLine("Database DATA:" + Environment.NewLine + 
                            "ID: " + reader["id"] + Environment.NewLine + 
                            "Starters: " + reader["starters"] + Environment.NewLine + 
                            "Mains: " + reader["mains"] + Environment.NewLine +
                            "DrinksBefore19: " + reader["drinksBefore19"] + Environment.NewLine +
                            "Drinks: " + reader["drinks"] + Environment.NewLine);
        }
      }
    }
  }

  public void UpdateOrder(Order order)
  {
    using SQLiteCommand command = new SQLiteCommand(
      "UPDATE orders " +
      "SET starters = @Starters, " +
      "mains = @Mains, " +
      "drinksBefore19 = @DrinksBefore19, " +
      "drinks = @Drinks " +
      "WHERE id = @Id;", connection);

      command.Parameters.AddWithValue("@Id", order.Id);
      command.Parameters.AddWithValue("@Starters", order.Starter);
      command.Parameters.AddWithValue("@Mains", order.Main);
      command.Parameters.AddWithValue("@DrinksBefore19", order.DrinkBefore19);
      command.Parameters.AddWithValue("@Drinks", order.Drink);
      command.ExecuteNonQuery();
  }

  public void DeleteOrder(int id)
  {
    using SQLiteCommand command = new SQLiteCommand("DELETE FROM orders WHERE id = @Id;", connection);
    command.Parameters.AddWithValue("@Id", id);
    command.ExecuteNonQuery();
  }
}