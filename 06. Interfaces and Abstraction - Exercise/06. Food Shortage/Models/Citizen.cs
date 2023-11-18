using FoodShortage.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage.Models
{
    public class Citizen : INameable, IIdentifiable, IBirthable, IBuyer
    {
        private const int DefaultFoodIncrement = 10;

        public Citizen(string name, int age, string id, string birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
        }

        public string Name { get; private set; } 
        public int Age { get; private set; }
        public string Id { get; private set; } 
        public string Birthdate { get; private set; } 
        public int Food { get; private set; } 

    public void BuyFood()
        {
            Food += DefaultFoodIncrement;
        }
    }
}