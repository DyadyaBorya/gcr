using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core
{
    public static class Configuration
    {
        /// <summary>
        /// Dictionary used to store the accessed settings/values.
        /// </summary>
        private static Dictionary<string, object> _settings = new Dictionary<string, object>();

        public static ConnectionStringSettings DatabaseConnectionSetting
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DB"];
            }
        }
        
        /// <summary>
        /// Main connection string to the database for application.
        /// </summary>
        public static string DatabaseConnectionString
        {
            get { return DatabaseConnectionSetting.ConnectionString; }
        }



        public static string UploadPath 
        {
            get { return GetRequiredValue<string>("UploadPath").TrimStart('~', '/').TrimEnd('/'); }
        }

        /// <summary>
        /// Gets a required value with the specified setting name.
        /// </summary>
        /// <typeparam name="T">Type of setting name.</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Setting name value in the specified type.</returns>
        private static T GetRequiredValue<T>(string settingName)
        {
            return GetValue<T>(settingName, true, default(T));
        }

        /// <summary>
        /// Gets an optional value with the specified setting name.
        /// </summary>
        /// <typeparam name="T">Type of setting name.</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">Default value if to use.</param>
        /// <returns>Setting name in the specified type or the default value.</returns>
        private static T GetOptionalValue<T>(string settingName, T defaultValue)
        {
            return GetValue(settingName, false, defaultValue);
        }

        /// <summary>
        /// Gets the setting name from the configuration file.
        /// </summary>
        /// <typeparam name="T">Type to return.</typeparam>
        /// <param name="settingName">Setting name to use.</param>
        /// <param name="isRequired">True if setting is required. False if not required.</param>
        /// <param name="defaultValue">Default value to use (if any).</param>
        /// <returns>Setting name value in the specified type.</returns>
        private static T GetValue<T>(string settingName, bool isRequired, T defaultValue)
        {
            // try to get the value from our cache
            object value = null;
            if (!_settings.TryGetValue(settingName, out value))
            {
                // try to get the setting
                string strValue = ConfigurationManager.AppSettings[settingName];

                if (strValue == null)
                {
                    if (isRequired)
                    {
                        throw new ConfigurationErrorsException("Application setting '" + settingName + "' is missing from configuration file and must be specified.");
                    }
                }
                else
                {
                    try
                    {
                        value = TypeConverter.Convert(strValue, false, defaultValue);
                    }
                    catch
                    {
                        throw new ConfigurationErrorsException("Application setting '" + settingName + "' is invalid. Could not convert '" + strValue + "' to type '" + typeof(T).FullName + ".");
                    }
                }

                _settings[settingName] = value;
            }

            return (T)value;
        }
    }
}
