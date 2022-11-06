using API.ModelsDTO;
using System.ComponentModel;
using System.Reflection;

namespace API.Services
{
    public static class EnumService<T>
    {
        public static List<EnumDescriptionDTO<T>> GetEnumValues()
        {
            try
            {
                List<EnumDescriptionDTO<T>> enumValues = new List<EnumDescriptionDTO<T>>();

                foreach (T value in Enum.GetValues(typeof(T)))
                {
                    string name = GetEnumDescription(value);
                    EnumDescriptionDTO<T> enumValue = new EnumDescriptionDTO<T>(value, name);
                    enumValues.Add(enumValue);
                }

                return enumValues;
            }
            catch (Exception ex)
            {
                return new List<EnumDescriptionDTO<T>>();
            }
        }

        public static string GetEnumDescription(T value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                var customAttr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (customAttr != null && customAttr.Any())
                    return customAttr[0].Description;
                else
                    return String.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
