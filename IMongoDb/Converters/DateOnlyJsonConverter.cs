﻿using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        string isoDate = value.ToString("O");
        writer.WriteStringValue($"ISODate({isoDate})");
    }
}

public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string isoDate = value.ToString("O");
        writer.WriteStringValue($"ISODate({isoDate})");
    }
}
