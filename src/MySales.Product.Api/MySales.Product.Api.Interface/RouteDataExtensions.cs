using Microsoft.AspNetCore.Routing;
using System;

namespace MySales.Product.Api.Interface
{
    /// <summary>
    /// Contains extensions methods for RouteData
    /// </summary>
    public static class RouteDataExtensions
    {
        /// <summary>
        /// Get values from route values.
        /// </summary>
        /// <typeparam name="T">Type of value will be returned.</typeparam>
        /// <param name="routeData">Route's data.</param>
        /// <param name="key">Name of route data to get value.</param>
        /// <returns>Returns the value of route.</returns>
        public static T GetValue<T>(this RouteData routeData, string key)
        {
            var value = routeData?.Values[key];

            if (value.ToString() == "null")
            {
                value = null;
            }

            if (typeof(T) == typeof(Guid))
            {
                if (Guid.TryParse(Convert.ToString(value), out var result))
                {
                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }


            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
