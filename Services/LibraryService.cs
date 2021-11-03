using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meditec.Models;
using Microsoft.EntityFrameworkCore;

namespace Meditec.Services
{
    public class LibraryService : DbService
    {
        public LibraryService(LibraryDbContext context) : base(context)
        {
        }

        public int StartMenu()
        {
            Console.WriteLine("Choose the next action by pressing the corresponding number:");
            Console.WriteLine("1 Add book");
            Console.WriteLine("2 Add author");
            Console.WriteLine("3 Display all books titles");
            Console.WriteLine("4 Display all authors");
            Console.WriteLine("5 Display all books and their authors");
            Console.WriteLine("6 Exit application");

            int input = int.Parse(Console.ReadLine());

            return input;
        }

        public void DisplayAuthors(DbSet<Author> authors)
        {
            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);
            }
        }

        public void DisplayBooks(DbSet<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine("Title:" + book.Title);
            }
        }

        public void DisplayTitlesAndAuthors(LibraryDbContext context)
        {
            foreach (var book in context.Books)
            {
                Console.WriteLine("Title:" + book.Title + "written by: " + book.Author.FirstName + " " + book.Author.LastName);
            }
        }

        public void ClearTables<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
