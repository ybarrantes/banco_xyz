using BancoXYZ.Business.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BancoXYZ.Business.Entities
{
    [Serializable]
    public class Cuenta
    {
        public long Id { get; set; }

        public Cliente Cliente { get; set; }
        public string Numero { get; set; }
        public string Alias { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoType Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
