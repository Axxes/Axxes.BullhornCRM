using System.Linq.Expressions;
using System.Reflection;
using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Models.CrmEntities;
using Axxes.BullhornCRM.Utility.Extensions;
using Newtonsoft.Json;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhornQuery<T> where T : IBullhornEntity
{
    [Post("")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<BullhornRecords<T>> QueryInternal([Body] BullhornQuery bullhornQuery, [Query] string fields = "*",
        [Query] string orderBy = null, [Query] int count = 500, [Query] int start = 0,
        CancellationToken cancellationToken = default);

    public async Task<BullhornRecords<T>> Query<T1>(string where, string fields = "*",
        Expression<Func<T, T1>> orderBySelector = null, int count = 500, int start = 0,
        CancellationToken cancellationToken = default)
    {
        var orderbyField = orderBySelector.GetBullhornFieldName();
        var queryBody = new BullhornQuery() { Where = where };
        return await QueryInternal(queryBody, fields, orderbyField, count,
            start, cancellationToken);
    }

    public async Task<BullhornRecords<T>> QueryEquals<T1, T2, T3>(Expression<Func<T, T2>> fieldSelector, T3 value,
        string fields = "*",
        Expression<Func<T, T1>> orderBySelector = null, int count = 500, int start = 0,
        CancellationToken cancellationToken = default)
    {
        var name = fieldSelector.GetBullhornFieldName();
        var query = $"{name}={GetStringValue(value)}";
        return await Query(query, fields, orderBySelector, count, start, cancellationToken);
    }

    public async Task<BullhornRecords<T>> QueryIn<T1, T2, T3>(Expression<Func<T, T2>> fieldSelector, IEnumerable<T3> values,
        string fields = "*",
        Expression<Func<T, T1>> orderBySelector = null, int count = 500, int start = 0,
        CancellationToken cancellationToken = default)
    {
        var name = fieldSelector.GetBullhornFieldName();
        var query = $"{name} IN ({string.Join(", ", values.Select(GetStringValue))})";
        return await Query(query, fields, orderBySelector, count, start, cancellationToken);
    }

    private static string GetStringValue<T1>(T1 value)
    {
        if (value is string stringValue)
        {
            return $"'{value}'";
        }
        else
        {
            return value.ToString();
        }
    }
}