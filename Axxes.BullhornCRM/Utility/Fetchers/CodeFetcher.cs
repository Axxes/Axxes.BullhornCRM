using System.Net;
using System.Web;
using Axxes.BullhornCRM.Exceptions;
using Axxes.BullhornCRM.Utility.Models;

namespace Axxes.BullhornCRM.Utility.Fetchers;

internal sealed class CodeFetcher : BaseFetcher
{
    private readonly BullhornAuthCredentials _bullhornAuthCredentials;
    
    public CodeFetcher(HttpClient client, BullhornAuthCredentials bullhornAuthCredentials) : base(client)
    {
        _bullhornAuthCredentials = bullhornAuthCredentials;
    }
        
    public async Task<string> Fetch(string clientId, string username, string password)
    {
        var uri = $"{_bullhornAuthCredentials.AuthorizeEndpoint}?response_type=code&client_id={clientId}&state=ips&password={password}&action=Login&username={username}";
        var result = await _client.GetAsync(uri);

        if (result.StatusCode != HttpStatusCode.Found)
            throw new CodeFetchFailedException(
                $"Failed to fetch code: {result.StatusCode} '{result.Content.ReadAsStringAsync().GetAwaiter().GetResult()}'");

        try
        {
            var location = result.Headers.Location;
            var query = HttpUtility.ParseQueryString(location.Query);
            return query.Get("code");
        }
        catch (Exception e)
        {
            throw new CodeFetchFailedException($"Could not fetch code because of error: '{e.Message}'");
        }
    }
}