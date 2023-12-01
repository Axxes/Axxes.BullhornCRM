using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(Specialty))]
[EntityAssociation("specialties")]
public class Specialty : IBullhornToManyAssociationEntity
{
    [JsonProperty("id")] public long Id { get; set; }
    
    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset? DateAdded { get; set; }
    
    [JsonProperty("enabled")] public bool Enabled { get; set; }
    
    [JsonProperty("name")] public string Name { get; set; }
}