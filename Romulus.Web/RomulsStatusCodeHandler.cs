namespace Romulus.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Nancy;
    using Nancy.ErrorHandling;
    using Nancy.ViewEngines;

    [UsedImplicitly]
    public class RomulsStatusCodeHandler : IStatusCodeHandler
    {
        private readonly IViewRenderer viewRenderer;

        static RomulsStatusCodeHandler()
        {
            Checks = new List<int>();
        }

        public RomulsStatusCodeHandler(IViewRenderer viewRenderer)
        {
            this.viewRenderer = viewRenderer;
        }

        private static IEnumerable<int> Checks { get; set; }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
            => Checks.Any(x => x == (int) statusCode);

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            try
            {
                var response = viewRenderer.RenderView(context, "Codes/" + (int) statusCode + ".cshtml");
                response.StatusCode = statusCode;
                context.Response = response;
            }
            catch (Exception)
            {
                RemoveCode((int) statusCode);
                context.Response.StatusCode = statusCode;
            }
        }

        public static void AddCode(int code) => AddCode(new List<int> { code });

        public static void AddCode(IEnumerable<int> code) => Checks = Checks.Union(code);

        public static void RemoveCode(int code) => RemoveCode(new List<int> { code });

        public static void RemoveCode(IEnumerable<int> code) => Checks = Checks.Except(code);

        public static void Disable() => Checks = new List<int>();
    }
}