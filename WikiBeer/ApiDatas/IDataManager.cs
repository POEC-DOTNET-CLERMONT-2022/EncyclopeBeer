namespace Ipme.WikiBeer.ApiDatas
{
    public interface IDataManager<TModel, TDto>
        where TModel : class
        where TDto : class
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(Guid id);
        Task Add(TModel model);
        Task Update(Guid id, TModel model);
        Task<bool> DeleteById(Guid id);
    }
}