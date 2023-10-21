using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoApi.Models;

public class MetadataItem
{
    public long Id { get; set; }

    public string? Name { get; set;}

    public object? Data { get; set;}

    [JsonIgnore]
    [ForeignKey("Id")]
    public TodoItem? ParentItem { get; set;}
}