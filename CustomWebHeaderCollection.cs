using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

internal class CustomWebHeaderCollection : WebHeaderCollection
{
    private readonly Dictionary<string, string> _customHeaders;

    public CustomWebHeaderCollection(Dictionary<string, string> customHeaders)
    {
        _customHeaders = customHeaders;
    }

    public override string ToString()
    {
        return string.Join("\r\n", _customHeaders.Select(kvp => $"{kvp.Key}: {kvp.Value}").Concat(new[] { string.Empty, string.Empty }));
    }
}