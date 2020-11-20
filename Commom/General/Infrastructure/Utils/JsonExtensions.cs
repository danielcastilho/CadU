using System.Text.Json;

namespace CadU.Infrastructure.Utils
{
  public static class JsonExtensions
  {
    public static T FromJson<T>(this string json)
    {
      if (json == null || json.Length == 0)
        return default;

      return JsonSerializer.Deserialize<T>(json);
    }
    public static string ToJson<T>(this T obj)
    {
      if (obj == null)
      {
        return null;
      }
      var result = JsonSerializer.Serialize(obj,
        new JsonSerializerOptions()
        {
          DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
          WriteIndented = true,
          PropertyNameCaseInsensitive = false,
          PropertyNamingPolicy = null,
          DictionaryKeyPolicy = null
        });
      return result;

    }
  }
}