using BancoXYZ.Business.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BancoXYZ.Business.Entities
{
    [Serializable]
    public class Cliente
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoDocumentoType TipoDocumento { get; set; }

        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GeneroType Genero { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoType Estado { get; set; }

        public Ciudad Ciudad { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
