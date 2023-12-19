﻿using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> banks;

        public BankRepository()
        {
            this.banks = new List<IBank>();
        }

        public IReadOnlyCollection<IBank> Models => this.banks;

        public void AddModel(IBank model)
        {
            this.banks.Add(model);
        }

        public bool RemoveModel(IBank model)
            => this.banks.Remove(model);

        public IBank FirstModel(string name)
            => this.banks.FirstOrDefault(b => b.Name == name);
    }
}
