using Axxes.BullhornCRM.Utility;
using Axxes.BullhornCRM.Utility.Models;

namespace Axxes.BullhornCRM.DelegatingHandlers;

internal class BullhornTokenHandler : DelegatingHandler
{
    private readonly BullhornAuthCredentials _bullhornAuthCredentials;
    private readonly TokenProvider _tokenProvider;
    private readonly BullhornToken _bullhornToken;
    private string _baseUrl;
        
    public BullhornTokenHandler(BullhornAuthCredentials bullhornAuthCredentials, TokenProvider tokenProvider, BullhornToken bullhornToken)
    {
        InnerHandler = new HttpClientHandler();
        _bullhornAuthCredentials = bullhornAuthCredentials;
        _tokenProvider = tokenProvider;
        _bullhornToken = bullhornToken;
        _bullhornToken.Expires ??= DateTime.UtcNow.AddMinutes(-10);
    }

    public bool Authorized() => _bullhornToken.Expires >= DateTime.UtcNow;
        
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!Authorized())
        {
            var token = await _tokenProvider.Retrieve(_bullhornAuthCredentials);
            _baseUrl = token.RestUrl.ToString();
            _bullhornToken.BhRestToken = token.BhRestToken;
            _bullhornToken.Expires = DateTime.UtcNow.AddMinutes(9);
        }

        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace(Settings.BaseUri, _baseUrl));
        
        request.Headers.Add("BhRestToken", _bullhornToken.BhRestToken);
            
        HttpResponseMessage response = null;
        for (var i = 0; i < 5; i++)
        {
            response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode) {
                return response;
            }
        }

        return response;
    }
}