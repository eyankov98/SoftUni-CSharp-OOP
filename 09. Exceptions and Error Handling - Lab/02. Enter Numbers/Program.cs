List<int> numbers = new List<int>();

while (numbers.Count != 10)
{

    try
    {
        int number = int.Parse(Console.ReadLine());

        if (number <= 1 || number >= 100)
        {
            int lastNumber = numbers.LastOrDefault() == 0
                 ? 1
                 : numbers.LastOrDefault();

            throw new ArgumentOutOfRangeException("", $"Your number is not in range {lastNumber} - 100!");
        }
        else if (numbers.Count == 0)
        {
            numbers.Add(number);
        }
        else if (numbers.LastOrDefault() < number)
        {
            numbers.Add(number);
        }
        else
        {
            throw new ArgumentOutOfRangeException("", $"Your number is not in range {numbers.LastOrDefault()} - 100!");
        }

    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid Number!");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine(string.Join(", ", numbers));