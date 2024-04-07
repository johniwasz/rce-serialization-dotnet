using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class MetadataItem
    {
        public string Name { get; set; }

        [JsonConverter(typeof(BinaryJsonConverter))]
        public object Data { get; set; }
    }
}