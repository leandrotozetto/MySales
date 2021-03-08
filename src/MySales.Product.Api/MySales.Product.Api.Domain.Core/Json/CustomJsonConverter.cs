using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MySales.Product.Api.Domain.Core.Json
{
    public class CustomJsonConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader,
                                      Type typeToConvert,
                                      JsonSerializerOptions options)
        {
            var entity = Activator.CreateInstance(typeof(T), true);
            var name = string.Empty;

            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                name = jsonDoc.RootElement.GetRawText();

                var source = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(name);
                var entityType = entity.GetType();
                var entityProps = entityType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var s in source.Keys)
                {
                    var entityProp = entityProps.FirstOrDefault(x => x.Name.ToUpper() == s.ToUpper());

                    if (entityProp != null)
                    {
                        var value = JsonSerializer.Deserialize(source[s].GetRawText(), entityProp.PropertyType);

                        entityType.InvokeMember(entityProp.Name,
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                            null,
                            entity,
                            new object[] { value });
                    }
                }
            }

            return (T)entity;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var props = value.GetType()
                             .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .ToDictionary(x => x.Name, x => x.GetValue(value));

            var ser = JsonSerializer.Serialize(props);

            writer.WriteStringValue(ser);
        }
    }
}
