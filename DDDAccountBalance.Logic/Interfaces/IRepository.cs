

using DDDAccountBalance.Logic.Classes;

namespace DDDAccountBalance.Logic.Interfaces
{
    public interface IRepository<T>
    {
        public Task Save(Guid accountId, Account account);
        public Task<T> GetById(Guid id);
    }
}

