using System.Dynamic;
using System.Reflection;

namespace Presentation.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<T>(this IEnumerable<T> enumerable, string properties)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            var expandoObjectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();


            if (string.IsNullOrEmpty(properties))
            {
                var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var propertiesSplited = properties.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var prop in propertiesSplited)
                {

                    var propName = prop.Trim();

                    var propertyInfo =
                        typeof(T).GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo is null)
                        continue;

                    propertyInfoList.Add(propertyInfo);
                }

            }

            foreach (T item in enumerable)
            {
                var dataShapedObject = new ExpandoObject();

                foreach (var propInfo in propertyInfoList)
                {
                    var propvalue = propInfo.GetValue(item);

                    ((IDictionary<string, object>)dataShapedObject).Add(propInfo.Name, propvalue);
                }

                expandoObjectList.Add(dataShapedObject);
            }

            return expandoObjectList;
        }
    }
}
