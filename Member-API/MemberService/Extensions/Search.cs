using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemberService.Extensions
{
    public static class Search
    {
        public static Predicate<T> SearchFromQuery<T>(IQueryCollection query)
        {
            var predicateList = new List<Predicate<T>>
            {
                t => true
            };
            predicateList.AddRange(query.Select(option => (Predicate<T>) ((T anyObject) => Condition(anyObject, option.Key, option.Value))));

            var predicate = And(predicateList.ToArray());
            return predicate;
        }

        private static bool Condition(object anyObject, string key, string value)
        {
            if(GetPropertyType(anyObject, key) == typeof(string))
            {
                return GetThePropertyValue(anyObject, key).ToString()
                    .Contains(value);
            }
            return GetThePropertyValue(anyObject, key)
                  .Equals(ConvertedValue(key, value, anyObject));
        }

        public static object GetThePropertyValue(object anyObject, string key)
        {
            return anyObject.GetType().GetProperty(key).GetValue(anyObject);
        }

        private static Type GetPropertyType(object anyObject, string key)
        {
            return anyObject.GetType().GetProperty(key).PropertyType;
        }

        private static object ConvertedValue(string key, string value, object anyObject)
        {
            if(GetPropertyType(anyObject, key).IsEnum)
            {
                return Enum.Parse(GetPropertyType(anyObject, key), value);
            }
            return Convert.ChangeType((string)value, GetPropertyType(anyObject, key));
        }

        private static Predicate<T> And<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                return predicates.All(predicate => predicate(item));
            };
        }
    }
}
