using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models.CrmEntities
{
    public sealed class SubscriptionEvent
    {
        [JsonProperty("requestId")]
        public long RequestId { get; set; }

        [JsonProperty("events")]
        public Event[] Events { get; set; }
        
        public sealed class Event
        {
            [JsonProperty("eventId")]
            public string EventId { get; set; }

            [JsonProperty("eventType")]
            public string EventType { get; set; }

            [JsonProperty("eventTimestamp")]
            public long EventTimestamp { get; set; }
            
            [JsonProperty("eventMetadata")]
            public EventMetadata EventMetadata { get; set; }

            [JsonProperty("entityName")]
            public string EntityName { get; set; }

            [JsonProperty("entityId")]
            public long EntityId { get; set; }

            [JsonProperty("entityEventType")]
            public string EntityEventType { get; set; }

            [JsonProperty("updatedProperties")]
            public string[] UpdatedProperties { get; set; }
        }
        
        public sealed class EventMetadata
        {
            [JsonProperty("PERSON_ID")]
            public string PersonId { get; set; }
        }
    }
}