using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Application.Entensions
{
    public static class EnumExtensions
    {
        //this is a reflection example
        public static string GetDescription(this Enum obj)
        {
            var type = obj.GetType(); //get obj type
            var memberInfo = type.GetMember(obj.ToString()); //give obj info
            var attributes = memberInfo.FirstOrDefault().GetCustomAttributes
                (typeof(DescriptionAttribute), false); //give obj first attribute

            return 
                attributes.Length > 0 
                ? ((DescriptionAttribute)attributes.FirstOrDefault()).Description
                : obj.ToString();
        }
    }
}
