namespace Ipme.WikiBeer.ApiDatas
{
    public interface IDataManager<TModel, TDto>
        where TModel : class
        where TDto : class
    {

        Task Add(TModel model);
        Task<bool> DeleteById(Guid id);
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(Guid id);
        Task Update(Guid id, TModel model);


    }
}