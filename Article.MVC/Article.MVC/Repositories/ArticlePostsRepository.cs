using Microsoft.EntityFrameworkCore;
using Article.MVC.Models.DomainModels;
using Article.MVC.Models;
using Article.MVC.Repositories.Contrcats;

namespace Article.MVC.Repositories
{
    public class ArticlePostsRepository : IArticlePostsRepository
    {
        private readonly ProjectDbContext _dbContext;

        #region [- Ctor -]
        public ArticlePostsRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        #endregion

        #region [- InsertAsync -]
        public async Task<bool> InsertAsync(ArticlePost model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                model.PublishedAt = DateTime.Now;
                _dbContext.articlePosts.Add(model);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        } 
        #endregion

        #region [- SelectAllAsync -]
        public async Task<List<ArticlePost>> SelectAllAsync()
        {
            try
            {
                return await _dbContext.articlePosts.ToListAsync();
            }
            catch
            {
                throw;
            }
        } 
        #endregion

        #region [- SelectAsync -]
        public async Task<ArticlePost> SelectAsync(Guid id)
        {
            try
            {
                var article = await _dbContext.articlePosts.FirstOrDefaultAsync(m => m.Id == id);
                if (article == null)
                {
                    return null;
                }
                return article;
            }
            catch
            {
                throw;
            }
        } 
        #endregion

        #region [- UpdateAsync -]
        public async Task<bool> UpdateAsync(ArticlePost model)
        {
            try
            {
                var existing = await _dbContext.articlePosts.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (existing == null)
                    return false;

                existing.Title = model.Title;
                existing.Content = model.Content;
                existing.ImagePath = model.ImagePath;
                existing.VideoUrl = model.VideoUrl;
                existing.PublishedAt = model.PublishedAt;
                existing.UpdatedAt = DateTime.Now;

                _dbContext.articlePosts.Update(existing);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        } 
        #endregion

        #region [- DeleteAsync -]
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var existing = await _dbContext.articlePosts.FirstOrDefaultAsync(x => x.Id == id);
                if (existing == null)
                    return false;

                _dbContext.articlePosts.Remove(existing);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        } 
        #endregion
    }
}
