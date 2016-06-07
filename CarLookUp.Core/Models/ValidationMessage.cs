using CarLookUp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Core.Models
{
    /// <summary>
    /// Validation message model
    /// </summary>
    public class ValidationMessage
    {
        public ValidationMessage(MessageTypes type, string messsageText)
        {
            Type = type;
            Text = messsageText;
        }

        public string Text { get; set; }
        public MessageTypes Type { get; set; }
    }
}
