using Application_Temp.DTOs;
using Application_Temp.Interfaces;
using Core_Temp.Entities;
using Core_Temp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application_Temp.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year
                });
            }

            return bookDtos;
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year
            };
        }

        public async Task AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Year = bookDto.Year
            };

            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBook(BookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookDto.Id);
            if (book == null)
                return;

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Year = bookDto.Year;

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
