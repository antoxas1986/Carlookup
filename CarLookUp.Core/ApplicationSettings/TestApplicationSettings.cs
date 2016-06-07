using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.ApplicationSettings
{
    public class TestApplicationSettings : BaseApplicationSettings
    {
        public static string Test { get { return Get("TestValue"); } }
    }
}
