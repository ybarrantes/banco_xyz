using MySql.Data.MySqlClient;
using System;

namespace BancoXYZ.Business.Repository.DbConnection
{
    public abstract class MySqlDbConnection : IDisposable
    {
        public MySqlConnection Connection { get; internal set; }

        public MySqlDbConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
