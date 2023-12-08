using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Models.CrmEntities;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhornSearch<T> where T : IBullhornEntity
{
    [Post("")]
    internal Task<BullhornRecords<T>> FindInternal([Body] BullhornFind query, [Query] string fields = "*",
        CancellationToken cancellationToken = default);

    public async Task<BullhornRecords<T>> FindAll(Dictionary<string, string> properties, [Query] string fields = "*",
        CancellationToken cancellationToken = default)
    {
        var body = new BullhornFind(properties);

        return await FindInternal(body, fields, cancellationToken);
    }

    public async Task<T> Find(Dictionary<string, string> properties, [Query] string fields = "*",
        CancellationToken cancellationToken = default)
    {
        var result = await FindAll(properties, fields, cancellationToken);
        return result.Data.FirstOrDefault();
    }
}