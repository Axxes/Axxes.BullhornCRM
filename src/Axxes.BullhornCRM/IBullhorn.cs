using System.Linq.Expressions;
using System.Reflection;
using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Models.CrmEntities;
using Newtonsoft.Json;
using Refit;

namespace Axxes.BullhornCRM;

public interface IBullhorn<T>  where T : IBullhornEntity
{
    #region Get

    [Get("/{id}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<BullhornRecord<T>> GetInternal(long id, [Query] string fields = "*", CancellationToken cancellationToken = default);

    async Task<T> Get(int id, string fields = "*", CancellationToken cancellationToken = default)
    {
        var result = await GetInternal(id, fields, cancellationToken);
        return result.Data;
    }
    
    public Task<T> Get(int id, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] ignoredMembers) => Get(id, Fields(ignoredMembers), cancellationToken);
    
    [Get("/{ids}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<BullhornRecords<T>> Get(string ids, [Query] string fields = "*", CancellationToken cancellationToken = default);

    public Task<BullhornRecords<T>> Get(IEnumerable<int> ids, string fields = "*", CancellationToken cancellationToken = default) =>
        Get(ids.Select(x => (long) x), fields, cancellationToken);

    public Task<BullhornRecords<T>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] ignoredMembers) => Get(ids, Fields(ignoredMembers), cancellationToken);

    public async Task<BullhornRecords<T>> Get(IEnumerable<long> ids, string fields = "*", CancellationToken cancellationToken = default)
    {
        var idsArray = ids.Distinct().ToArray();

        if (idsArray.Length == 1)
        {
            var result = await GetInternal(idsArray[0], fields, cancellationToken);
            return new BullhornRecords<T>
            {
                Total = 1,
                Data = new []{ result.Data }
            };
        }
        
        var idsConcatenated = string.Join(",", idsArray);
        return await Get(idsConcatenated, fields, cancellationToken);
    }

    private static string Fields(Expression<Func<T, object>>[] ignoredMembers)
    {
        if (ignoredMembers == null) throw new ArgumentNullException(nameof(ignoredMembers));

        var type = typeof(T);
        
        var ignoredProperties = ignoredMembers
            .Select(expression =>
            {
                if (expression.Body is not MemberExpression body) {
                    body = (((UnaryExpression)expression.Body).Operand as MemberExpression)!;
                }
                    
                var propertyName = body switch
                {
                    { NodeType: ExpressionType.MemberAccess } x => x.Member.Name,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(propertyName)) return null;

                var property = type.GetProperty(propertyName);
                return property
                    ?.GetCustomAttributes<JsonPropertyAttribute>()
                    .FirstOrDefault()
                    ?.PropertyName;
            }).ToHashSet();
        var fields = string.Join(",", ignoredProperties);
        return fields;
    }
    

    #endregion

    #region Add

    [Put("/")]
    internal Task<CrmChangeResponse> AddInternal([Body] T entity, CancellationToken cancellationToken);

    public async Task<long> Add([Body] T entity, CancellationToken cancellationToken = default)
    {
        var response = await AddInternal(entity, cancellationToken);
        return response.ChangedEntityId;
    }

    #endregion
    
    #region Update
    [Post("/{entity.id}")]
    Task Update([Body] T entity, CancellationToken cancellationToken = default);

    [Post("/{entity.id}")]
    Task Update<T1>([Body] T1 entity, CancellationToken cancellationToken = default);
    #endregion
    
    #region CreateToManyAssociation

    [Put("/{entityId}/{associationName}/{joinedAssociationIds}")]
    internal Task CreateToManyAssociationsInternal([Body] object body, long entityId, string associationName, string joinedAssociationIds, CancellationToken cancellationToken);

    public async Task CreateToManyAssociations<T1>( long entityId, T1[] associations, CancellationToken cancellationToken) where T1: IBullhornToManyAssociationEntity
    {
        var associationName = typeof(T1).GetCustomAttribute<EntityAssociationAttribute>(true)?.Name;
        if (string.IsNullOrEmpty(associationName))
        {
            throw new ArgumentNullException($"Entity {typeof(T1)} has no {nameof(EntityAssociationAttribute)}");
        }
        
        await CreateToManyAssociationsInternal(new object(), entityId, associationName.ToLowerInvariant(),
            string.Join(',', associations.Select( x => x.Id)), cancellationToken);
    }

    #endregion
}