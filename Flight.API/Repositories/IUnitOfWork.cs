namespace Flight.API.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
