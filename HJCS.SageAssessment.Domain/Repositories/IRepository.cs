namespace HJCS.SageAssessment.Domain.Repositories
{
    public interface IRepository<TModel> : IRepositoryReadOnly<TModel>
        where TModel : class
    {
        void Add(TModel item);
        void Remove(long id);
        void Update(TModel item);
    }
}
