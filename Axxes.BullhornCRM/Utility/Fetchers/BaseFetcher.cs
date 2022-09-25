namespace Axxes.BullhornCRM.Utility.Fetchers;

internal abstract class BaseFetcher
{
    protected HttpClient _client;

    protected BaseFetcher(HttpClient client)
    {
        _client = client;
    }
}