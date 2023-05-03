using Axxes.BullhornCRM.Exceptions;
using Axxes.BullhornCRM.Utility.Models;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Utility.Fetchers;

internal sealed class RestTokenFetcher : BaseFetcher
{
    private readonly BullhornAuthCredentials _bullhornAuthCredentials;
    
    public RestTokenFetcher(HttpClient client, BullhornAuthCredentials bullhornAuthCredentials) : base(client)
    {
        _bullhornAuthCredentials = bullhornAuthCredentials;
    }

    public async Task<RestToken> Fetch(string accessToken)
    {
        var restUri = $"{_bullhornAuthCredentials.LoginEndpoint}?version=2.0&access_token={accessToken}";

        var result = await _client.GetAsync(restUri);

        if (!result.IsSuccessStatusCode)
            throw new RestTokenFetchFailedException(
                $"Could not fetch rest token: {restUri} {result.StatusCode} '{result.Content.ReadAsStringAsync().GetAwaiter().GetResult()}'");

        var content = await result.Content.ReadAsStringAsync();
            
        return JsonConvert.DeserializeObject<RestToken>(content);
    } 
}