using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(PlacementChangeRequest))]
public class PlacementChangeRequest : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("requestType")] public string RequestType { get; set; }
    [JsonProperty("placement")] public PlacementChangeRequestPlacement Placement { get; set; }

    public class PlacementChangeRequestPlacement
    {
        [JsonProperty("id")] public long Id { get; set; }
    }
}