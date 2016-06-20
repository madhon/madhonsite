namespace Romulus.Web.Constants
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            public const string Domain = "ajax.googleapis.com";
            public const string JQuery1Url = "https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js";
            public const string JQuery2Url = "https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js";
            public const string SwfObjectUrl = "https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js";
        }

        public static class MaxCdn
        {
            public const string Domain = "maxcdn.bootstrapcdn.com";
            public const string BootstrapCssUrl = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css";
            public const string BootstrapJsUrl = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js";
            public const string FontAwesomeUrl = "https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css";
        }

        public static class JsDelivr
        {
            public const string Domain = "cdn.jsdelivr.net";
            public const string ModernizrUrl = "https://cdn.jsdelivr.net/modernizr/2.8.3/modernizr.min.js";
            public const string RespondUrl = "https://cdn.jsdelivr.net/respond/1.4.2/respond.min.js";
            public const string HtmlShivUrl = "https://cdn.jsdelivr.net/html5shiv/3.7.3/html5shiv-printshiv.min.js";
            public const string ParsleyUrl = "https://cdn.jsdelivr.net/parsleyjs/2.4.3/parsley.min.js";
        }

        public static class Cloudflare
        {
            public const string Domain = "cdnjs.cloudflare.com";
            public const string MaterializeCssUrl =
                "https://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.6/css/materialize.min.css";
            public const string MaterializeJsUrl =
                "https://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.6/js/materialize.min.js";
        }

        public static class GetMdl
        {
            public const string MdlIconsUrl = "https://fonts.googleapis.com/icon?family=Material+Icons";
            public const string MdlCssUrl = "https://code.getmdl.io/1.1.3/material.indigo-pink.min.css";
            public const string MdlJsUrl = "https://code.getmdl.io/1.1.3/material.min.js";
        }
    }
}
