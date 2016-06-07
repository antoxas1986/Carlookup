using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.Exceptions
{
    /// <summary>
    /// Custom application settings exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ApplicationSettingsException : Exception
    {
        public ApplicationSettingsException(string message) : base(message)
        {
        }
    }
}
