using CarLookUp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.ApplicationSettings
{
    /// <summary>
    /// Base Application Settings
    /// </summary>
    public class BaseApplicationSettings
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationSettingsException"></exception>
        protected static string Get(string key)
        {
            var settings = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(settings))
            {
                throw new ApplicationSettingsException(string.Format("The setting '{0}' has not been set.", key));
            }
            return settings;
        }
    }
}
