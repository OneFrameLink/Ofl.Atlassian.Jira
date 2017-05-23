using System;

namespace Ofl.Atlassian.Jira.V2
{
    public static class JiraClientExtensions
    {
        #region Read-ony state.

        private const string Version = "2";

        private static readonly string ApiAbsolutePathBase = $"/rest/api/{ Version }/";

        #endregion

        #region Extensions.

        internal static string CreateApiUri(this JiraClientConfiguration configuration, string path)
        {
            // Validate parameters.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            // Construct the URL. Get the base first.
            string url = configuration.BaseUrl.ToString();

            // Combine.
            url = CombineUrls(url, ApiAbsolutePathBase);

            // Combine with the path passed in.
            return CombineUrls(url, path);
        }

        // TODO: Need to find a URL manipulation library, or have one of our own in Ofl.Core.
        private static string CombineUrls(string url1, string url2)
        {
            // Format parameters, trim the ends.
            url1 = url1?.TrimEnd('/') ?? "";
            url2 = url2?.TrimStart('/') ?? "";

            // If the right is empty, return the left.  Or the other way around.
            if (url1 == "") return url2;
            if (url2 == "") return url1;


            // Combine the two.
            return $"{ url1 }/{ url2 }";
        }

        #endregion
    }
}
