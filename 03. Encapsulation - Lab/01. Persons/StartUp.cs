namespace PersonsInfo;

public class StartUp
{
    public static void Main(string[] args)
    {
        int peopleCount = int.Parse(Console.ReadLine());

        List<Person> people = new List<Person>();

        for (int i = 0; i < peopleCount; i++)
        {
            string[] personInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string firstName = personInfo[0];
            string lastName = personInfo[1];
            int age = int.Parse(personInfo[2]);

            Person person = new Person(firstName, lastName, age);
            people.Add(person);
        }

        foreach (Person person in people.OrderBy(p => p.FirstName))
        {
            Console.WriteLine(person);
        }
    }
}
