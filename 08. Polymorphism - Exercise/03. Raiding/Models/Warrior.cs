using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Warrior : Hero
    {
        private const int WarriorDefaultPower = 100;
        public Warrior(string name) 
            : base(name, WarriorDefaultPower)
        {
        }

        public override string CastAbility() 
            => $"{this.GetType().Name} - {Name} hit for {Power} damage";
    }
}
