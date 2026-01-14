using Minesweeper.Core.Board;

namespace Minesweeper.Application.Persistence;
using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class BoardJsonConverter : JsonConverter<Board>
{
    public override Board Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var cells = root.GetProperty("Cells").Deserialize<Cell[][]>(options)!;

        var mineCells = cells
            .SelectMany(col => col)
            .Where(cell => cell.IsMine)
            .ToArray();

        return new Board
        {
            Cells = cells,
            MineCells = mineCells
        };
    }

    public override void Write(Utf8JsonWriter writer, Board value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("Cells");
        JsonSerializer.Serialize(writer, value.Cells, options);
        
        writer.WriteEndObject();
    }
}