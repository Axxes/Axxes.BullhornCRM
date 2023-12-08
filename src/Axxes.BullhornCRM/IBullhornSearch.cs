using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Models.CrmEntities;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhornSearch<T> where T : IBullhornEntity
{
    [Post("")]
    internal Task<BullhornRecords<T>> FindInternal([Body] BullhornFind query, [Query] string fields = "*", [Query] int count = 500, [Query] int start = 0,
        CancellationToken cancellationToken = default);

    public async Task<BullhornRecords<T>> FindAll(Dictionary<string, string> properties, string fields = "*", int count = 500, int start = 0,
        CancellationToken cancellationToken = default)
    {
        var body = new BullhornFind(properties);

        return await FindInternal(body, fields, count, start, cancellationToken);
    }

    public async Task<T> Find(Dictionary<string, string> properties, string fields = "*", int count = 500, int start = 0, 
        CancellationToken cancellationToken = default)
    {
        var result = await FindAll(properties, fields, count, start, cancellationToken);
        return result.Data.FirstOrDefault();
    }
}