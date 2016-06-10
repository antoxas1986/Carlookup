using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Core.Models
{
    /// <summary>
    /// Object to hold all validation messages.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{CarLookUp.Core.Models.ValidationMessage}" />
    public class ValidationMessageList : List<ValidationMessage>
    {
        public string GetFirstErrorMsg
        {
            get
            {
                return this.Where(m => m.Type == Enum.MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            }
        }

        public bool HasError
        {
            get
            {
                return this.Where(x => x.Type == Enum.MessageTypes.Error).Any();
            }
        }
    }
}
