using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using TemperatureChartDrawer.Controllers;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.src
{
    public class GenericFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            
            var allTestCaseV2 = currentAssembly
                .GetTypes()
                .Where(value => value
                .IsClass && !value
                .IsAbstract && value
                .IsSubclassOf(typeof(SourceBase)));
            foreach (var candidate in allTestCaseV2)
            {
                feature.Controllers.Add(
                    typeof(SourceController<>).MakeGenericType(candidate).GetTypeInfo()
                );
                feature.Controllers.Add(
                    typeof(RecordController<>).MakeGenericType(candidate).GetTypeInfo()
                );
            }
        }
    }
}
