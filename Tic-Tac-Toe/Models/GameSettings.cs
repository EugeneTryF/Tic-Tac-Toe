using System.Text.Json.Serialization;

namespace Tic_Tac_Toe.Models;

public class GameSettings
{
    public string GameMode { get; set; }
    public string Turn { get; set; }
}

[JsonSerializable(typeof(List<GameSettings>))]
public sealed partial class GameSettingsContext : JsonSerializerContext
{
}
