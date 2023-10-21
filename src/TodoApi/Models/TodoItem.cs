using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }


    public Dictionary<string, object>? Metadata { get; set;}
    
    // public List<MetadataItem>? Metadata { get; set; }
}



