using System;
using Dapper.FluentMap.Conventions;
using Dapper.FluentMap;
using System.Text.RegularExpressions;

namespace Mgutz.DapperPg.Services {

    public static class ColumnMapper {
        public static void Initialize() {
            FluentMapper.Initialize(config => {
                config.AddConvention<PropertyTransformConvention>()
                      .ForEntitiesInCurrentAssembly("Mgutz.DapperPg.Models");
            });
        }
    }

    public class PropertyTransformConvention : Convention {
        public PropertyTransformConvention() {
            Properties()
                .Configure(c => c.Transform(s => {
                    Regex pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
                    return string.Join("_", pattern.Matches(s)).ToLower();
                }));
        }
    }

}
