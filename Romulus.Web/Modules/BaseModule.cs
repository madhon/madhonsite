namespace Romulus.Web.Modules
{
    using Nancy;
    using NewRelicAgent = NewRelic.Api.Agent.NewRelic;

    public abstract class BaseModule : NancyModule
    {
        public BaseModule()
        {
            Before += ctx =>
            {
                var routeDescription = ctx.ResolvedRoute.Description;
                NewRelicAgent.SetTransactionName("Custom", $"{routeDescription.Method} {routeDescription.Path}");

                return null;
            };
        }
    }
}