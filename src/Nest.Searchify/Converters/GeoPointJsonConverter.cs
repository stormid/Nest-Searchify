using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Searchify.Converters
{
    public class GeoPointJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var geoPointJObject = JObject.Load(reader);
                var lat = geoPointJObject.Value<double>("lat");
                var lon = geoPointJObject.Value<double>("lon");
                var point = GeoPoint.TryCreate(lat, lon);
                if (objectType == typeof(string)) return point.ToString();
                return point;
            }
            if (reader.TokenType == JsonToken.String)
            {
                GeoPoint point = (string)reader.Value;
                return point;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GeoPoint) || objectType == typeof(string);
        }
    }
}