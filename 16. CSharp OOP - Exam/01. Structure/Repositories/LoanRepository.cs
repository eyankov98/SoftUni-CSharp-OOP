using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> loans;

        public LoanRepository()
        {
            this.loans = new List<ILoan>();
        }

        public IReadOnlyCollection<ILoan> Models => this.loans;

        public void AddModel(ILoan model)
        {
            this.loans.Add(model);
        }

        public bool RemoveModel(ILoan model)
            => this.loans.Remove(model);

        public ILoan FirstModel(string name)
            => this.loans.FirstOrDefault(l => l.GetType().Name == name);
    }
}
