using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Utility.Models;

public class RestToken
{
    public RestToken()
    {
    }

    [JsonProperty("BhRestToken")]
    public string BhRestToken { get; set; }

    [JsonProperty("restUrl")]
    public Uri RestUrl { get; set; }
}