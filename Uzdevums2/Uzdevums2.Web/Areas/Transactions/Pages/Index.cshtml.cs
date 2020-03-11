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

            // Can't add to an unitialized list.
            OutgoingFinancialTransactions = new List<FinancialTransaction>();
            IncomingFinancialTransactions = new List<FinancialTransaction>();
        }

        public decimal Loan { get; private set; }

        public decimal Debt { get; private set; }

        public decimal TotalBalance { get; set; }

        public IList<FinancialTransaction> OutgoingFinancialTransactions { get; private set; }

        public IList<FinancialTransaction> IncomingFinancialTransactions { get; private set; }

        public async Task OnGetAsync()
        {
            // Looks up current user in HttpContext based on ClaimTypes.Name. By default Identity puts it as email.
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var AllFinancialTransactions = await GetUserFinancialTransactions(currentUserName);            

            // Then we iterate through transactions to shuffle them into incoming/outgoing lists
            foreach (var transaction in AllFinancialTransactions)
            {
                if (transaction.FromUsername == currentUserName)
                {
                    HandleOutgoingTransaction(transaction);
                    
                }
                else
                {
                    HandleIncomingTransaction(transaction);
                    
                }
            }

            TotalBalance = Loan - Debt;
        }


        /// <summary>
        /// Returns user's incoming and outgoing financial transactions without sorting it.
        /// </summary>
        /// <param name="currentUserName">Current user's name used to look up transactions</param>
        /// <returns>List of unsorted financial transactions</returns>
        private async Task<List<FinancialTransaction>> GetUserFinancialTransactions(string currentUserName)
        {
            // here we get the current user to only get FinancialTransactions that are related to current user
            return await _context.FinancialTransactions
                .AsQueryable()
                .Where(ft => ft.FromUsername == currentUserName || ft.ToUsername == currentUserName)
                .ToListAsync();
        }

        /// <summary>
        /// Adds transaction to Incoming list, also updates Debt and Loan according to transaction's IsLoan state
        /// </summary>
        private void HandleIncomingTransaction(FinancialTransaction transaction)
        {
            // An incoming transaction is either a user getting a loan (Debt+=) or getting their loan repaid(Loan-=)
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

        /// <summary>
        /// Adds transaction to Outgoing list, also updates Debt and Loan according to transaction's IsLoan state
        /// </summary>
        private void HandleOutgoingTransaction(FinancialTransaction transaction)
        {
            // An outgoing transaction is either a user giving a loan (Loan+=) or returning money(Debt-=)
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
    }
}
