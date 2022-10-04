//namespace DDDAccountBalance.Testing
//{
//    using DDDAccountBalance.Logic.Commands;
//    using Logic.Classes;
//    using Logic.Interfaces;
//    using UnitTesting.Mocks;
//    public class UnitTests
//    {

//        [Fact]
//        public async Task account_can_be_created_with_id_and_name()
//        {
//            var repo = new MockedAccountRepository();
//            var handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            var CreateAccount = new CreateAccount(id, "Random Name");

//            var response = await handler.Handle(CreateAccount);
//            Assert.Equal(CommandResponse.Accepted, response);
//        }

//        [Fact]
//        public async Task money_can_be_deposited_after_creating_the_account()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(createAccountCommand);
//            DepositFund depositCommand = new DepositFund(id, 120);
//            CommandResponse depositresponse = handler.Handle(depositCommand).Result;
//            Assert.Equal(CommandResponse.Accepted, depositresponse);
//        }

//        [Fact]
//        public async Task cant_set_a_negative_daily_transferlimit()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount CreateAccount = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(CreateAccount);
//            SetDailyTransferLimit setNegativeDailyTransferLimit = new SetDailyTransferLimit(id, -10);
//            CommandResponse setNegativeDailyTransferLimitResponse =
//                await handler.Handle(setNegativeDailyTransferLimit);
//            Assert.Equal(CommandResponse.Rejected, setNegativeDailyTransferLimitResponse);
//        }


//        [Fact]
//        public async Task cant_deposit_a_negative_value_cheque()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount CreateAccount = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(CreateAccount);
//            DepositCheque depositChequeCommand = new DepositCheque(id, -10);
//            CommandResponse depositChequeResponse = handler.Handle(depositChequeCommand).Result;
//            Assert.Equal(CommandResponse.Rejected, depositChequeResponse);
//        }

//        [Fact]
//        public async Task cant_withdraw_a_negative_amount()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount CreateAccount = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(CreateAccount);
//            WithdrawFund withdrawCommand = new WithdrawFund(id, -10);
//            CommandResponse withdrawResponse = handler.Handle(withdrawCommand).Result;
//            Assert.Equal(CommandResponse.Rejected, withdrawResponse);
//        }

//        [Fact]
//        public async Task cant_set_a_negative_overdraft_limit()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount CreateAccount = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(CreateAccount);
//            SetOverdraftLimit setNegativeOverdraftLimitCommand = new SetOverdraftLimit(id, -5);
//            CommandResponse setNegativeDailyTransferLimitResponse =
//                await handler.Handle(setNegativeOverdraftLimitCommand);
//            Assert.Equal(CommandResponse.Rejected, setNegativeDailyTransferLimitResponse);
//        }

//        [Fact]
//        public async Task cant_deposit_a_negative_amount()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount CreateAccount = new CreateAccount(id, "Random Name");
//            CommandResponse createaccountResponse = await handler.Handle(CreateAccount);
//            DepositFund depositCommand = new DepositFund(id, -10);
//            CommandResponse depositresponse = handler.Handle(depositCommand).Result;
//            Assert.Equal(CommandResponse.Rejected, depositresponse);
//        }


//        [Fact]
//        public async Task cant_withdraw_more_than_your_balance_and_the_overdraftlimit()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();

//            CreateAccount c = new CreateAccount(id, "Random Name");
//            DepositFund depositfundCommand = new DepositFund(id, 100);
//            WithdrawFund withdrawCommand = new WithdrawFund(id, 151);
//            SetOverdraftLimit setOverdraftLimitCommand = new SetOverdraftLimit(id, 50)
//            ;

//            CommandResponse creatingAccountResponse = await handler.Handle(c);
//            CommandResponse setoverdraftLimitResponse = await handler.Handle(setOverdraftLimitCommand);
//            CommandResponse depositfundResponse = await handler.Handle(depositfundCommand);
//            CommandResponse witdhrawfundresponse = await handler.Handle(withdrawCommand);

//            Assert.Equal(CommandResponse.Rejected, witdhrawfundresponse);
//        }


//        [Fact]
//        public async Task cant_withdraw_from_a_blocked_account()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();

//            CreateAccount c = new CreateAccount(id, "Random Name");
//            DepositFund depositfundCommand = new DepositFund(id, 100);
//            WithdrawFund withdrawCommand = new WithdrawFund(id, 150);
//            WithdrawFund secondWithdawFundCommand = new WithdrawFund(id, 90);

//            CommandResponse createAccountResponse = await handler.Handle(c);
//            CommandResponse depositfundResponse = await handler.Handle(depositfundCommand);
//            CommandResponse withdrawfundresponse = await handler.Handle(withdrawCommand);
//            CommandResponse secondWithdrawfundResponse = await handler.Handle(secondWithdawFundCommand);

//            Assert.Equal(CommandResponse.Rejected, secondWithdrawfundResponse);
//        }

//        [Fact]
//        public async Task daily_transfer_limit_has_to_be_respected()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");
//            DepositFund depositfundCommand = new DepositFund(id, 100);

//            SetDailyTransferLimit setDailyTransferLimitCommand = new SetDailyTransferLimit(id, 100);

//            TransferFund firstTransferFundCommand = new TransferFund(id ,90);
//            TransferFund secondTransferFundCommand = new TransferFund(id, 90);

