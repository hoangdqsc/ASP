namespace Application_Temp.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooks();
        Task<BookDto> GetBookById(int id);
        Task AddBook(BookDto bookDto);
        Task UpdateBook(BookDto bookDto);
        Task DeleteBook(int id);
    }
}
