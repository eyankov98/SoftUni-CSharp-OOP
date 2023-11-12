using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree.Models
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person (string name, decimal money)
        {
            this.Name = name;
            this.Money = money;

            products = new List<Product>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameEmpty);
                }

                this.name = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.money;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.MoneyNegative);
                }

                this.money = value;
            }
        }

        public string Add(Product product)
        {
            if (this.Money < product.Cost)
            {
                return $"{this.Name} can't afford {product.Name}";
            }

            this.products.Add(product);

            Money -= product.Cost;

            return $"{this.Name} bought {product.Name}";
        }

        public override string ToString()
        {
            /*string productsString = products.Any()
                ? string.Join(", ", products.Select(p => p.Name)) 
                : "Nothing bought";

            return $"{this.Name} - {productsString}";*/

            if (products.Any())
            {
                return $"{this.Name} - {string.Join(", ", products.Select(p => p.Name))}";
            }
            else
            {
                return $"{this.Name} - Nothing bought";
            }
        }
    }
}