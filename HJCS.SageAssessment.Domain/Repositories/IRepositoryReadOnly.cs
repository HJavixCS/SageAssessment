using System.Collections.Generic;

namespace HJCS.SageAssessment.Domain.Repositories
{
    public interface IRepositoryReadOnly<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAll();
        TModel FindById(long id);
    }
}
