using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private readonly IAnimalFactory animalFactory;
        private readonly IFoodFactory foodFactory;

        private readonly ICollection<IAnimal> animals;

        public Engine(IReader reader, IWriter writer, IAnimalFactory animalFactory, IFoodFactory foodFactory)
        {
            this.reader = reader;
            this.writer = writer;

            this.animalFactory = animalFactory;
            this.foodFactory = foodFactory;

            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string command;

            while ((command = reader.ReadLine()) != "End")
            {
                IAnimal animal = CreateAnimal(command);
                IFood food = CreateFood();
                writer.WriteLine(animal.ProduceSound());

                try
                {
                    animal.Eat(food);

                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }

                animals.Add(animal);
            }

            foreach (IAnimal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }

        private IAnimal CreateAnimal(string command)
        {
            string[] animalInfo = command
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IAnimal animal = animalFactory.CreateAnimal(animalInfo);

            return animal;
        }

        private IFood CreateFood()
        {
            string[] foodInfo = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string foodType = foodInfo[0];
            int foodQuantity = int.Parse(foodInfo[1]);

            IFood food = foodFactory.CreateFood(foodType, foodQuantity);

            return food;
        }
    }
}
