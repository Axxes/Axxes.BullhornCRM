using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

public class JobOrder : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("address")] public AddressType Address { get; set; }

    [JsonProperty("billRateCategoryID")] public long? BillRateCategoryId { get; set; }

    [JsonProperty("bonusPackage")] public string BonusPackage { get; set; }

    [JsonProperty("branch")] public object Branch { get; set; }

    [JsonProperty("branchCode")] public string BranchCode { get; set; }

    [JsonProperty("clientBillRate")] public decimal? ClientBillRate { get; set; }

    [JsonProperty("clientContact")] public ClientContactType ClientContact { get; set; }

    [JsonProperty("clientCorporation")] public ClientCorporationType ClientCorporation { get; set; }

    [JsonProperty("costCenter")] public string CostCenter { get; set; }

    [JsonProperty("customText1")] public string[] TeamFreelance { get; set; }

    [JsonProperty("customText11")] public string OnScrumboard { get; set; }

    [JsonProperty("customText4")] public string BusinessUnit { get; set; }

    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateAdded { get; set; }

    [JsonProperty("dateClosed")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateClosed { get; set; }

    [JsonProperty("dateEnd")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateEnd { get; set; }

    [JsonProperty("dateLastExported")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateLastExported { get; set; }

    [JsonProperty("dateLastModified")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateLastModified { get; set; }

    [JsonProperty("dateLastPublished")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? DateLastPublished { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("durationWeeks")] public decimal? DurationWeeks { get; set; }

    [JsonProperty("educationDegree")] public string EducationDegree { get; set; }

    [JsonProperty("employmentType")] public string EmploymentType { get; set; }

    [JsonProperty("externalCategoryID")] public int? ExternalCategoryId { get; set; }

    [JsonProperty("externalID")] public string ExternalId { get; set; }

    [JsonProperty("feeArrangement")] public decimal? FeeArrangement { get; set; }

    [JsonProperty("hoursOfOperation")] public string HoursOfOperation { get; set; }

    [JsonProperty("hoursPerWeek")] public decimal? HoursPerWeek { get; set; }

    [JsonProperty("isClientEditable")] public bool? IsClientEditable { get; set; }

    [JsonProperty("isDeleted")] public bool? IsDeleted { get; set; }

    [JsonProperty("isInterviewRequired")] public bool? IsInterviewRequired { get; set; }

    [JsonProperty("isJobcastPublished")] public bool? IsJobcastPublished { get; set; }

    [JsonProperty("isOpen")] public bool? IsOpen { get; set; }

    [JsonProperty("isPublic")] public long? IsPublic { get; set; }

    [JsonProperty("jobBoardList")] public string JobBoardList { get; set; }

    [JsonProperty("location")] public object Location { get; set; }

    [JsonProperty("markUpPercentage")] public decimal? MarkUpPercentage { get; set; }

    [JsonProperty("numOpenings")] public int? NumOpenings { get; set; }

    [JsonProperty("onSite")] public string OnSite { get; set; }

    [JsonProperty("opportunity")] public object Opportunity { get; set; }

    [JsonProperty("optionsPackage")] public string OptionsPackage { get; set; }

    [JsonProperty("owner")] public ClientContactType Owner { get; set; }

    [JsonProperty("payRate")] public long PayRate { get; set; }

    [JsonProperty("reasonClosed")] public string ReasonClosed { get; set; }

    [JsonProperty("reportTo")] public string ReportTo { get; set; }

    [JsonProperty("reportToClientContact")]
    public object ReportToClientContact { get; set; }

    [JsonProperty("salary")] public long? Salary { get; set; }

    [JsonProperty("salaryUnit")] public string SalaryUnit { get; set; }

    [JsonProperty("startDate")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime? StartDate { get; set; }

    [JsonProperty("status")] public string Status { get; set; }

    [JsonProperty("submissions")] public SubmissionsType Submissions { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("travelRequirements")] public string TravelRequirements { get; set; }

    [JsonProperty("type")] public long? Type { get; set; }

    [JsonProperty("usersAssigned")] public string UsersAssigned { get; set; }

    [JsonProperty("willRelocate")] public bool? WillRelocate { get; set; }

    [JsonProperty("willRelocateInt")] public long? WillRelocateInt { get; set; }

    [JsonProperty("willSponsor")] public bool? WillSponsor { get; set; }

    [JsonProperty("workersCompRate")] public object WorkersCompRate { get; set; }

    [JsonProperty("yearsRequired")] public long? YearsRequired { get; set; }

    public class AddressType
    {
        [JsonProperty("address1")] public string Address1 { get; set; }

        [JsonProperty("address2")] public string Address2 { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("zip")] public string Zip { get; set; }

        [JsonProperty("countryID")] public long CountryId { get; set; }

        [JsonProperty("countryName")] public string CountryName { get; set; }

        [JsonProperty("countryCode")] public string CountryCode { get; set; }
    }

    public class SubmissionsType
    {
        [JsonProperty("total")] public long Total { get; set; }

        [JsonProperty("data")] public SubmissionsItemType[] Data { get; set; }

        public class SubmissionsItemType
        {
            [JsonProperty("id")] public long Id { get; set; }
        }
    }

    public class ClientContactType
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }
    }

    public class ClientCorporationType
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}