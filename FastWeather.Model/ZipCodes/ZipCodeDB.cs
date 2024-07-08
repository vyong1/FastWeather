using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FastWeather.Models.ZipCodes
{
    /// <summary>
    /// SQLite DB of US zip codes.
    /// See: US_ZipCodesReadme.txt for schema info
    /// 
    /// TODO - Use EntityFramework as ORM
    /// </summary>
    public class ZipCodeDB
    {
        private const string _connectionString = $"Data Source={DB_FILENAME};Version=3;";
        public const string DB_FILENAME = "ZipCodeDB.sqlite";

        public ZipCodeDB()
        {

        }

        public void Init()
        {
            // Create a new database file
            SQLiteConnection.CreateFile(DB_FILENAME);

            // Create a connection to the database
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE IF NOT EXISTS " +
                    "ZipCodes (Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER)";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void AddData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", "John Doe");
                    command.Parameters.AddWithValue("@Age", 30);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void RetrieveData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Users";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);

                        Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
                    }
                }
                connection.Close();
            }
        }
    }
}
