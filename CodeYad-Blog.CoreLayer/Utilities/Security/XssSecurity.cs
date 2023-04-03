using Ganss.Xss;

namespace CodeYad_Blog.CoreLayer.Utilities.Security;

public static class XssSecurity
{
    public static string SanitizeText(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var htmlSanitizer = new HtmlSanitizer();

        htmlSanitizer.KeepChildNodes = true;

        htmlSanitizer.AllowDataAttributes = true;

        return htmlSanitizer.Sanitize(text);
    }
}