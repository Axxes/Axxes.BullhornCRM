using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Axxes.BullhornCRM.Converters;

public class MillisecondEpochConverter : DateTimeConverterBase
{
    private static readonly DateTimeOffset Epoch = new(1970, 1, 1, 0, 0, 0, TimeSpan.FromHours(-6));

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var dto = (DateTimeOffset)value;
        dto = new DateTimeOffset(dto.Year, dto.Month, dto.Day, 12, 0, 0, dto.Offset);
        writer.WriteRawValue((dto - Epoch).TotalMilliseconds.ToString((IFormatProvider)CultureInfo.InvariantCulture));
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        return reader.Value == null ? null : Epoch.AddMilliseconds((long) reader.Value).DateTime;
    }
}