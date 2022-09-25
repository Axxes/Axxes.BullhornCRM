namespace Axxes.BullhornCRM.Utility.Models;

public sealed class BullhornAuthCredentials
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string LoginEndpoint { get; set; }
    public string AuthorizeEndpoint { get; set; }
    public string TokenEndpoint { get; set; }

    public BullhornAuthCredentials()
    {
        LoginEndpoint = "https://rest.bullhornstaffing.com/rest-services/login";
        AuthorizeEndpoint = "https://auth.bullhornstaffing.com/oauth/authorize";
        TokenEndpoint = "https://auth.bullhornstaffing.com/oauth/token";
    }

    public BullhornAuthCredentials(string clientId, string clientSecret): this()
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }
}