string[] bankAccountsInfo = Console.ReadLine().Split(",");

Dictionary<int, double> bankAccounts = new Dictionary<int, double>();

for (int i = 0; i < bankAccountsInfo.Length; i++)
{
    string[] bankAccount = bankAccountsInfo[i].Split("-");
    int accountNumber = int.Parse(bankAccount[0]);
    double accountBalance = double.Parse(bankAccount[1]);

    bankAccounts.Add(accountNumber, accountBalance);
}

string command = string.Empty;

while ((command = Console.ReadLine()) != "End")
{
    string[] commandInfo = command.Split(" ");

    string commandType = commandInfo[0];
    int accountNumber = int.Parse(commandInfo[1]);
    double balance = double.Parse(commandInfo[2]);

    try
    {
        if (commandType != "Deposit" && commandType != "Withdraw")
        {
            throw new InvalidCommandException(InvalidCommandException.InvalidCommandExceptionMessage);
        }

        if (!bankAccounts.ContainsKey(accountNumber))
        {
            throw new InvalidAccountException(InvalidAccountException.InvalidAccountExceptionMessage);
        }

        if (commandType == "Deposit")
        {
            bankAccounts[accountNumber] += balance;
        }
        else if (commandType == "Withdraw")
        {
            if (bankAccounts[accountNumber] - balance < 0)
            {
                throw new InsufficientBalanceException(InsufficientBalanceException.InsufficientBalanceExceptionMessage);
            }
            bankAccounts[accountNumber] -= balance;
        }

        Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:f2}");
    }
    catch (InvalidCommandException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InvalidAccountException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InsufficientBalanceException ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.WriteLine("Enter another command");
    }
}

internal class InvalidCommandException : Exception
{
    public const string InvalidCommandExceptionMessage = "Invalid command!";

    public InvalidCommandException(string invalidCommandExceptionMessage) : base(invalidCommandExceptionMessage) { }
}

internal class InvalidAccountException : Exception
{
    public const string InvalidAccountExceptionMessage = "Invalid account!";

    public InvalidAccountException(string invalidAccountExceptionMessage) : base(invalidAccountExceptionMessage) { }
}

internal class InsufficientBalanceException : Exception
{
    public const string InsufficientBalanceExceptionMessage = "Insufficient balance!";

    public InsufficientBalanceException(string insufficientBalanceExceptionMessage) : base(insufficientBalanceExceptionMessage) { }
}