using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastWeather.Model.ZipCodes
{
    public class ZipCodeDBContext : DbContext
    {
        private const string _connectionString = $"Data Source={DB_FILENAME};";
        public const string DB_FILENAME = "ZipCodeDB.sqlite";

        public DbSet<ZipCodeInfo> ZipCodes { get; set; }

        public void Init()
        {
            // Create a new database file
            SQLiteConnection.CreateFile(DB_FILENAME);

            // Create a connection to the database
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = @"CREATE TABLE IF NOT EXISTS ZipCodes (
ZipCode INTEGER PRIMARY KEY, 
CountryCode TEXT,
PlaceName  TEXT,
AdminName1 TEXT,
AdminCode1 TEXT,
AdminName2 TEXT,
AdminCode2 TEXT,
AdminName3 TEXT,
AdminCode3 TEXT,
Latitude   REAL,
Longitude  REAL,
Accuracy INTEGER)";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ZipCodeInfo>().HasKey(x => x.ZipCode);
        }
    }
}
