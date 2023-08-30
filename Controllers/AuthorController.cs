using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web1.Data;
using web1.Models;

namespace web1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public List<Author> PostAuthor([FromBody] Author author)
        {
            _context.ContactDatas.Add(author.Contact);
            _context.SaveChanges();

            author.ContactDataId = author.Contact.Id;

            _context.Authors.Add(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }
        [HttpGet]
        public List<Author> GetAuthors()
        {
            var authors = _context.Authors.Include(a => a.Contact).ToList();
            return authors;
        }
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }
        [HttpDelete("{id}")]
        public List<Author> DeleteAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return _context.Authors.Include(a => a.Contact).ToList();
            }

            if (author.Contact != null)
            {
                _context.ContactDatas.Remove(author.Contact);
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }
    }
}
