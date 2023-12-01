using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Utility.Extensions;

public static class MemberExpressionExtensions
{
    public static string GetBullhornFieldName<T, T1>(this Expression<Func<T, T1>> expression)
    {
        var memberExpression = (MemberExpression)expression?.Body;
        return memberExpression?.Member?.GetCustomAttribute<JsonPropertyAttribute>()
            ?.PropertyName;
    }
}