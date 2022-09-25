using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

public class EditHistory : IBullhornEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }
        
    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTimeOffset DateAdded { get; set; }
        
    [JsonProperty("fieldChanges")] public FieldsChangesType FieldChanges { get; set; }
        
    public class FieldsChangesType
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("data")]
        public DataType[] Data { get; set; }
            
        public class DataType
        {
            [JsonProperty("id")]
            public long Id { get; set; }
        
            [JsonProperty("columnName")]
            public string ColumnName { get; set; }
        
            [JsonProperty("display")]
            public string Display { get; set; }
        
            [JsonProperty("newValue")]
            public string NewValue { get; set; }
        
            [JsonProperty("oldValue")]
            public string OldValue { get; set; }
        }
    }
}