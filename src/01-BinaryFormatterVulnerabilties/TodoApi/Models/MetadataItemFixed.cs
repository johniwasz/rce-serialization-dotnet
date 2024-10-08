using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class MetadataItemFixed
    {
        public string Name { get; set; }

        [JsonConverter(typeof(BinaryJsonFixedConverter))]
        public object Data { get; set; }
    }
}