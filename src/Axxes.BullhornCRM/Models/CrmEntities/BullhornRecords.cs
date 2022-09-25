using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities;

public class BullhornRecords<T>  where T : IBullhornEntity
{
    [JsonProperty("total")] public int Total { get; set; }
    [JsonProperty("data")] public T[] Data { get; set; }
}