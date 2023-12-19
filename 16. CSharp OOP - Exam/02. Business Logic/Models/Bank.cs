using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Bank : IBank
    {
        private string name;

        private List<ILoan> loans;
        private List<IClient> clients;

        public Bank(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            this.loans = new List<ILoan>();
            this.clients = new List<IClient>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BankNameNullOrWhiteSpace));
                }

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => this.loans;

        public IReadOnlyCollection<IClient> Clients => this.clients;

        public double SumRates()
            => this.loans.Sum(l => l.InterestRate);


        public void AddClient(IClient Client)
        {
            if (this.Capacity <= this.clients.Count)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }

            this.clients.Add(Client);
        }

        public void RemoveClient(IClient Client)
        {
            this.clients.Remove(Client);
        }

        public void AddLoan(ILoan loan)
        {
            this.loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {this.Name}, Type: {this.GetType().Name}");
            sb.Append("Clients: ");

            if (this.clients.Any())
            {
                sb.AppendLine(string.Join(", ", this.clients.Select(c => c.Name)));
            }
            else
            {
                sb.AppendLine("none");
            }

            sb.AppendLine($"Loans: {this.loans.Count}, Sum of Rates: {SumRates()}");

            return sb.ToString().TrimEnd();
        }
    }
}
