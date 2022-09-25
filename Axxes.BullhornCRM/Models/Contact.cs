using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(Contact))]
public class Contact : IBullhornEntity
{
    public Contact()
    {
        Address = new AddressType();
        ClientCorporation = new ClientCorporationType();
    }

    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("address")] public AddressType Address { get; set; }
    [JsonProperty("email")] public string Email { get; set; }
    [JsonProperty("phone")] public string Phone { get; set; }
    [JsonProperty("firstName")] public string FirstName { get; set; }
    [JsonProperty("lastName")] public string LastName { get; set; }
    [JsonProperty("occupation")] public string Occupation { get; set; }
    [JsonProperty("clientCorporation")] public ClientCorporationType ClientCorporation { get; set; }

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

    public class ClientCorporationType
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}