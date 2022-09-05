using System.Text.Json.Serialization;

namespace webapi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RgbClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3

    }
}