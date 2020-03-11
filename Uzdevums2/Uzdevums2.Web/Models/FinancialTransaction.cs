using System;
using System.ComponentModel.DataAnnotations;

namespace Uzdevums2.Web.Models
{
    public class FinancialTransaction
    {
        public int FinancialTransactionId { get; set; }

        [Required]
        [Display(Name = "Transaction initiator")]
        public string FromUsername { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Display(Name = "Transaction target")]
        public string ToUsername { get; set; }

        [Required]
        [Range(0, 99999999999999.9999)]
        [Display(Name = "Amount €")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Is this a loan?")]
        public bool IsLoan { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
