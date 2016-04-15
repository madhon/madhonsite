namespace Romulus.Web.Helpers
{
  using JetBrains.Annotations;
  using Nancy.ViewEngines.Razor;

  public static class HtmlHelperExtensions
  {
    public static IHtmlString CssTag<T>([UsedImplicitly] this  HtmlHelpers<T> helper, string uri) => new NonEncodedHtmlString(
        $"<link href=\"{uri}\" rel=\"stylesheet\" />");

      public static IHtmlString JsTag<T>([UsedImplicitly] this HtmlHelpers<T> helper, string uri, bool async = false) => new NonEncodedHtmlString(string.Format("<script {1}type=\"text/javascript\" src=\"{0}\"></script>  ", uri, async ? "async " : string.Empty));
  }
}