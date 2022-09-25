using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities;

internal class CrmChangeResponse
{
    [JsonProperty("changedEntityId")] public long ChangedEntityId { get; set; }

    [JsonProperty("changeType")] public string ChangeType { get; set; }
}