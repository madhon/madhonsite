namespace Romulus.Web.Features.WellKown;

public class WebFingerController : Controller
{



    [HttpGet()]
    [AllowAnonymous()]
    [Route(".well-known/webfinger")]
    public IActionResult Index()
    {
        var webfingerContent = @"{
    ""aliases"": [
        ""https://hachyderm.io/@Madhon"",
        ""https://hachyderm.io/users/Madhon""
    ],
    ""links"": [
        {
            ""href"": ""https://hachyderm.io/@Madhon"",
            ""rel"": ""http://webfinger.net/rel/profile-page"",
            ""type"": ""text/html""
        },
        {
            ""href"": ""https://hachyderm.io/users/Madhon"",
            ""rel"": ""self"",
            ""type"": ""application/activity+json""
        },
        {
            ""rel"": ""http://ostatus.org/schema/1.0/subscribe"",
            ""template"": ""https://hachyderm.io/authorize_interaction?uri={uri}""
        }
    ],
    ""subject"": ""acct:Madhon@hachyderm.io""
}";

        return Ok(webfingerContent);


    }
}
