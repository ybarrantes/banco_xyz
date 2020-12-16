using MySql.Data.MySqlClient;
using System;

namespace BancoXYZ.Business.Repository.DbConnection
{
    public abstract class MySqlDbConnection : IDisposable
    {
        public MySqlConnection Connection { get; internal set; }
        public MySqlTransaction Transaction { get; internal set; }

        public MySqlDbConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void BeginTransaction()
        {
            if (Transaction == null)
                Transaction = Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (Transaction != null)
                Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (Transaction != null)
                Transaction.Rollback();
            Transaction = null;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
