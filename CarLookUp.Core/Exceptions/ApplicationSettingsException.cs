using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.Exceptions
{
    public class ApplicationSettingsException : Exception
    {
        public ApplicationSettingsException(string message) : base(message)
        {
        }
    }
}
