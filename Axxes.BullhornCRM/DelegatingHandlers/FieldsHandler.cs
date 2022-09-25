using System.Reflection;
using System.Web;
using Axxes.BullhornCRM.Models;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.DelegatingHandlers;

internal class FieldsHandler<T> : DelegatingHandler where T: class, IBullhornEntity
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Method == HttpMethod.Get)
        {
            var uriBuilder = new UriBuilder(request.RequestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var fields = query["fields"];
            
            if (string.IsNullOrWhiteSpace(fields) || "*".Equals(fields, StringComparison.InvariantCultureIgnoreCase)) fields = Fields();
            query["fields"] = fields;
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
    
    private static string Fields() => string.Join(",", typeof(T).GetProperties()
        .Where(p => p.IsDefined(typeof(JsonPropertyAttribute), false))
        .Select(p => p.GetCustomAttribute(typeof(JsonPropertyAttribute)) as JsonPropertyAttribute)
        .Select(p => p?.PropertyName)
        .Where(p => p is not null));
}