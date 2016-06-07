using System.ComponentModel.DataAnnotations;

namespace CarLookUp.Web.ViewModels
{
    public class UserVM
    {
        public int RoleId { get; set; }

        [Required]
        [MinLength(2)]
        public string UserName { get; set; }
    }
}
