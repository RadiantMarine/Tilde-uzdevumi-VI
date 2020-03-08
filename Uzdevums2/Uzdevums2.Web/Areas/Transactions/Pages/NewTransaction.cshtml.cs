using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Uzdevums2.WebClient.Areas.Identity.Pages.Transactions
{
    public class NewTransactionModel : PageModel
    {
        private readonly ILogger<NewTransactionModel> _logger;

        public NewTransactionModel(
            ILogger<NewTransactionModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        // TODO: Consider adding date of transaction
        public class InputModel
        {
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

        public async Task OnGetAsync(string userName, string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            Input.FromUsername = userName;

            returnUrl = returnUrl ?? Url.Content("~/");

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {

                //_context.Customers.Add(InputModel);
                //await _context.SaveChangesAsync();
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = true; //await _financialTransactionManager.AddTransactionAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result)
                {
                    _logger.LogInformation("User added transaction.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid transaction.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
