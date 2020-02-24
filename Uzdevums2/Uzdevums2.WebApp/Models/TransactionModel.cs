using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uzdevums2.WebApp.Enums;

namespace Uzdevums2.WebApp.Models
{
    public class TransactionModel
    {
        int TransactionId { get; set; }

        int FromCustomerId { get; set; }

        int ToCustomerId { get; set; }

        TransactionType Type { get; set; }

        double Amount { get; set; }
    }
}
