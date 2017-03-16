namespace Romulus.Web.Constants
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            public const string Domain = "ajax.googleapis.com";
            public const string JQuery2Url = "https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js";
            public const string JQuery3Url = "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js";
            public const string SwfObjectUrl = "https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js";
        }

        public static class MaxCdn
        {
            public const string Domain = "maxcdn.bootstrapcdn.com";

            public const string BootstrapCssUrl =
                "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";

            public const string BootstrapJsUrl = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";

            public const string FontAwesomeUrl =
                "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css";
        }

        public static class JsDelivr
        {
            public const string Domain = "cdn.jsdelivr.net";

            public const string MaterializeCssUrl =
                "https://cdn.jsdelivr.net/materialize/0.98.0/css/materialize.min.css";

            public const string MaterializeJsUrl =
                "https://cdn.jsdelivr.net/materialize/0.98.0/js/materialize.min.js";

            public const string ModernizrUrl = "https://cdn.jsdelivr.net/modernizr/2.8.3/modernizr.min.js";
            public const string ParsleyUrl = "https://cdn.jsdelivr.net/parsleyjs/2.6.5/parsley.min.js";
        }

        public static class Cloudflare
        {
            public const string Domain = "cdnjs.cloudflare.com";

            public const string Migrate1Url =
                "https://cdnjs.cloudflare.com/ajax/libs/jquery-migrate/1.4.1/jquery-migrate.js";

            public const string Migrate3Url =
                "https://cdnjs.cloudflare.com/ajax/libs/jquery-migrate/3.0.0/jquery-migrate.js";
        }

        public static class GetMdl
        {
            public const string MdlIconsUrl = "https://fonts.googleapis.com/icon?family=Material+Icons";
            public const string MdlCssUrl = "https://code.getmdl.io/1.3.0/material.indigo-pink.min.css";
            public const string MdlJsUrl = "https://code.getmdl.io/1.3.0/material.min.js";
        }
    }
}