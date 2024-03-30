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
    public DbSet<MetadataItem> MetadataItems { get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MetadataItem>().HasIndex(e => e.Name);


        JsonSerializerSettings serSettings = new JsonSerializerSettings
        {
         //   TypeNameHandling = TypeNameHandling.All,
         //   TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };
        
        /*
        modelBuilder.Entity<TodoItem>().Property(e => e.MetaList)
               .HasConversion(
                    data => 
                   Serialize(data, serSettings),
                    data => 
                     DeserializeDict(data, serSettings));


        modelBuilder.Entity<MetadataItem>().Property(e => e.Data)
               .HasConversion(
                    data => 
                    Serialize(data, serSettings),
                    data => 
                    Deserialize(data, serSettings));
*/                        
    }


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