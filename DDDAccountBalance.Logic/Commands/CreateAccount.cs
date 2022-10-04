namespace DDDAccountBalance.Logic.Commands
{
    public class CreateAccount : ICommand
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }

        public CreateAccount(Guid accountId, string accountName)
        {
            AccountId=accountId;
            AccountName=accountName;
        }

        public bool IsValid()
        {
            return AccountName.All((char c) => Char.IsLetter(c) || c == ' ');
        }
    }
}
