using System.ComponentModel.DataAnnotations;

namespace FBV.API.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}
