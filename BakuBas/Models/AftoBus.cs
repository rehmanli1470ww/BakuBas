using System.Text.Json.Serialization;

namespace BingMapLesson.Models;

public  class AftoBus
{
    [JsonPropertyName("@attributes")]
    public Attributes attributes { get; set; }
}
