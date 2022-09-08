using System.Text.RegularExpressions;

namespace TubeCube.Framework.Extensions;

internal static class StringExtensions
{
    public static string CleanCacheKey(this string uri) => 
        Regex.Replace((new Regex("[\\~#%&*{}/:<>?|\"-]")).Replace(uri, " "), @"\s+", "_");
}
