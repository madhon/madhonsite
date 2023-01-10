namespace Romulus.Web.Features.BuildInfo;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppBuildInfo = Romulus.Web.BuildInfo;

[ApiController]
public class BuildInfoController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("api/build")]
    public AppBuildInfo GetBuild() => AppVersionInfo.GetBuildInfo();
}
