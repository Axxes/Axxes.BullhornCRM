using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(Candidate))]
[EntityHistoryName("UserEditHistory")]
public class Candidate : IBullhornEntityWithHistory
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("address")] public AddressType Address { get; set; }

    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset? DateAdded { get; set; }

    [JsonProperty("dateAvailable")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset? DateAvailable { get; set; }

    [JsonProperty("dateAvailableEnd")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset? DateAvailableEnd { get; set; }

    [JsonProperty("dateOfBirth")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset? DateOfBirth { get; set; }

    [JsonProperty("email")] public string Email { get; set; }
    [JsonProperty("email2")] public string WorkEmail { get; set; }
    [JsonProperty("employeeType")] public string EmployeeType { get; set; }
    [JsonProperty("employmentPreference")] public IEnumerable<string> EmploymentPreference { get; set; }
    [JsonProperty("firstName")] public string FirstName { get; set; }
    [JsonProperty("lastName")] public string LastName { get; set; }

    [JsonProperty("owner")] public OwnerType Owner { get; set; }
    [JsonProperty("phone")] public string Phone { get; set; }
    [JsonProperty("workPhone")] public string WorkPhone { get; set; }
    [JsonProperty("status")] public string Status { get; set; }
    [JsonProperty("companyName")] public string CurrentCompany { get; set; }

    [JsonProperty("massMailOptOut")]
    [JsonConverter(typeof(BooleanJsonConverter))]
    public bool BulkEmailOptOut { get; set; }

    [JsonProperty("specialties")] public FunctionsType Functions { get; set; }

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

    public class OwnerType
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("firstName")] public string FirstName { get; set; }
        [JsonProperty("lastName")] public string LastName { get; set; }
    }

    public class FunctionsType
    {
        [JsonProperty("total")] public int Total { get; set; }
        [JsonProperty("data")] public IEnumerable<FunctionType> Data { get; set; }

        public class FunctionType
        {
            [JsonProperty("id")] public long Id { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
        }
    }
}