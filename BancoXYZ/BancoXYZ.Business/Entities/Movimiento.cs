using BancoXYZ.Business.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BancoXYZ.Business.Entities
{
    [Serializable]
    public class Movimiento
    {
        public long Id { get; set; }
        public double Valor { get; set; }

        public Cuenta Cuenta { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoMovimientoType TipoMovimiento { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
