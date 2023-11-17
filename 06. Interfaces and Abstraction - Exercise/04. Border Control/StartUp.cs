using BorderControl.Models;
using BorderControl.Models.Interfaces;

namespace BorderControl;

public class StartUp
{
    static void Main(string[] args)
    {
        List<IIdentifiable> society = new List<IIdentifiable>();

        string command = string.Empty;

        while ((command = Console.ReadLine()) != "End")
        {
            string[] inputInfo = command
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (inputInfo.Length == 3) // citizen
            {
                string name = inputInfo[0];
                int age = int.Parse(inputInfo[1]);
                string idForCitizens = inputInfo[2];

                IIdentifiable citizen = new Citizen(name, age, idForCitizens);

                society.Add(citizen);
            }
            else if (inputInfo.Length == 2) // robot
            {
                string model = inputInfo[0];
                string idForRobots = inputInfo[1];

                IIdentifiable robot = new Robot(model, idForRobots);

                society.Add(robot);
            }
        }

        string fakeIds = Console.ReadLine();

        foreach (var element in society)
        {
            if (element.Id.EndsWith(fakeIds))
            {
                Console.WriteLine(element.Id);
            }
        }
    }
}