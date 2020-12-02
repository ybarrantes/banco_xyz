using BancoXYZ.Business.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BancoXYZ.Business.Models
{
    [Serializable]
    public class Response
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultType Result { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AcceptType Accept { get; set; }

        public string Body { get; set; }
        public string Message { get; set; }
    }
}
