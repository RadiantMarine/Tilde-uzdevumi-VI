using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uzdevums2.Web.Data;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public FinancialTransaction FinancialTransaction { get; set; }

        public bool IsOutgoing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FinancialTransaction = await _context.FinancialTransactions.FirstOrDefaultAsync(m => m.FinancialTransactionId == id);

            if (FinancialTransaction == null)
            {
                return NotFound();
            }

            CheckIsOutgoing();
            return Page();
        }

        /// <summary>
        /// Check if Financial Transaction was outgoing for current user
        /// based on this, details page will show different fields
        /// </summary>
        private void CheckIsOutgoing()
        {
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            IsOutgoing = FinancialTransaction.FromUsername == currentUserName;
        }
    }
}
