using System;
using System.ComponentModel.DataAnnotations;

namespace Uzdevums2.Web.Models
{
    public class FinancialTransaction
    {
        int FinancialTransactionId { get; set; }

        [Required]
        public string FromUsername { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Display(Name = "Transaction target")]
        public string ToUsername { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public double Amount { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
        public string Description { get; set; }
    }
}
