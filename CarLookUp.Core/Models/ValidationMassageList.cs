using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Core.Models
{
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
