using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(ClientCorporation))]
public class ClientCorporation : IBullhornEntity
{
    public ClientCorporation()
    {
        Address = new AddressType();
    }
        
    [JsonProperty("id")] public long Id { get; set; }
        
    [JsonProperty("address")] public AddressType Address { get; set; }
        
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("phone")] public string Phone { get; set; }
    [JsonProperty("status")] public string Status { get; set; }
}
    
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