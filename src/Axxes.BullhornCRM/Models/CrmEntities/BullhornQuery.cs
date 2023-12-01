using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities;

internal class BullhornQuery
{
    [JsonProperty("where")] public string Where { get; set; }
}