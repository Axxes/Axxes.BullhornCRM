using System.Net;
using Axxes.BullhornCRM.Models.CrmEntities;
using Axxes.BullhornCRM.Utility;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhornSubscriptions
{
    [Get("/event/subscription/{id}?maxEvents=100000")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<SubscriptionEvent> GetInternal(string id, CancellationToken cancellationToken = default);

    async Task<IEnumerable<SubscriptionEvent.Event>> Get(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await GetInternal(id, cancellationToken: cancellationToken);
            return result.Events;
        }
        catch (ApiException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            await PutInternal(id, "Candidate", "UPDATED", cancellationToken);
            return Enumerable.Empty<SubscriptionEvent.Event>();
        }
    }

    [Put("/event/subscription/{id}?type=entity&names={names}&eventTypes={eventTypes}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<SubscriptionEvent> PutInternal(string id, string names, string eventTypes, CancellationToken cancellationToken = default);
}