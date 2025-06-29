using Dapper;
using MySql.Data.MySqlClient;

namespace meuprimeirocrud_karol.Infrastructure
{
    public class Connection
    {
        protected string connectionString = "Server=localhost;Database=raizes;User=root;Password=root;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public async Task<int> Execute(string sql, object obj)
        {
            using (MySqlConnection con = GetConnection())
            {
                return await con.ExecuteAsync(sql, obj);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            using var connection = GetConnection();
            return await connection.ExecuteScalarAsync<T>(sql, param);
        }
    }
}