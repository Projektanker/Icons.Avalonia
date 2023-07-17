using System.Collections.Generic;
using System.Text.Json.Serialization;
using Projektanker.Icons.Avalonia.FontAwesome.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(Dictionary<string, FontAwesomeIcon>))]
    internal partial class FontAwesomeIconsJsonContext : JsonSerializerContext
    {
    }
}
