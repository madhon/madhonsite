namespace Romulus.Web.Modules
{
    using System;
    using JetBrains.Annotations;
    using Nancy;

    [UsedImplicitly]
    public class BotModule : NancyModule
    {
        private static readonly string[] TenHoursOfFun =
        {
            "https://www.youtube.com/watch?v=wbby9coDRCk",
            "https://www.youtube.com/watch?v=nb2evY0kmpQ",
            "https://www.youtube.com/watch?v=eh7lp9umG2I",
            "https://www.youtube.com/watch?v=z9Uz1icjwrM",
            "https://www.youtube.com/watch?v=Sagg08DrO5U",
            "https://www.youtube.com/watch?v=09m0B8RRiEE",
            "https://www.youtube.com/watch?v=jI-kpVh6e1U",
            "https://www.youtube.com/watch?v=jScuYd3_xdQ",
            "https://www.youtube.com/watch?v=S5PvBzDlZGs",
            "https://www.youtube.com/watch?v=9UZbGgXvCCA",
            "https://www.youtube.com/watch?v=O-dNDXUt1fg",
            "https://www.youtube.com/watch?v=MJ5JEhDy8nE",
            "https://www.youtube.com/watch?v=VnnWp_akOrE",
            "https://www.youtube.com/watch?v=jwGfwbsF4c4",
            "https://www.youtube.com/watch?v=8ZcmTl_1ER8",
            "https://www.youtube.com/watch?v=q44qwyHlMfk",
            "https://www.youtube.com/watch?v=ozPPwl53c_4",
            "https://www.youtube.com/watch?v=KMFOVSWn0mI",
            "https://www.youtube.com/watch?v=clU0Sh9ngmY",
            "https://www.youtube.com/watch?v=sCNrK-n68CM"
        };

        public BotModule()
        {
            Get["/admin.php"] = _ => GetHoursOfFun();
            Get["/admin/login.php"] = _ => GetHoursOfFun();
            Get["/administrator/index.php"] = _ => GetHoursOfFun();
            Get["/ajaxproxy/proxy.php"] = _ => GetHoursOfFun();
            Get["/bitrix/admin/index.php"] = _ => GetHoursOfFun();
            Get["/index.php"] = _ => GetHoursOfFun();
            Get["/magmi/web/magmi.php"] = _ => GetHoursOfFun();
            Get["/wp-admin/admin-ajax.php"] = _ => GetHoursOfFun();
            Get["/wp-admin/includes/themes.php"] = _ => GetHoursOfFun();
            Get["/wp-admin/options-link.php"] = _ => GetHoursOfFun();
            Get["/wp-admin/post-new.php"] = _ => GetHoursOfFun();
            Get["/wp-login.php"] = _ => GetHoursOfFun();
            Get["/xmlrpc.php"] = _ => GetHoursOfFun();
        }

        private readonly Random rnd = new Random();

        private dynamic GetHoursOfFun()
            => Response.AsRedirect(TenHoursOfFun[rnd.Next(0, TenHoursOfFun.Length)]);
    }
}