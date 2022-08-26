namespace Romulus.Web.Infrastructure
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            public const string Domain = "ajax.googleapis.com";
            public const string JQuery3Url = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js";
        }

        public static class JsDelivrCdn
        {
            public const string Domain = "cdn.jsdelivr.net";

            public const string BootstrapCssUrl =
			  "https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css";

            public const string BootstrapJsUrl = "https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.min.js";
        }

        public static class Cloudflare
        {
            public const string Domain = "cdnjs.cloudflare.com";

            public const string ModernizrUrl = "https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";

            public const string ParsleyUrl = "https://cdnjs.cloudflare.com/ajax/libs/parsley.js/2.9.2/parsley.min.js";

            public const string Migrate3Url =
              "https://cdnjs.cloudflare.com/ajax/libs/jquery-migrate/3.1.0/jquery-migrate.js";
        }

        public static class UnPkg
        {
	        public const string Domain = "unpkg.com";
	        public const string Popper = "https://unpkg.com/@popperjs/core@2.11.6/dist/umd/popper.min.js";
        }
    }
}
