using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;

namespace Infrastructure_Temp.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
