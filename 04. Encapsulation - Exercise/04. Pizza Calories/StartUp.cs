namespace PizzaCalories;

using PizzaCalories.Models;

public class StartUp
{
    static void Main(string[] args)
    {
        try
        {
            string[] pizzaInputInfo = Console.ReadLine()
            .Split(" ");

            string pizzaName = pizzaInputInfo[1];

            string[] doughInputInfo = Console.ReadLine()
                .Split(" ");

            string flourType = doughInputInfo[1];
            string bakingTechnique = doughInputInfo[2];
            double weightDough = double.Parse(doughInputInfo[3]);

            Dough dough = new Dough(weightDough, flourType, bakingTechnique);

            Pizza pizza = new Pizza(pizzaName, dough);

            string toppingsInputInfo = string.Empty;

            while ((toppingsInputInfo = Console.ReadLine()) != "END")
            {
                string[] toppingsInfo = toppingsInputInfo
                    .Split(" ");

                string toppingName = toppingsInfo[1];
                double weightTopping = double.Parse(toppingsInfo[2]);

                Topping topping = new Topping(toppingName, weightTopping);

                pizza.AddTopping(topping);
            }

            Console.WriteLine(pizza);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}