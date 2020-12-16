using System.Collections.Generic;
using System.Linq;
using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Repository.DbConnection;
using MySql.Data.MySqlClient;
using System;
using BancoXYZ.Business.Types;

namespace BancoXYZ.Business.Repository
{
    public class TransactionRepository : MySqlDbConnection
    {
        public TransactionRepository(string connectionString)
            : base(connectionString)
        { }

        public double GetSaldo(string accountNumber)
        {
            MySqlCommand command = this.Connection.CreateCommand();
            command.CommandText = $"SELECT " +
                    $"valor " +
                $"FROM Saldo AS s " +
                $"INNER JOIN Cuenta AS c ON c.id = s.cuenta_id " +
                $"WHERE " +
                    $"c.numero = @numero " +
                    $"AND s.estado_id = 1 " +
                    $"AND c.estado_id = 1 " +
                $"ORDER BY s.fecha_creacion DESC " +
                $"LIMIT 1";
            command.Parameters.AddWithValue("@numero", accountNumber);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return Convert.ToDouble(reader["valor"]);
            }
            return 0d;
        }

        public Movimiento RegistrarMovimiento(Movimiento movimiento)
        {
            Dictionary<string, string> map = new Dictionary<string, string>
            {
                { "valor", movimiento.Valor.ToString() },
                { "tipo_movimiento_id", ((int)movimiento.TipoMovimiento).ToString() },
                { "cuenta_id", movimiento.Cuenta.Id.ToString() },
            };
            string fields = string.Join(",", map.Keys);
            string values = string.Join(",", map.Keys.Select(x => $"@{x}"));

            MySqlCommand command = this.Connection.CreateCommand();
            command.CommandText = $"INSERT INTO Movimiento ({fields}) VALUES ({values}); SELECT last_insert_id();";

            foreach(KeyValuePair<string, string> keyValuePair in map)
            {
                command.Parameters.AddWithValue($"@{keyValuePair.Key}", keyValuePair.Value);
            }

            movimiento.Id = Convert.ToInt32(command.ExecuteScalar());
            return movimiento;
        }

        public Saldo RegistrarSaldo(Saldo saldo)
        {
            Dictionary<string, string> map = new Dictionary<string, string>
            {
                { "valor", saldo.Valor.ToString() },
                { "movimiento_id", saldo.Movimiento.Id.ToString() },
                { "estado_id", ((int)saldo.Estado).ToString() },
                { "cuenta_id", saldo.Cuenta.Id.ToString() },
            };
            string fields = string.Join(",", map.Keys);
            string values = string.Join(",", map.Keys.Select(x => $"@{x}"));

            MySqlCommand command = this.Connection.CreateCommand();
            command.CommandText = $"INSERT INTO Saldo (valor, movimiento_id, cuenta_id, estado_id) " +
                $"SELECT " +
                    $"COALESCE((" +
                        $"SELECT " +
                            $"valor " +
                        $"FROM Saldo " +
                        $"WHERE cuenta_id = @cuenta_id " +
                        $"ORDER BY fecha_creacion DESC " +
                        $"LIMIT 1" +
                    $"), 0) + @valor, " +
                    $"@movimiento_id, " +
                    $"@cuenta_id, " +
                    $"@estado_id; " +
                $"SELECT last_insert_id();";

            foreach (KeyValuePair<string, string> keyValuePair in map)
            {
                command.Parameters.AddWithValue($"@{keyValuePair.Key}", keyValuePair.Value);
            }

            saldo.Id = Convert.ToInt32(command.ExecuteScalar());
            return saldo;
        }
    }
}
