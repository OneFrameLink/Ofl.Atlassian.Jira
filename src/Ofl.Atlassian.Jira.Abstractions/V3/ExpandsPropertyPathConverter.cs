using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ofl.Reflection;

namespace Ofl.Atlassian.Jira.V3
{
    public class ExpandsPropertyPathConverter : JsonConverter<IEnumerable<PropertyPath>?>
    {
        #region Overrides of JsonConverter

        public override void Write(
            Utf8JsonWriter writer, 
            IEnumerable<PropertyPath>? value, 
            JsonSerializerOptions options
        )
        {
            // Validate parameters.
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Set to empty if null.
            value ??= Enumerable.Empty<PropertyPath>();

            // Get the enumerator.
            using IEnumerator<PropertyPath> enumerator = value.GetEnumerator();

            // Move next.  If there are no elements in it
            // then write null.
            if (!enumerator.MoveNext())
            {
                // Write null.
                writer.WriteNullValue();

                // Get out.
                return;
            }

            // Gets the property name.
            static string GetPropertyName(PropertyInfo propertyInfo) {
                // Get the attribute; if it exists, use that.
                JsonPropertyNameAttribute attribute = propertyInfo
                    .GetCustomAttribute<JsonPropertyNameAttribute>();

                // If there is an attribute, use the name.
                if (attribute != null)
                    return attribute.Name;

                // Camel case.
                return JsonNamingPolicy.CamelCase
                    .ConvertName(propertyInfo.Name);
            }

            // The string builder.
            var builder = new StringBuilder();

            // Cycle.
            do
            {
                // Add the property.
                builder = builder
                    .Append(enumerator.Current.Path.Select(GetPropertyName).Join("."))
                    .Append(',');
            } while (enumerator.MoveNext());

            // Remove the last comma.
            builder.Length--;

            // Write.
            writer.WriteStringValue(builder.ToString());
        }

        public override IEnumerable<PropertyPath> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            // Currently not needed.
            throw new NotImplementedException();

        public override bool CanConvert(Type objectType) =>
            typeof(IEnumerable<PropertyPath>)
            .GetTypeInfo()
            .IsAssignableFrom(objectType.GetTypeInfo());

        #endregion
    }
}
