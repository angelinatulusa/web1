using Microsoft.AspNetCore.Mvc;
using web1.Data;
using web1.Models;
//https://taltech-coding.gitlab.io/prog2/orm/orm1-09_kasutamine/c/3_make_controller.html
namespace web1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] //вывод на экран
        public List<Article> GetArticles()
        {
            var articles = _context.Articles.ToList();
            return articles;
        }
        [HttpPost] //добавление
        public List<Article> PostArtikkel([FromBody] Article artikkel)
        {
            _context.Articles.Add(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<Article> DeleteArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return _context.Articles.ToList();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }
        [HttpDelete("/kustuta2/{id}")] //второй способ удаления
        public IActionResult DeleteArtikkel2(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<Article> GetArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            return artikkel;
        }
        [HttpPut("{id}")]//изменение (обязательно брать и ид и запись)
        public ActionResult<List<Article>> PutArtikkel(int id, [FromBody] Article updatedArtikkel)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            artikkel.Header = updatedArtikkel.Header;
            artikkel.Content = updatedArtikkel.Content;

            _context.Articles.Update(artikkel);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }
    }
}
