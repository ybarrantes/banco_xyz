using BancoXYZ.Business.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BancoXYZ.Business.Entities
{
    [Serializable]
    public class Saldo
    {
        public long Id { get; set; }
        public double Valor { get; set; }

        public Cuenta Cuenta { get; set; }
        public Movimiento Movimiento { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoType Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
