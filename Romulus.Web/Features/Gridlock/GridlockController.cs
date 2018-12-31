namespace Romulus.Web.Features.Gridlock
{
    using Microsoft.AspNetCore.Mvc;

    public class GridlockController : Controller
    {
        public IActionResult Index() => View();
    }
}