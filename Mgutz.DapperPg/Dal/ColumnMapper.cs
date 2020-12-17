using Dapper.FluentMap;
using Dapper.FluentMap.Conventions;
using System.Text.RegularExpressions;

namespace Mgutz.DapperPg.Dal {

    public static class ColumnMapper {
        public static void Initialize() {
            FluentMapper.Initialize(config => {
                config.AddConvention<PropertyTransformConvention>()
                      .ForEntitiesInCurrentAssembly("Mgutz.DapperPg.Models");
            });
        }
    }

    /// <summary>A transform to map C# pascal case identifiers to Postgres snake case.</summary>
    public class PropertyTransformConvention : Convention {
        public PropertyTransformConvention() {
            Properties()
               .Configure(c => c.Transform(s => {
                   var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
                   return string.Join("_", pattern.Matches(s)).ToLower();
               }));
        }
    }
}
