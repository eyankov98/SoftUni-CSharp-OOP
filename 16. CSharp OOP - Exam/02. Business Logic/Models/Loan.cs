using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Loan : ILoan
    {
        public Loan(int interestRate, double amount)
        {
            this.InterestRate = interestRate;
            this.Amount = amount;
        }

        public int InterestRate { get; private set; }

        public double Amount { get; private set; }
    }
}
