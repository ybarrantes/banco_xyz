using System.Collections.Generic;
using System.Linq;
using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Repository.DbConnection;
using MySql.Data.MySqlClient;
using BancoXYZ.Business.Exceptions;

namespace BancoXYZ.Business.Repository
{
    public class ClientRepository : MySqlDbConnection
    {
        public ClientRepository(string connectionString)
            : base(connectionString)
        { }

        public Cliente CreateClient(Cliente cliente)
        {
            Dictionary<string, string> map = new Dictionary<string, string>
            {
                { "tipo_documento_id", ((int)cliente.TipoDocumento).ToString() },
                { "documento", cliente.Documento.ToString() },
                { "nombres", cliente.Nombres },
                { "apellidos", cliente.Apellidos },
                { "direccion", cliente.Direccion },
                { "telefono", cliente.Telefono },
                { "fecha_nacimiento", cliente.FechaNacimiento.ToString("yyyy-MM-dd") },
                { "genero_id", ((int)cliente.Genero).ToString() },
                { "estado_id", ((int)cliente.Estado).ToString() },
                { "ciudad_id", cliente.Ciudad.Id.ToString() },
            };
            string fields = string.Join(",", map.Keys);
            string values = string.Join(",", map.Keys.Select(x => $"@{x}"));

            MySqlCommand command = this.Connection.CreateCommand();
            command.CommandText = $"INSERT INTO Cliente ({fields}) VALUES ({values})";

            foreach(KeyValuePair<string, string> keyValuePair in map)
            {
                command.Parameters.AddWithValue($"@{keyValuePair.Key}", keyValuePair.Value);
            }

            if (command.ExecuteNonQuery() != 1)
                throw new RepositoryException("No fue posible guardar los datos del cliente!");

            return cliente;
        }
    }
}
