using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Rogue : Hero
    {
        private const int RogueDefaultPower = 80;

        public Rogue(string name) 
            : base(name, RogueDefaultPower)
        {
        }

        public override string CastAbility()
            => $"{this.GetType().Name} - {Name} hit for {Power} damage";
    }
}
