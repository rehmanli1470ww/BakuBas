using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BingMapLesson.Models;

public class BakuBus
{
    // [JsonPropertyName("BUS")]
    public List<AftoBus> BUS { get; set; }
}
