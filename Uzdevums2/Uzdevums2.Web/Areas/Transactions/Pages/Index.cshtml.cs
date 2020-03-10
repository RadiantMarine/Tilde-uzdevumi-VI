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
            OutgoingSum = 0.0M;
            IncomingSum = 0.0M;
            TotalBalance = 0.0M;
        }

        public decimal OutgoingSum { get; set; }

        public decimal IncomingSum { get; set; }

        public decimal TotalBalance { get; set; }

        public IList<FinancialTransaction> FinancialTransactions { get; set; }

        public async Task OnGetAsync()
        {
            // here we get the current user to only get FinancialTransactions that are related to current user
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            FinancialTransactions = await _context.FinancialTransactions
                .AsQueryable()
                .Where(ft => ft.FromUsername == currentUserName || ft.ToUsername == currentUserName)
                .ToListAsync();

            // Then we iterate through transactions to update owed or debt values based on target of transaction
            foreach (var transaction in FinancialTransactions)
            {
                if (transaction.FromUsername == currentUserName)
                {
                    OutgoingSum += transaction.Amount;
                }
                else
                {
                    IncomingSum += transaction.Amount;
                }
            }

            TotalBalance = OutgoingSum - IncomingSum;
        }
    }
}
