using System;

namespace PersonsInfo;

public class StartUp
{
    public static void Main(string[] args)
    {
        int countPerson = int.Parse(Console.ReadLine());

        Team team = new Team("SoftUni");

        for (int i = 0; i < countPerson; i++)
        {
            string[] personInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string firstName = personInfo[0];
            string lastName = personInfo[1];
            int age = int.Parse(personInfo[2]);
            decimal salary = decimal.Parse(personInfo[3]);

            Person person = new Person(firstName, lastName, age, salary);

            team.AddPlayer(person);
        }

        Console.WriteLine($"First team has {team.FirstTeam.Count} players.");
        Console.WriteLine($"Reserve team has {team.ReserveTeam.Count} players.");
    }
}