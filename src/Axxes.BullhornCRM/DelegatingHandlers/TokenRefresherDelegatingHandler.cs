using Axxes.BullhornCRM.Utility;
using Axxes.BullhornCRM.Utility.Models;

namespace Axxes.BullhornCRM.DelegatingHandlers;

internal class BullhornTokenHandler : DelegatingHandler
{
    private readonly BullhornAuthCredentials _bullhornAuthCredentials;
    private readonly TokenProvider _tokenProvider;
    private DateTime _expires;
    private string _bhRestToken;
    private string _baseUrl;
        
    public BullhornTokenHandler(BullhornAuthCredentials bullhornAuthCredentials, TokenProvider tokenProvider)
    {
        InnerHandler = new HttpClientHandler();
        _bullhornAuthCredentials = bullhornAuthCredentials;
        _tokenProvider = tokenProvider;
        _expires = DateTime.UtcNow.AddMinutes(-10);
    }

    public bool Authorized() => _expires >= DateTime.UtcNow;
        
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!Authorized())
        {
            var token = await _tokenProvider.Retrieve(_bullhornAuthCredentials);
            _baseUrl = token.RestUrl.ToString();
            _bhRestToken = token.BhRestToken.ToString();
            _expires = DateTime.UtcNow.AddMinutes(9);
        }

        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace(Settings.BaseUri, _baseUrl));
        
        request.Headers.Add("BhRestToken", _bhRestToken);
            
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