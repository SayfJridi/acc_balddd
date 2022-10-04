namespace DDDAccountBalance.Logic.Classes
{
    using Commands;
    using DDDAccountBalance.Logic.Exceptions;
    using Interfaces;
    using NodaTime;

    public class CommandHandler : ICommandHandler
    {
        private IClock _clock;
        private IRepository<Account> _accountRepository;
        public CommandHandler(IRepository<Account> accountRepository, IClock clock)
        {
            _accountRepository = accountRepository;
            _clock = clock;
        }

        public async Task<CommandResponse> Handle(CreateAccount command)
        {
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            var account = new Account(command.AccountId, command.AccountName);
            await _accountRepository.Save(command.AccountId, account);
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(SetOverdraftLimit command)
        {
            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }
            var result = account.SetOverdraftLimit(command.OverdraftLimit);
            await _accountRepository.Save(command.AccountId, account);
            if (!result)
            {
                return CommandResponse.Rejected;
            }
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(SetDailyTransferLimit command)
        {
            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }
            var result = account.SetDailyTransferLimit(command.DailyTransferLimit);
            await _accountRepository.Save(command.AccountId, account);

            if (!result)
            {

                return CommandResponse.Rejected;
            }
            ;
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(DepositCheque command)
        {
            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }

            var result = account.DepositCheque(command.Value);
            await _accountRepository.Save(command.AccountId, account);
            if (!result)
            {
                return CommandResponse.Accepted;
            }
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(WithdrawFund command)
        {
            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }
            var result = account.WithdrawFund(command.Amount);
            Console.WriteLine(result);
            await _accountRepository.Save(command.AccountId, account);
            if (!result)
            {
                return CommandResponse.Rejected;
            }
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(TransferFund command)
        {

            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }
            var result = account.WireTransfer(command.Amount);
            await _accountRepository.Save(command.AccountId, account);
            Console.WriteLine(result);
            if (!result)
            {
                return CommandResponse.Rejected;
            }
            return CommandResponse.Accepted;
        }

        public async Task<CommandResponse> Handle(DepositFund command)
        {
            Account account;
            if (!command.IsValid())
            {
                return CommandResponse.Rejected;
            }
            try
            {
                account = await _accountRepository.GetById(command.AccountId);
            }
            catch (AccountNotFoundException e)
            {
                return CommandResponse.Rejected;
            }
            var result = account.DepositFund(command.Amount);
            await _accountRepository.Save(command.AccountId, account);
            if (!result)
            {
                return CommandResponse.Rejected;
            }
            return CommandResponse.Accepted;
        }
    }
}
