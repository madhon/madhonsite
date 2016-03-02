namespace Romulus.Web.Constants
{
  using System;

  public static class ContentDeliveryNetwork
    {
      public static class Google
      {
        public const string Domain = "ajax.googleapis.com";
        public const string JQuery1Url = "//ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js";
        public const string JQuery2Url = "//ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js";
        public const string SwfObjectUrl = "//ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js";
      }

      public static class MaxCdn
      {
        public const string Domain = "maxcdn.bootstrapcdn.com";
        public const string BootstrapCssUrl = "//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css";
        public const string BootstrapJsUrl = "//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js";
        public const string FontAwesomeUrl = "//maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css";
      }

      public static class JsDelivr
      {
        public const string Domain = "cdn.jsdelivr.net";
        public const string ModernizrUrl = "//cdn.jsdelivr.net/modernizr/2.8.3/modernizr.min.js";
        public const string RespondUrl = "//cdn.jsdelivr.net/respond/1.4.2/respond.min.js";
        public const string HtmlShivUrl = "//cdn.jsdelivr.net/html5shiv/3.7.3/html5shiv-printshiv.min.js";
        public const string ParsleyUrl = "//cdn.jsdelivr.net/parsleyjs/2.3.5/parsley.min.js";
      }

      public static class Cloudflare
      {
        public const string Domain = "cdnjs.cloudflare.com";
        public const string MaterializeCssUrl =
          "//cdnjs.cloudflare.com/ajax/libs/materialize/0.97.5/css/materialize.min.css";
        public const string MaterializeJsUrl =
          "//cdnjs.cloudflare.com/ajax/libs/materialize/0.97.5/js/materialize.min.js";
      }
    }
}
