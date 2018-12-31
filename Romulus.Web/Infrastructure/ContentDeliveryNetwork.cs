namespace Romulus.Web.Infrastructure
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            public const string Domain = "ajax.googleapis.com";
            public const string JQuery2Url = "https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js";
            public const string JQuery3Url = "https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js";
            public const string SwfObjectUrl = "https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js";
        }

        public static class BootstrapCdn
        {
            public const string Domain = "stackpath.bootstrapcdn.com";

            public const string BootstrapCssUrl =
              "https://stackpath.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css";

            public const string BootstrapJsUrl = "https://stackpath.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js";

            public const string FontAwesomeUrl =
              "https://use.fontawesome.com/releases/v5.6.3/css/all.css";
        }

        public static class Cloudflare
        {
            public const string Domain = "cdnjs.cloudflare.com";


            public const string MaterializeCssUrl =
              "https://cdnjs.cloudflare.com/ajax/libs/materialize/0.100.2/css/materialize.min.css";

            public const string MaterializeJsUrl =
              "https://cdnjs.cloudflare.com/ajax/libs/materialize/0.100.2/js/materialize.min.js";

            public const string ModernizrUrl = "https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";

            public const string ParsleyUrl = "https://cdnjs.cloudflare.com/ajax/libs/parsley.js/2.8.1/parsley.min.js";

            public const string Migrate1Url =
              "https://cdnjs.cloudflare.com/ajax/libs/jquery-migrate/1.4.1/jquery-migrate.js";

            public const string Migrate3Url =
              "https://cdnjs.cloudflare.com/ajax/libs/jquery-migrate/3.0.1/jquery-migrate.js";
        }

        public static class GetMdl
        {
            public const string MdlIconsUrl = "https://fonts.googleapis.com/icon?family=Material+Icons";
            public const string MdlCssUrl = "https://code.getmdl.io/1.3.0/material.indigo-pink.min.css";
            public const string MdlJsUrl = "https://code.getmdl.io/1.3.0/material.min.js";
        }
    }
}
