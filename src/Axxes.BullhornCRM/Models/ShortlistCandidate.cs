using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(ShortlistCandidate))]
public class ShortlistCandidate : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("appointments")] public AppointmentsType Appointments { get; set; }

    [JsonProperty("billRate")] public decimal? BillRate { get; set; }

    [JsonProperty("branch")] public object Branch { get; set; }

    [JsonProperty("candidate")] public CandidateType Candidate { get; set; }

    [JsonProperty("comments")] public string Comments { get; set; }

    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateAdded { get; set; }

    [JsonProperty("dateLastModified")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateLastModified { get; set; }

    [JsonProperty("dateWebResponse")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateWebResponse { get; set; }

    [JsonProperty("endDate")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? EndDate { get; set; }

    [JsonProperty("isDeleted")] public bool IsDeleted { get; set; }

    [JsonProperty("isHidden")] public bool IsHidden { get; set; }

    [JsonProperty("jobOrder")] public JobOrderType JobOrder { get; set; }

    [JsonProperty("jobSubmissionCertificationRequirements")]
    public AppointmentsType JobSubmissionCertificationRequirements { get; set; }

    [JsonProperty("latestAppointment")] public object LatestAppointment { get; set; }

    [JsonProperty("migrateGUID")] public string MigrateGuid { get; set; }

    [JsonProperty("owners")] public AppointmentsType Owners { get; set; }

    [JsonProperty("payRate")] public decimal? PayRate { get; set; }

    [JsonProperty("salary")] public decimal? Salary { get; set; }

    [JsonProperty("sendingUser")] public CandidateType SendingUser { get; set; }

    [JsonProperty("source")] public string Source { get; set; }

    [JsonProperty("startDate")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? StartDate { get; set; }

    [JsonProperty("status")] public string Status { get; set; }

    [JsonProperty("tasks")] public AppointmentsType Tasks { get; set; }

    public class AppointmentsType
    {
        [JsonProperty("total")] public long Total { get; set; }

        [JsonProperty("data")] public object[] Data { get; set; }
    }

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