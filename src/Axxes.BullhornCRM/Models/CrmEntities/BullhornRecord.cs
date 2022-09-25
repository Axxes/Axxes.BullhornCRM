using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities;

public class BullhornRecord<T> where T : IBullhornEntity
{
    [JsonProperty("data")] public T Data { get; set; }
}