using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(Placement))]
public class Placement : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("status")] public string Status { get; set; }
    [JsonProperty("employmentType")] public string EmploymentType { get; set; }
    [JsonProperty("candidate")] public PlacementCandidate Candidate { get; set; }
    [JsonProperty("clientCorporation")] public PlacementClientCorporation ClientCorporation { get; set; }

    [JsonProperty("payRate")] public decimal? CostPrice { get; set; }
    [JsonProperty("clientBillRate")] public decimal? SellingPrice { get; set; }
    [JsonProperty("dateBegin")] public long? StartDate { get; set; }
    [JsonProperty("dateEnd")] public long? EndDate { get; set; }

    [JsonProperty("comments")] public string Comments { get; set; }

    public class PlacementCandidate
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("firstName")] public string FirstName { get; set; }
        [JsonProperty("lastName")] public string LastName { get; set; }
        [JsonProperty("employmentPreference")] public string EmploymentPreference { get; set; }
    }

    public class PlacementClientCorporation
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}