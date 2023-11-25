using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Paladin : Hero
    {
        private const int PaladinDefaultPower = 100;
        public Paladin(string name) 
            : base(name, PaladinDefaultPower)
        {
        }

        public override string CastAbility()
            => $"{this.GetType().Name} - {Name} healed for {Power}";
    }
}
