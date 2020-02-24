using System.ComponentModel.DataAnnotations;

namespace Uzdevums2.WebApp.Models
{
    public class CustomerModel
    {
        [Required]
        int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(250)]
        string Name { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(250)]
        string Surname { get; set; }
    }
}
