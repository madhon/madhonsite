namespace Romulus.Web
{
  using System;
  using JetBrains.Annotations;
  using Microsoft.Owin.Extensions;
  using Owin;

  [UsedImplicitly]
  public class Startup
  {
    [UsedImplicitly]
    public void Configuration(IAppBuilder app)
    {
      app.UseNancy();
      app.UseStageMarker(PipelineStage.MapHandler);
    }
  }
}
