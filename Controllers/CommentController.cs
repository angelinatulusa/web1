using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web1.Data;
using web1.Models;

namespace web1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Comment> GetComments()
        {
            var comments = _context.Comments.ToList();
            return comments;
        }

        /*[HttpPost]
        public List<Comment> PostComment([FromBody] Comment comment)
        {
            comment.Date = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return _context.Comments.ToList();
        }*/
        [HttpPost] // доюавление комментария
        public ActionResult<List<Comment>> PostComment([FromBody] Comment comment)
        {
            comment.Date = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            var article = _context.Articles.Include(a => a.Comments).SingleOrDefault(a => a.Id == comment.ArticleId);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article.Comments);
        }
        [HttpDelete("{id}")]
        public List<Comment> DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return _context.Comments.ToList();
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return _context.Comments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }
        [HttpGet("article/{articleId}")]
        public ActionResult<List<Comment>> GetCommentsForArticle(int articleId)
        {
            var comments = _context.Comments.Where(c => c.ArticleId == articleId).ToList();

            if (comments.Count == 0)
            {
                return NotFound();
            }

            return comments;
        }
        [HttpPut("{id}")]
        public ActionResult<List<Comment>> PutComment(int id, [FromBody] Comment updatedComment)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = updatedComment.Content;

            _context.Comments.Update(comment);
            _context.SaveChanges();

            return Ok(_context.Comments);
        }

    }
}
