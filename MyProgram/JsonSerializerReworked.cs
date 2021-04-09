using System;
using System.Reflection;
using System.Linq;

namespace MyProgram
{
    public class JsonSerializerReworked
    {
        public string Serialize<T>(T obj)
        {
            return Serialize(obj, typeof(T));
        }

        private string Serialize(object obj, Type type)
        {
            var stringRepresentations = type.GetProperties()
                .Select(p => $"\"{p.Name}\" , {GetValueString(p, obj)}");

            return $"{{ {string.Join(", ", stringRepresentations)} }}";
        }

        private string GetValueString<T>(PropertyInfo propertyInfo, T obj)
        {
            var value = propertyInfo.GetValue(obj);

            if (value is null)
                return "null";

            if (propertyInfo.PropertyType == typeof(string))
                return $"\"{value}\"";

            return value.ToString();
        }
    }
}
