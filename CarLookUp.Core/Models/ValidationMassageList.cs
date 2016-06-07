using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Core.Models
{
    /// <summary>
    /// Object to hold all validation messages.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{CarLookUp.Core.Models.ValidationMessage}" />
    public class ValidationMassageList : List<ValidationMessage>
    {
        public bool HasError
        {
            get
            {
                return this.Where(x => x.Type == Enum.MessageTypes.Error).Any();
            }
        }
    }
}
