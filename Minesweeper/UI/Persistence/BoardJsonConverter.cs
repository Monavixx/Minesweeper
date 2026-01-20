using System.Text.Json;
using System.Text.Json.Serialization;
using Minesweeper.Core.Board;

namespace Minesweeper.UI.Persistence;

public sealed class BoardJsonConverter : JsonConverter<Board>
{
    public override Board Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var cells = root.GetProperty("Cells").Deserialize<Cell[][]>(options)!;

        return new Board(cells);
    }

    public override void Write(Utf8JsonWriter writer, Board value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("Cells");
        JsonSerializer.Serialize(writer, value.Cells, options);
        
        writer.WriteEndObject();
    }
}