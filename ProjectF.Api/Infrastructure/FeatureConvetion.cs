using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ProjectF.Api.Infrastructure
{
    /// <summary>
    /// This add Feature folder structure to the project
    /// </summary>
  class FeatureConvention : IControllerModelConvention
  {
    public void Apply(ControllerModel controller)
    {
      controller.Properties.Add("features", GetFeatureName(controller.ControllerType));
    }

    private object GetFeatureName(TypeInfo controllerType)
    {
        var tokens = controllerType?.FullName?.Split('.');
            if (tokens.All(t => t != "Features"))
                return "";
            var featureName = tokens
            .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
            .Skip(1)
            .Take(1)
            .FirstOrDefault();

            return featureName;
    }
  }

   public class FeatureFoldersRazorViewEngine : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
              IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                throw new NullReferenceException("ControllerActionDescriptor cannot be null.");
            }

            string featureName = controllerActionDescriptor.Properties["feature"] as string ?? string.Empty;
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{3}", featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}
