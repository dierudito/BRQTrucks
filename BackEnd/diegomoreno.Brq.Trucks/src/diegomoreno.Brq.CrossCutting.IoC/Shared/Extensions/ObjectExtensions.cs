using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace diegomoreno.Brq.CrossCutting.IoC.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class ObjectExtensions
{
    private static readonly JsonSerializerSettings DefaultSerializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    /// <summary>
    /// Deserializes a JSON string into an object.
    /// </summary>
    public static T FromJsonTo<T>(this string? s, JsonSerializerSettings? settings = null)
        where T : class, new() => JsonConvert.DeserializeObject<T>(s ?? "", settings ?? DefaultSerializerSettings) ?? new T();
}