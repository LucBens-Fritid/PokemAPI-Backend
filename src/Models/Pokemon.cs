namespace pokemAPI.Models;
using System.Text.Json.Serialization;

public class Pokemon {
    [JsonPropertyName("id")]
  public int Id { get; set; }

    [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("sprites")]
    public Sprites Sprites { get; set; }
}

public class Sprites 
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; }
}