using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WOCS.Common
{
    public static class EnumExtensions
    {
        public static string DisplayName<TEnum>(this TEnum value) where TEnum : Enum
        {
            var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
            return member?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
        }
    }
}
