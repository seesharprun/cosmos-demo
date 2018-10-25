using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CosmosBenchmark
{
    public static class Utilities
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static string GetDisplayShortName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetShortName();
        }
    }
}