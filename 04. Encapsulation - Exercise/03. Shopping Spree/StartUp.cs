namespace ShoppingSpree;

using ShoppingSpree.Models;

public class StartUp
{
    static void Main(string[] args)
    {
        List<Person> people = new List<Person>();
        List<Product> products = new List<Product>();

        try
        {
            string[] nameMoneyPairs = Console.ReadLine()
            .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var nameMoneyPair in nameMoneyPairs)
            {
                string[] nameMoney = nameMoneyPair
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);

                string name = nameMoney[0];
                decimal money = decimal.Parse(nameMoney[1]);

                Person person = new Person(name, money);

                people.Add(person);
            }

            string[] productCostPairs = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var productCostPair in productCostPairs)
            {
                string[] productCost = productCostPair
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);

                string productName = productCost[0];
                decimal cost = decimal.Parse(productCost[1]);

                Product product = new Product(productName, cost);

                products.Add(product);
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);

            return;
        }

        string command = string.Empty;

        while ((command = Console.ReadLine()) != "END")
        {
            string[] personProduct = command
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string personName = personProduct[0];
            string productName = personProduct[1];

            Person person = people.FirstOrDefault(p => p.Name == personName);

            Product product = products.FirstOrDefault(p => p.Name == productName);

            if (person is not null && product is not null)
            {
                Console.WriteLine(person.Add(product));
            }
        }

        foreach (var person in people)
        {
            Console.WriteLine(person);
        }
    }
}