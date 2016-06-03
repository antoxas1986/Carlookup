using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarLookUp.Web.ViewModels
{
    public class CarVMWithBodyTypeName : CarVM
    {
        public string BodyType { get; set; }
    }
}
