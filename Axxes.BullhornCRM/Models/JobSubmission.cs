using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(JobSubmission))]
public class JobSubmission : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("billRate")] public decimal? BillRate { get; set; }

    [JsonProperty("candidate")] public CandidateType Candidate { get; set; }

    [JsonProperty("comments")] public string Comments { get; set; }

    [JsonProperty("jobOrder")] public JobOrderType JobOrder { get; set; }

    [JsonProperty("startDate")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? StartDate { get; set; }

    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateAdded { get; set; }

    [JsonProperty("status")] public string Status { get; set; }


    public class CandidateType
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }

        [JsonProperty("_subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtype { get; set; }
    }

    public class JobOrderType
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("title")] public string Title { get; set; }
    }
}