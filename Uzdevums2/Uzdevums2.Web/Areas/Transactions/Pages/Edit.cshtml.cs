using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Uzdevums2.Web.Data;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FinancialTransaction> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(
            ApplicationDbContext context,
            ILogger<FinancialTransaction> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [BindProperty]
        public FinancialTransaction FinancialTransaction { get; set; }

        public bool IsOutgoing { get; private set; }

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

        private void CheckIsOutgoing()
        {
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            IsOutgoing = FinancialTransaction.FromUsername == currentUserName;
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FinancialTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"User edited transaction {FinancialTransaction.FinancialTransactionId}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialTransactionExists(FinancialTransaction.FinancialTransactionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FinancialTransactionExists(int id)
        {
            return _context.FinancialTransactions.Any(e => e.FinancialTransactionId == id);
        }
    }
}
