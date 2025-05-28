using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Article.MVC.Models.DomainModels;
using Article.MVC.Repositories.Contrcats;

namespace Article.MVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticlePostsRepository _articleRepo;

        #region [- Ctor -]
        public ArticleController(IArticlePostsRepository articlePostsRepository)
        {
            _articleRepo = articlePostsRepository;
        }
        #endregion

        #region [- Index -]
        // GET: Article
        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepo.SelectAllAsync();
            return View(articles);
        }
        #endregion

        #region [- Details -]
        // GET: Article/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var article = await _articleRepo.SelectAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        #endregion

        #region [- Create -]
        // GET: Article/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ImagePath,VideoUrl,PublishedAt,UpdatedAt")] ArticlePost model)
        {
            if (ModelState.IsValid)
            {
                await _articleRepo.InsertAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region [- Edit -]
        // GET: Article/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await _articleRepo.SelectAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Content,ImagePath,VideoUrl,PublishedAt,UpdatedAt")] ArticlePost model)
        {

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _articleRepo.UpdateAsync(model);
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }

            return View(model);

        }
        #endregion

        #region [- Delete -]
        // GET: Article/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = await _articleRepo.SelectAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _articleRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}
