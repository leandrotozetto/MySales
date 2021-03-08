using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MySales.Product.Api.Domain.Core.Json
{
    public class CustomSerializeOption : ICustomSerializeOption
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private CustomSerializeOption()
        {
            _jsonSerializerOptions ??= new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public static ICustomSerializeOption New()
        {
            return new CustomSerializeOption();
        }

        public ICustomSerializeOption AddConverter<T>(JsonConverter<T> jsonConverter)
        {
            _jsonSerializerOptions.Converters.Add(jsonConverter);

            return this;
        }

        public JsonSerializerOptions Option()
        {
            return _jsonSerializerOptions;
        }
    }
}
