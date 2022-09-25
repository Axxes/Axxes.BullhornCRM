using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Converters;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(Note))]
public class Note : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("dateAdded")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime DateAdded { get; set; }

    [JsonProperty("commentingPerson")]
    public NoteCommentingPerson CommentingPerson { get; set; }
}

public class NoteCommentingPerson
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }
}