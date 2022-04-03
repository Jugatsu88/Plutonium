using Plutonium.Classes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Plutonium.Helpers
{
    public static class AttributeHelper
    {

        public static string GetDisplayAttributeName(Type type)
        {
            string result = string.Empty;
            var modelDisplay = type.GetCustomAttributes(typeof(DisplayAttribute),
                            false).FirstOrDefault();
            if (modelDisplay != null)
            {
                result = ((DisplayAttribute)modelDisplay).Name;
            }
            else
            {
                result = type.Name;
            }
            return result;
        }

        public static string GetDisplayAttributeName(PropertyInfo propertyInfo)
        {
            string result = string.Empty;
            var propDisplay = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute),
                            false).FirstOrDefault();
            if (propDisplay != null)
            {
                result = ((DisplayAttribute)propDisplay).Name;
            }
            else
            {
                result = propertyInfo.Name;
            }
            return result;
        }


        public static string GetPropertyTypeName(PropertyInfo propertyInfo)
        {
            string result = string.Empty;
            string PropertyTypeName = propertyInfo.PropertyType.Name;
            if (PropertyTypeName == "Int32")
            {
                result = "Int";
            }
            else if (PropertyTypeName == "DateTime")
            {
                result = "Date";
            }
            else
            {
                result = PropertyTypeName; //  ('Date', 'Float', 'Int','String'))
            }
            return result;
        }

        public static int GetPropertyOrdering(PropertyInfo propertyInfo)
        {
            int result = 0;
            var propDisplay = propertyInfo.GetCustomAttributes(typeof(CRUDAttribute),
                         false).FirstOrDefault();
            if (propDisplay != null)
            {
                result = ((CRUDAttribute)propDisplay).Ordering;
            }
            return result;
        }

        public static bool GetPropertyIsVisible(PropertyInfo propertyInfo)
        {
            bool result = true;
            var propDisplay = propertyInfo.GetCustomAttributes(typeof(CRUDAttribute),
                           false).FirstOrDefault();
            if (propDisplay != null)
            {
                result = ((CRUDAttribute)propDisplay).IsVisible;
            } 
            return result;
        }
         




    }
}
