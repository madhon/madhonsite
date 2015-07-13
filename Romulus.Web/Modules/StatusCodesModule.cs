namespace Romulus.Web.Modules
{
  using JetBrains.Annotations;
  using Nancy;

    [UsedImplicitly]
    public class StatusCodesModule : NancyModule
    {
        public StatusCodesModule() : base("error")
        {
            Get["/add/{code}"] = x =>
            {
                RomulsStatusCodeHandler.AddCode(x.code);
                return "done";
            };

            Get["/remove/{code}"] = x =>
            {
                RomulsStatusCodeHandler.RemoveCode(x.code);
                return "done";
            };
        }
    }
}
