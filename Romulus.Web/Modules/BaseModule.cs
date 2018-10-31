namespace Romulus.Web.Modules
{
    using System.Dynamic;
    using Nancy;
    using Romulus.Web.ViewModels;

    public class BaseModule : NancyModule
    {
        public dynamic Model = new ExpandoObject();

        protected PageModel Page { get; set; }

        public BaseModule()
        {
            SetupModelDefaults();
        }

        public BaseModule(string modulepath)
            : base(modulepath)
        {
            SetupModelDefaults();
        }

        private void SetupModelDefaults()
        {
            Before += ctx =>
            {
                Page = new PageModel()
                {
                    PreFixTitle = "Madhon's Site - ",
                };

                Model.Page = Page;

                return null;
            };
        }
    }
}
