using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core
{
    public static class TypeConverter
    {
        /// <summary>
        /// Converts a value to a specified type
        /// </summary>
        /// <typeparam name="T">Type to return.</typeparam>
        /// <param name="isRequired">True if value is required. False if not required.</param>
        /// <param name="defaultValue">Default value to use (if any).</param>
        public static T Convert<T>(object value, bool isRequired, T defaultValue)
        {
            if (value == null)
            {
                if (isRequired)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    value = defaultValue;
                }
            }
            else
            {
                try
                {
                    value = System.Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidOperationException("Value is invalid. Could not convert '" + value + "' to type '" + typeof(T).FullName + ".");
                }
            }
            return (T)value;
        }
    }
}