//            CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//            CommandResponse depositfundResponse = await handler.Handle(depositfundCommand);
//            CommandResponse setDailyTransferLimitResponse = await handler.Handle(setDailyTransferLimitCommand);
//            CommandResponse firstTransferFundResponse = await handler.Handle(firstTransferFundCommand);
//            CommandResponse secondTransferFundResponse = await handler.Handle(secondTransferFundCommand);

//            Assert.Equal(CommandResponse.Rejected, secondTransferFundResponse);
//        }


//        [Fact]
//        public async Task account_should_blocked_after_a_rejected_withdraw()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();

//            CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");
//            DepositFund depositFundCommand = new DepositFund(id, 60);
//            SetOverdraftLimit overdraftLimitCommand = new SetOverdraftLimit(id, 50);

//            WithdrawFund bigwithdrawFundCommand = new WithdrawFund(id, 120);
//            WithdrawFund smallWithdrawFundCommand = new WithdrawFund(id, 20);


//            CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//            CommandResponse depositFundResponse = await handler.Handle(depositFundCommand);
//            CommandResponse setOverdraftLimitResponse = await handler.Handle(overdraftLimitCommand);
//            CommandResponse firstwitdhrawFundResponse = await handler.Handle(bigwithdrawFundCommand);
//            CommandResponse secondwithdrawFundResponse = await handler.Handle(smallWithdrawFundCommand);

//            Assert.Equal(CommandResponse.Rejected, secondwithdrawFundResponse);
//        }

//        [Fact]
//        public async Task account_should_be_unblocked_after_a_deposit()
//        {
//            IRepository<Account> repo = new MockedAccountRepository();
//            ICommandHandler handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();

//            CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");
//            DepositFund depositFundCommand = new DepositFund(id, 60);
//            SetOverdraftLimit overdraftLimitCommand = new SetOverdraftLimit(id, 50);

//            WithdrawFund bigwithdrawFundCommand = new WithdrawFund(id, 120);
//            DepositFund secondDepositFundCommand = new DepositFund(id, 10);
//            WithdrawFund smallWithdrawFundCommand = new WithdrawFund(id, 20)
//            {
//                AccountId = id,
//                Amount = 20
//            };

//            CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//            CommandResponse depositFundResponse = await handler.Handle(depositFundCommand);
//            CommandResponse setOverdraftLimitResponse = await handler.Handle(overdraftLimitCommand);
//            CommandResponse firstwitdhrawFundResponse = await handler.Handle(bigwithdrawFundCommand);
//            CommandResponse SeconddepositFundResponse = await handler.Handle(secondDepositFundCommand);
//            CommandResponse secondwithdrawFundResponse = await handler.Handle(smallWithdrawFundCommand);

//            Assert.Equal(CommandResponse.Accepted, secondwithdrawFundResponse);
//        }


//        [Fact]
//        public async Task fund_deposited_from_a_cheque_are_not_available_immediately()
//        {
//            var repo = new MockedAccountRepository();
//            var handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");

//            DepositCheque depositChequeCommand = new DepositCheque(id, 25);
//            WithdrawFund withdrawFundCommand = new WithdrawFund(id, 25);

//            CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//            CommandResponse depositChequeResponse = await handler.Handle(depositChequeCommand);
//            CommandResponse withdrawFundResponse = await handler.Handle(withdrawFundCommand);
//            Assert.Equal(CommandResponse.Rejected, withdrawFundResponse);
//        }

//        //        //[Fact]
//        //        //public async Task fund_deposited_from_a_cheque_available_after_next_business_day()
//        //        //{
//        //        //    var repo = new MockedAccountRepository();
//        //        //    var handler = new CommandHandler(repo);
//        //        //    var id = Guid.NewGuid();
//        //        //    CreateAccount createAccountCommand = new CreateAccount(id, "Random Name");

//        //        //    DepositCheque depositChequeCommand = new DepositCheque(id, 25);
//        //        //    WithdrawFund withdrawFundCommand = new WithdrawFund(id, 20);
//        //        //    var startBusinessDayCommand = new StartBusinessDay();



//        //        //    CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//        //        //    CommandResponse depositChequeResponse = await handler.Handle(depositChequeCommand);
//        //        //    CommandResponse startBusinessDayResponse = await handler.Handle(startBusinessDayCommand);
//        //        //    CommandResponse withdrawFundResponse = await handler.Handle(withdrawFundCommand);
//        //        //    Assert.Equal(CommandResponse.Accepted, withdrawFundResponse);
//        //        //}

//        [Fact]
//        public async Task transfer_money_command_accepted_with_respected_values()
//        {
//            var repo = new MockedAccountRepository();
//            var handler = new CommandHandler(repo);
//            var id = Guid.NewGuid();
//            var createAccountCommand = new CreateAccount(id, "Random Name");

//            var depositFundCommand = new DepositFund(id, 100);
//            var setDailyTransferLimitCommand = new SetDailyTransferLimit(id, 90);
//            var transferFundCommand = new TransferFund(id, 50);

//            CommandResponse createAccountResponse = await handler.Handle(createAccountCommand);
//            CommandResponse depositFundResponse = await handler.Handle(depositFundCommand);
//            CommandResponse setDailyTransferLimitResponse = await handler.Handle(setDailyTransferLimitCommand);
//            CommandResponse transferFundResponse = await handler.Handle(transferFundCommand);

//            Assert.Equal(CommandResponse.Accepted, transferFundResponse);
//        }


    
//    }
//}
