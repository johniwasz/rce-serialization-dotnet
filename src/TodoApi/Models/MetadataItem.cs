using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoApi.Models;

public class MetadataItem
{
    public long Id { get; set; }

    public string? Name { get; set;}

    [Newtonsoft.Json.JsonConverter(typeof(BinaryJsonConverter))]
    public object? Data { get; set;}
}