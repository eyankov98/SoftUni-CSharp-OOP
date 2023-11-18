using FoodShortage.Models.Interfaces;

namespace FoodShortage.Models;

public class StartUp
{
    static void Main(string[] args)
    {
        List<IBuyer> buyers = new List<IBuyer>();

        int countCitizens = int.Parse(Console.ReadLine());

        for (int i = 0; i < countCitizens; i++)
        {
            string[] inputInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (inputInfo.Length == 4)
            {
                string citizenName = inputInfo[0];
                int citizenAge = int.Parse(inputInfo[1]);
                string id = inputInfo[2];
                string birthdate = inputInfo[3];

                IBuyer citizen = new Citizen(citizenName, citizenAge, id, birthdate);

                buyers.Add(citizen);
            }
            else if (inputInfo.Length == 3)
            {
                string rebelName = inputInfo[0];
                int rebelAge = int.Parse(inputInfo[1]);
                string group = inputInfo[2];

                IBuyer rebel = new Rebel(rebelName, rebelAge, group);

                buyers.Add(rebel);
            }
        }

        string command = string.Empty;

        while ((command = Console.ReadLine()) != "End")
        {
            var buyer = buyers.FirstOrDefault(b => b.Name == command);

            if (buyer != null)
            {
                buyer.BuyFood();
            }
        }

        Console.WriteLine(buyers.Sum(b => b.Food));
    }
}