using DDDAccountBalance.Logic.Classes;
using DDDAccountBalance.Logic.Commands;
using DDDAccountBalance.Logic.Persistance;
using NodaTime;

public class Program
{

    public static void Main()
    {
        var repo = new AccountRepository(new EventStoreService());
        Console.WriteLine($"Next Businesss Day is {Utility.NextBusinessDay().ToString()}");
        SystemClock clock = SystemClock.Instance; 
        var handler = new CommandHandler(repo,clock);
        CommandResponse response;
        ICommand c;
        while (true)
        {
            try
            {
                Utility.WriteTheOptions();
                var choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        c = Utility.PrepareCreateAccount();
                        response =  handler.Handle((CreateAccount)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "2":
                        c = Utility.PrepareSetOverdraftLimit();
                        response =  handler.Handle((SetOverdraftLimit)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "3":
                        c = Utility.PrepareSetDailyTransferLimit();
                        response =  handler.Handle((SetDailyTransferLimit)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "4":
                        c = Utility.PrepareDepositFund();
                        response =  handler.Handle((DepositFund)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "5":
                        c = Utility.PrepareWithdrawFund();
                        response =  handler.Handle((WithdrawFund)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "6":
                        c = Utility.PrepareDepositCheque();
                        response =  handler.Handle((DepositCheque)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                    case "7":
                        c = Utility.PrepareSendFund();
                        response =  handler.Handle((TransferFund)c).Result;
                        Console.WriteLine($"Command : {response.ToString()}");
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        Console.ReadKey();
    }
}