using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uzdevums2.Web.Data;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            OutgoingFinancialTransactions = new List<FinancialTransaction>();
            IncomingFinancialTransactions = new List<FinancialTransaction>();

            Loan = 0.0M;
            Debt = 0.0M;
            TotalBalance = 0.0M;
        }

        public decimal Loan { get; set; }

        public decimal Debt { get; set; }

        public decimal TotalBalance { get; set; }

        public IList<FinancialTransaction> OutgoingFinancialTransactions { get; set; }

        public IList<FinancialTransaction> IncomingFinancialTransactions { get; set; }

        public async Task OnGetAsync()
        {
            // here we get the current user to only get FinancialTransactions that are related to current user
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var AllFinancialTransactions = await _context.FinancialTransactions
                .AsQueryable()
                .Where(ft => ft.FromUsername == currentUserName || ft.ToUsername == currentUserName)
                .ToListAsync();

            // Then we iterate through transactions to shuffle them into incoming/outgoing lists
            // And based on target of transaction
            foreach (var transaction in AllFinancialTransactions)
            {
                if (transaction.FromUsername == currentUserName)
                {
                    OutgoingFinancialTransactions.Add(transaction);
                    if (transaction.IsLoan)
                    {
                        Loan += transaction.Amount;
                    }
                    else
                    {
                        Debt -= transaction.Amount;
                    }
                }
                else
                {
                    IncomingFinancialTransactions.Add(transaction);
                    if (transaction.IsLoan)
                    {
                        Debt += transaction.Amount;
                    }
                    else
                    {
                        Loan -= transaction.Amount;
                    }
                }
            }

            TotalBalance = Loan - Debt;
        }
    }
}
