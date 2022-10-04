namespace DDDAccountBalance.Logic.Classes
{
    using Commands;
    using Exceptions;
    public class Utility
    {
        public static DateTime NextBusinessDay()
        {
            var date = DateTime.Now;
            do
            {
                date = date.AddDays(1.0);
            } while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
            date = new DateTime(date.Year, date.Month, date.Day, 9, 00, 00);
            return date;
        }
        public static void WriteTheOptions()
        {
            Console.WriteLine("1 - Create a New Account");
            Console.WriteLine("2 - Set The OverdraftLimit");
            Console.WriteLine("3 - Set The DailyTransferLimit");
            Console.WriteLine("4 - Deposit Fund");
            Console.WriteLine("5 - Withdraw Fund");
            Console.WriteLine("6 - Deposit a Cheque");
            Console.WriteLine("7 - Send Fund");
        }

        public static CreateAccount PrepareCreateAccount()
        {
            var id = Guid.NewGuid();
            Console.WriteLine("Enter Your  Name Please");
            var name = Console.ReadLine()!;
            return new CreateAccount(id,name); 
        }

        public static DepositFund PrepareDepositFund()
        {
            Console.WriteLine("Enter Your Account Id Please");
            var id = Console.ReadLine();
            Console.WriteLine("Enter The Amount You Want to Deposit");
            var amount = Convert.ToDecimal(Console.ReadLine());
            if (amount <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }
            Console.Clear();
            var _id = new Guid(id);
            return new DepositFund(new Guid(id),amount); 
        }

        public static WithdrawFund PrepareWithdrawFund()
        {
            Console.WriteLine("Enter Your Account Id Please");
            var id = Console.ReadLine();
            Console.WriteLine("Enter The Amount You Want to withdraw");

            var amount = Convert.ToDecimal(Console.ReadLine());
            if (amount <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }

            Console.Clear();
            return new WithdrawFund(new Guid(id), amount);
        }

        public static DepositCheque PrepareDepositCheque()
        {
            Console.WriteLine("Enter Your Account's Id Please");
            var id = Console.ReadLine();
            Console.WriteLine("Enter The Value of the cheque You Want to deposit");
            var amount = Convert.ToDecimal(Console.ReadLine());
            if (amount <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }
            Console.Clear();
            return new DepositCheque(new Guid(id), amount);

        }

        public static TransferFund PrepareSendFund()
        {

            Console.WriteLine("Enter Your Account's Id Please");
            var id = Console.ReadLine()!;
       
            Console.WriteLine("How Much Money are going to send");
            var amount = Convert.ToDecimal(Console.ReadLine()!);
            if (amount <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }
            Console.Clear();
            return new TransferFund(new Guid(id),  amount);

        }

        public static SetDailyTransferLimit PrepareSetDailyTransferLimit()
        {
            Console.WriteLine("Enter Your Account's Id Please");
            var id = Console.ReadLine();
            Console.WriteLine("Can you enter the new Value Please");
            var dailytransferlimit = Convert.ToDecimal(Console.ReadLine());
            if (dailytransferlimit <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }
            Console.Clear();
            return new SetDailyTransferLimit(new Guid(id), dailytransferlimit);
        }

        public static SetOverdraftLimit PrepareSetOverdraftLimit()
        {
            Console.WriteLine("Enter Your Account's Id Please");
            var id = Console.ReadLine();
            Console.WriteLine("Can you enter the new Value Please");
            var overDraftLimit = Convert.ToDecimal(Console.ReadLine());
            if (overDraftLimit <= 0)
            {
                throw new NegativeAmountException("Negative amount are not accepted");
            }
            Console.Clear();
            return new SetOverdraftLimit(new Guid(id), overDraftLimit);
        }

    }
}
