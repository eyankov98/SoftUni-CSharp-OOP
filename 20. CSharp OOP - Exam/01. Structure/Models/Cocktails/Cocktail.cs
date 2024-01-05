using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;

        public Cocktail (string name, string size, double price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
        }

        public string Name
        {
            get => this.name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.name = value;
            }
        }

        public string Size { get; private set; }

        public double Price
        {
            get => this.price;
            private set
            {
                if (Size == "Small")
                {
                    value /= 3;
                }
                else if (Size == "Middle")
                {
                    value = (value / 3) * 2;
                }

                this.price = value;
            }
        }

        public override string ToString()
            => $"{this.Name} ({this.Size}) - {Price:F2} lv";
    }
}
