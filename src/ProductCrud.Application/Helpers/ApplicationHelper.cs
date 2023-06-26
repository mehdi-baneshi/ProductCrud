using System.Linq.Expressions;
using System.Reflection;

namespace ProductCrud.Application.Helpers
{
    public static class ApplicationHelper
    {
        public static List<Expression<Func<T, bool>>> GetExpressionsFromDto<T, TFilter>(TFilter filterClass)
        {
            if (filterClass == null)
            {
                return new List<Expression<Func<T, bool>>>();
            }

            List<Expression<Func<T, bool>>> predicates = new();

            var props = filterClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var propertyType = prop.PropertyType;
                object defaultValue = propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;

                if (!prop.GetValue(filterClass)?.Equals(defaultValue) ?? false)
                {
                    Expression<Func<T, bool>> predicate = null;

                    if (propertyType==typeof(string))
                    {
                        predicate = t => t.GetType().GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance) != null &&
                                                               String.Equals(t.GetType().GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance).GetValue(t).ToString(), prop.GetValue(filterClass).ToString(), StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        predicate = t => t.GetType().GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance) != null &&
                                                               t.GetType().GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance).GetValue(t).Equals(prop.GetValue(filterClass));
                    }
                    predicates.Add(predicate);
                }
            }

            return predicates;
        }
    }
}
