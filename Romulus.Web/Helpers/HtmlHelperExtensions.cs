namespace Romulus.Web.Helpers
{
  using JetBrains.Annotations;
  using Nancy.ViewEngines.Razor;

  public static class HtmlHelperExtensions
  {
    public static IHtmlString CssTag<T>([UsedImplicitly] this  HtmlHelpers<T> helper, string uri)
    {
      return new NonEncodedHtmlString(string.Format("<link href=\"{0}\" rel=\"stylesheet\" />", uri));
    }

    public static IHtmlString JsTag<T>([UsedImplicitly] this HtmlHelpers<T> helper, string uri, bool async = false)
    {
      return new NonEncodedHtmlString(string.Format("<script {1}type=\"text/javascript\" src=\"{0}\"></script>  ", uri, async ? "async " : string.Empty));
    }
  }
}