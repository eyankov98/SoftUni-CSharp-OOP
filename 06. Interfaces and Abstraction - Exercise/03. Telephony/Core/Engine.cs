using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Core.Interfaces;
using Telephony.Models.Interfaces;
using Telephony.Models;
using Telephony.IO.Interfaces;

namespace Telephony.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] phoneNumbers = reader.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] urls = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICallable phone = null;

            foreach (var phoneNumber in phoneNumbers)
            {
                if (phoneNumber.Length == 10)
                {
                    phone = new Smartphone();
                }
                else if (phoneNumber.Length == 7)
                {
                    phone = new StationaryPhone();
                }

                try
                {
                    writer.WriteLine(phone.Call(phoneNumber));
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            IBrowsable browsable = new Smartphone();

            foreach (var url in urls)
            {
                try
                {
                    writer.WriteLine(browsable.Browse(url));
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}