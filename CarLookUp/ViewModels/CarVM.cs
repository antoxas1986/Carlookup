using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarLookUp.Web.ViewModels
{
    public class CarVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [MinLength(3, ErrorMessage = "Make is required to have 3 letters")]
        [DisplayName("Make")]
        public string Maker { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MinLength(2, ErrorMessage = "Model is required to have 2 letters")]
        [MaxLength(20, ErrorMessage = "Model is required to have 20 letters at most")]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2016, ErrorMessage = "Year is required to be between 1900 and 2016")]
        [DisplayName("Year")]
        public int Year { get; set; }
    }
}
