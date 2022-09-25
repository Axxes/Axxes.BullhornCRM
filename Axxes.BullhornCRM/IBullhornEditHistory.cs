using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Models.CrmEntities;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhornEditHistory<in T>  where T : IBullhornEntityWithHistory, new()
{
    [Get("")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<BullhornRecords<EditHistory>> Get([Query] string where, [Query] int count = 200, [Query] string fields = "*", [Query] string orderBy = "-dateAdded", CancellationToken cancellationToken = default);

    internal async Task<IEnumerable<EditHistory>> Get(string id, CancellationToken cancellationToken = default)
    {
        var result = await Get(
            $"targetEntity.id={id}",
            fields: "id,dateAdded,fieldChanges(id,columnName,oldValue,newValue)",
            cancellationToken: cancellationToken
        );

        return result.Data;
    }

    Task<IEnumerable<EditHistory>> Get(long id, CancellationToken cancellationToken = default) =>
        Get(id.ToString(), cancellationToken);
    
    Task<IEnumerable<EditHistory>> Get(T entity, CancellationToken cancellationToken = default) =>
        Get(
            entity.Id.ToString(),
            cancellationToken
        );
}