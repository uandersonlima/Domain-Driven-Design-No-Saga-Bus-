namespace AscoreStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}