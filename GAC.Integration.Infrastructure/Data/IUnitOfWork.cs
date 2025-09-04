namespace GAC.Integration.Infrastructure.Data
{
        public interface IUnitOfWork
        {
            int Commit();
            Task<int> CommitAsync();
        }
}