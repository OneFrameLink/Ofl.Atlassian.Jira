﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ofl.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ofl.Atlassian.Jira.V3
{
    public static class PropertyPathExtensions
    {
        public static string ToParameterValue(
            this IEnumerable<PropertyPath> propertyPaths
        )
        {
            // Validate parameters.
            if (propertyPaths == null) throw new ArgumentNullException(nameof(propertyPaths));

            // Create an return.
            return propertyPaths.Select(
                // Get the components of the path.
                e => e.Path.Select(
                    // If there's a JsonPropertyNameAttribute, use the name.
                    pi => pi.GetCustomAttribute<JsonPropertyNameAttribute>(true)
                        ?.Name
                        // Otherwise, camel case the property.
                        ?? JsonNamingPolicy.CamelCase.ConvertName(pi.Name)
                    ).Join(".")
                ).Join(",");
        }
    }
}
