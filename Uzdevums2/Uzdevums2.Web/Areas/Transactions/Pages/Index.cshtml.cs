using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uzdevums2.Web.Data;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal Owed { get; set; }

        public decimal Debt { get; set; }

        public decimal Total { get; set; }

        public IList<FinancialTransaction> FinancialTransactions { get; set; }

        public async Task OnGetAsync()
        {
            // here we get the current user to only get FinancialTransactions that are related to current user
            var currentUserName = "";

            FinancialTransactions = await _context.FinancialTransactions
                .AsQueryable()
                .Where(ft => ft.FromUsername == currentUserName || ft.ToUsername == currentUserName)
                .ToListAsync();

            // Then we iterate through transactions to update owed or debt values based on target of transaction
            foreach (var transaction in FinancialTransactions)
            {
                if (transaction.FromUsername == currentUserName)
                {
                    Owed += transaction.Amount;
                }
                else
                {
                    Debt += transaction.Amount;
                }
            }

            Total = Owed - Debt;
        }
    }
}
