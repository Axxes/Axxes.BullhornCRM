using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities;

public class BullhornFind
{
    public BullhornFind()
    {
        
    }

    public BullhornFind(Dictionary<string, string> properties)
    {
        Query = string.Join(" AND ", properties.Select(p => $"{p.Key}:\"{p.Value.Replace(" ", " ")}\""));
    }
    [JsonProperty("query")] public string Query { get; set; }
}