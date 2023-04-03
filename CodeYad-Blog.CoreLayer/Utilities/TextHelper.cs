using System.Text.RegularExpressions;

namespace CodeYad_Blog.CoreLayer.Utilities
{
    public static class TextHelper
    {
        public static string ToSlug(this string url)
        {
            return url.Trim().ToLower()
                .Replace(" ", "-")
                .Replace("$", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("?", "")
                .Replace("??", "")
                .Replace("???", "")
                .Replace("^", "")
                .Replace("*", "")
                .Replace("@", "")
                .Replace("!", "")
                .Replace("#", "")
                .Replace("&", "")
                .Replace("~", "")
                .Replace("(", "")
                .Replace("=", "")
                .Replace(")", "")
                .Replace("/", "")
                .Replace(@"\", "")
                .Replace(".", "")
                .Replace("،", "")
                .Replace(",", "")
                .Replace("--", "-")
                .Replace("---", "-");
        }

        public static bool IsUniCode(this string value)
        {
            return value.Any(c => c > 255);
        }

        public static string ConvertHtmlToText(this string text)
        {
            return Regex.Replace(text, "<.*?>", " ")
                .Replace(":&nbsp;", " ");
        }
    }
}