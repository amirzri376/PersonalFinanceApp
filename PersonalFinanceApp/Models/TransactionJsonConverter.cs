using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalFinanceApp.Models
{
    public class TransactionJsonConverter : JsonConverter<Transaction>
    {
        public override Transaction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (!root.TryGetProperty("Type", out var typeProp))
                throw new JsonException("Missing Type property.");

            var type = typeProp.GetString();

            return type switch
            {
                "Income" => JsonSerializer.Deserialize<Income>(root.GetRawText(), options),
                "Expense" => JsonSerializer.Deserialize<Expense>(root.GetRawText(), options),
                _ => throw new JsonException($"Unknown Type: {type}")
            };
        }

        public override void Write(Utf8JsonWriter writer, Transaction value, JsonSerializerOptions options)
        {
            if (value is Income income)
                JsonSerializer.Serialize(writer, income, options);
            else if (value is Expense expense)
                JsonSerializer.Serialize(writer, expense, options);
            else
                throw new NotSupportedException($"Unknown transaction type: {value.GetType()}");
        }
    }
}
