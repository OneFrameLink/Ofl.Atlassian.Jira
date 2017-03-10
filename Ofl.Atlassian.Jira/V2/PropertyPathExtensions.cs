using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Ofl.Core;
using System.Reflection;

namespace Ofl.Atlassian.Jira.V2
{
    public static class PropertyPathExtensions
    {
        public static string ToParameterValue(this IEnumerable<PropertyPath> propertyPaths)
        {
            // Validate parameters.
            if (propertyPaths == null) throw new ArgumentNullException(nameof(propertyPaths));

            // Create an return.
            return propertyPaths.Select(
                // Get the components of the path.
                e => e.Path.Select(
                    // If there's a JsonProperty, use the name.
                    pi => pi.GetCustomAttribute<JsonPropertyAttribute>(true)?.PropertyName ??
                          // Otherwise, camel case the property.
                          pi.Name.ToCamelCase()
                    ).ToDelimitedString(".")
                ).ToDelimitedString(",");
        }
    }
}
