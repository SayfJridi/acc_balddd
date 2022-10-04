using DDDAccountBalance.Logic.Commands;

namespace DDDAccountBalance.Logic.Interfaces
{
    public interface ICommandHandler
    {

        public Task<CommandResponse> Handle(WithdrawFund command);
        public Task<CommandResponse> Handle(DepositFund command);
        public Task<CommandResponse> Handle(TransferFund command);
        public Task<CommandResponse> Handle(DepositCheque command);
        public Task<CommandResponse> Handle(SetDailyTransferLimit command);
        public Task<CommandResponse> Handle(SetOverdraftLimit command);
        public Task<CommandResponse> Handle(CreateAccount command);

    }
}
