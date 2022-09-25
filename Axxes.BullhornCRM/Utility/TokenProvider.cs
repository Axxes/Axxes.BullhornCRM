using Axxes.BullhornCRM.Utility.Fetchers;
using Axxes.BullhornCRM.Utility.Models;

namespace Axxes.BullhornCRM.Utility;

internal class TokenProvider
{
    private readonly CodeFetcher _code;
    private readonly AuthorizationCodeFetcher _authorizationCode;
    private readonly RestTokenFetcher _restToken;

    public TokenProvider(CodeFetcher code, AuthorizationCodeFetcher authorizationCode, RestTokenFetcher restToken)
    {
        _code = code;
        _authorizationCode = authorizationCode;
        _restToken = restToken;
    }

    public async Task<RestToken> Retrieve(BullhornAuthCredentials bullhornAuthCredentials)
    {
        var code = await _code.Fetch(bullhornAuthCredentials.ClientId, bullhornAuthCredentials.Username, bullhornAuthCredentials.Password);
        var authorizationCode = await _authorizationCode.Fetch(code, bullhornAuthCredentials.ClientId, bullhornAuthCredentials.ClientSecret);
        var restToken = await _restToken.Fetch(authorizationCode.AccessToken);

        return restToken;
    }
}