using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Uzdevums2.Web.Areas.Transactions.Pages
{
    public class MyTransactionsModel : PageModel
    {
        //private readonly FinancialTransactionManager _financialTransactionManager;

        public string Username { get; set; }

        public List<UserTransaction> UserTransactions { get; set; }

        public string ReturnUrl { get; set; }

        // TODO: Consider adding date of transaction
        public class UserTransaction
        {
            string Id { get; set; }

            [Display(Name = "Transaction target")]
            public string ToUsername { get; set; }

            [Display(Name = "Amount")]
            public double Amount { get; set; }

            [Display(Name = "Transaction description")]
            public string Description { get; set; }
        }

        public async Task OnGet(string userName, string returnUrl = null)
        {
            Username = userName;
            //UserTransactions = await _financialTransactionManager.GetTransactionsAsync(userName);

            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;
        }
    }
}
