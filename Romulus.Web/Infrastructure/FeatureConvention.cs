namespace Romulus.Web.Infrastructure;

using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

internal sealed class FeatureConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.Properties.Add("feature",
            GetFeatureName(controller.ControllerType));
    }
    private static string GetFeatureName(TypeInfo controllerType)
    {
        string[] tokens = controllerType.FullName!.Split('.');

        if (tokens.All(t => !t.Equals("Features", StringComparison.OrdinalIgnoreCase)))
        {
            return "";
        }

        string? featureName = tokens
            .SkipWhile(t => !t.Equals("features",
                StringComparison.OrdinalIgnoreCase))
            .Skip(1)
            .Take(1)
            .FirstOrDefault();

        return string.IsNullOrEmpty(featureName) ? string.Empty : featureName;
    }
}
