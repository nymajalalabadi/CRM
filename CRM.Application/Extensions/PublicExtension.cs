using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Extensions
{
    public static class PublicExtension
    {
        public static string GetEnumDisplayName(this Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType()
                .GetMember(myEnum.ToString())
                .FirstOrDefault();

            if (enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>().GetName();
            }

            return "";
        }
    }
}
