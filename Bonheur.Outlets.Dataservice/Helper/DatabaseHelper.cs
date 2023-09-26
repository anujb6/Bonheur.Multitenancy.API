using Bonheur.Outlets.Dataservice.Abstracts.Database;
using MySqlConnector;

namespace Bonheur.Outlets.Dataservice.Helper
{
    public class DatabaseHelper : IDatabaseHelper
    {
        public async Task<bool> CreateShopDatabaseAsync(string connectionString, string shopName)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string createDatabaseQuery = $"CREATE DATABASE {shopName}";
                using MySqlCommand command = new MySqlCommand(createDatabaseQuery, connection);
                var data = await command.ExecuteNonQueryAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
