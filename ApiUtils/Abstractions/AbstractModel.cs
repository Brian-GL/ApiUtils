using ApiUtils.Extensions;
using System.Reflection;

namespace ApiUtils.Abstractions
{
    internal class AbstractModel<TModel> where TModel : class
    {
        /// <summary>
        /// Trims every <see cref="string"/> field in <typeparamref name="TModel"/>
        /// </summary>
        /// <param name="instance">Referenced instance</param>
        protected static void Trimming(ref TModel instance)
        {
            Type modelType = instance.GetType();
            FieldInfo[] fields = modelType.GetFields();

            foreach (FieldInfo field in fields)
            {
                if(field.FieldType.Equals(o: typeof(string)))
                {
                    string? value = field.GetValue(obj: instance) as string;
                    field.SetValue(obj: instance, value: value.Available().Trim());
                }
            }

        }
    }
}
