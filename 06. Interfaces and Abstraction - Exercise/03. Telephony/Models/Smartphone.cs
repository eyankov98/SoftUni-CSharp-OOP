using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string phoneNumber)
        {
            if (!ValidatePhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {phoneNumber}";
        }

        public string Browse(string url)
        {
            if (!ValidateUrl(url))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public bool ValidatePhoneNumber(string phoneNumber) 
            => phoneNumber.All(ch => char.IsDigit(ch)); // return phoneNumber.All(ch => char.IsDigit(ch));

        public bool ValidateUrl(string url)
            => url.All(ch => !char.IsDigit(ch));
    }
}