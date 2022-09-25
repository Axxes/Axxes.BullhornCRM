using Axxes.BullhornCRM.Exceptions;
using Axxes.BullhornCRM.Utility.Models;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Utility.Fetchers;

internal sealed class AuthorizationCodeFetcher : BaseFetcher
{
    private readonly BullhornAuthCredentials _bullhornAuthCredentials;
    
    public AuthorizationCodeFetcher(HttpClient client, BullhornAuthCredentials bullhornAuthCredentials) : base(client)
    {
        _bullhornAuthCredentials = bullhornAuthCredentials;
    }

    public async Task<AuthorizationCode> Fetch(string code, string clientId, string clientSecret)
    {
        var authUri =
            $"{_bullhornAuthCredentials.TokenEndpoint}?grant_type=authorization_code&code={code}&client_id={clientId}&client_secret={clientSecret}";
        var result = await _client.PostAsync(authUri, new StringContent(""));

        if (!result.IsSuccessStatusCode)
            throw new AuthorizationCodeFetchFailedException(
                $"Fetching authorization code failed: {result.StatusCode} '{result.Content.ReadAsStringAsync().GetAwaiter().GetResult()}'");
            
        var str = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AuthorizationCode>(str);
    }
}