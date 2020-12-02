using BancoXYZ.Business.Types;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Text.Json.Serialization;

namespace BancoXYZ.Business.Models
{
    [Serializable]
    public class Request
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MethodType Method { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AcceptType Accept { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CommandType Command { get; set; }

        public IDictionary Arguments { get; set; }
        public string Body { get; set; }
        public string Authorization { get; set; }
    }
}
