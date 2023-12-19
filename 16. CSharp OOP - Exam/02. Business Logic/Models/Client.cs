using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Client : IClient
    {
        private string name;
        private string id;
        private double income;

        public Client(string name, string id, int interest, double income)
        {
            this.Name = name;
            this.Id = id;
            this.Interest = interest;
            this.Income = income;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ClientNameNullOrWhitespace));
                }

                this.name = value;
            }
        }

        public string Id
        {
            get => this.id;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ClientIdNullOrWhitespace));
                }

                this.id = value;
            }
        }

        public int Interest { get; protected set; }

        public double Income
        {
            get => this.income;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ClientIncomeBelowZero));
                }

                this.income = value;
            }
        }

        public abstract void IncreaseInterest();
    }
}
