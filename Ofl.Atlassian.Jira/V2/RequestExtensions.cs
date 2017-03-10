using System;
using Microsoft.AspNetCore.WebUtilities;
using Ofl.Core;

namespace Ofl.Atlassian.Jira.V2
{
    public static class RequestExtensions
    {
        public static string AddExpandToUrl(this Request request, string url)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            // Call the overload, camel case the property name.
            return request.AddExpandToUrl(url, nameof(request.Expand).ToCamelCase());
        }

        public static string AddExpandToUrl(this Request request, string url, string key)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            // Convert, add to query helper, return.
            return QueryHelpers.AddQueryString(url, key, request.Expand.ToParameterValue());
        }
    }
}
