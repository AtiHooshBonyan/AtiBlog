using Article.MVC.Models.DomainModels;

namespace Article.MVC.Repositories.Contrcats
{
    public interface IRepository
    {
        Task<List<ArticlePost>> SelectAllAsync();
        Task<ArticlePost> SelectAsync(Guid id);
        Task<bool> InsertAsync(ArticlePost obj);
        Task<bool> UpdateAsync(ArticlePost obj);
        Task<bool> DeleteAsync(Guid id);

    }
}
