using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Druid : Hero
    {
        private const int DruidDefaultPower = 80;

        public Druid(string name) 
            : base(name, DruidDefaultPower)
        {
        }

        public override string CastAbility()
            => $"{this.GetType().Name} - {Name} healed for {Power}";
    }
}
