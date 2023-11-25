﻿using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IHeroFactory heroFactory;

        private readonly ICollection<IHero> heroes;

        public Engine(IReader reader, IWriter writer, IHeroFactory heroFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroFactory = heroFactory;

            heroes = new List<IHero>();
        }

        public void Run()
        {
            int countHero = int.Parse(reader.ReadLine());

            while (countHero > 0)
            {
                string name = reader.ReadLine();
                string type = reader.ReadLine();

                try
                {
                    IHero hero = heroFactory.CreateHero(type, name);
                    heroes.Add(hero);
                    countHero--;
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var hero in heroes)
            {
                writer.WriteLine(hero.CastAbility());
            }

            int bossPower = int.Parse(reader.ReadLine());

            int heroesSumPower = heroes.Sum(h => h.Power);

            if (heroesSumPower >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
