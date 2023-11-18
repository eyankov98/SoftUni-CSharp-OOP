using BirthdayCelebrations.Models;
using BirthdayCelebrations.Models.Iterfaces;

namespace BirthdayCelebrations;

public class StartUp
{
    static void Main(string[] args)
    {
        List<IBirthable> society = new List<IBirthable>();

        string command = string.Empty;

        while((command = Console.ReadLine()) != "End")
        {
            string[] inputInfo = command
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string input = inputInfo[0];

            if (input == "Citizen")
            {
                string citizenName = inputInfo[1];
                int age = int.Parse(inputInfo[2]);
                string id = inputInfo[3];
                string citizenBirthdate = inputInfo[4];

                IBirthable citizen = new Citizen(citizenName, age, id, citizenBirthdate);

                society.Add(citizen);
            }
            else if (input == "Robot")
            {
                string model = inputInfo[1];
                string id = inputInfo[2];

                IIdentifiable robot = new Robot(model, id);
            }
            else if (input == "Pet")
            {
                string petName = inputInfo[1];
                string petBirthdate = inputInfo[2];

                IBirthable pet = new Pet(petName, petBirthdate);

                society.Add(pet);
            }
        }

        string year = Console.ReadLine();

        foreach (var element in society)
        {
            if (element.Birthdate.EndsWith(year))
            {
                Console.WriteLine(element.Birthdate);
            }
        }
    }
}