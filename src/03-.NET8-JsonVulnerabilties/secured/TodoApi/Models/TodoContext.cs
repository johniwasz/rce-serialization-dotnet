using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TodoApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;


    private string Serialize(Dictionary<string, object> data, JsonSerializerSettings serSettings)
    {
        string serializedData = JsonConvert.SerializeObject(data, serSettings);
        return serializedData;
    }

    private Dictionary<string, object>? DeserializeDict(string data, JsonSerializerSettings serSettings)
    {
        Dictionary<string, object>? retVal = JsonConvert.DeserializeObject<Dictionary<string, object>>(data, serSettings);
        return retVal;
    }

    private string Serialize(object? data, JsonSerializerSettings serSettings)
    {
        return JsonConvert.SerializeObject(data, serSettings);
    }

    private object? Deserialize(string data, JsonSerializerSettings serSettings)
    {
        return JsonConvert.DeserializeObject<object?>(data, serSettings);
    }
}