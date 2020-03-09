using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Uzdevums2.Web.Data;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<FinancialTransaction> _logger;
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context,
            ILogger<FinancialTransaction> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult OnGet()
        {
            //TODO: FinancialTransaction.FromUsername =
            return Page();
        }

        [BindProperty]
        public FinancialTransaction FinancialTransaction { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FinancialTransactions.Add(FinancialTransaction);
            var newTransactionId = await _context.SaveChangesAsync();
            _logger.LogInformation($"User added transaction {newTransactionId}.");

            return RedirectToPage("./Index");
        }
    }
}
