using System;
using System.IO;
using Meditec.Models;
using Meditec.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Meditec
{
   public class Program : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("Storage");

            DbContextOptionsBuilder<LibraryDbContext> optionsBuilder =
                new DbContextOptionsBuilder<LibraryDbContext>().UseSqlServer
                    ("Server=.;Database=Meditec;Trusted_Connection=True;MultipleActiveResultSets=true"); //MSI\\MSSQLSERVER01


            return new LibraryDbContext(optionsBuilder.Options);
        }

        private static readonly LibraryService _siteService;
        private static readonly LibraryDbContext _context;

        public static void Main(string[] args)
        {
            Program p = new Program();

            using (LibraryDbContext dbContext = p.CreateDbContext(null))
            {
                dbContext.Database.Migrate();
                dbContext.SaveChanges();

                LibraryService _siteService = new LibraryService(dbContext);

                bool shouldContinue = true;

                _siteService.ClearTables<Book>();
                _siteService.ClearTables<Author>();

                while (shouldContinue)
                {
                    var input = _siteService.StartMenu();

                    switch (input)
                    {
                        case 1:
                            Book newBook = new Book();
                            Author newAuthor = new Author();
                            Console.WriteLine("Enter the title of the book");
                            var title = Console.ReadLine();
                            Console.WriteLine("Enter the first and last name of author of the book");
                            var authorName = Console.ReadLine();
                            newBook.Title = title;
                            newAuthor.FirstName = authorName.Split(' ')[0];
                            newAuthor.LastName = authorName.Split(' ')[1];
                            newBook.Author = newAuthor;
                            _siteService.Create(newBook);
                            Console.Clear();
                            break;

                        case 2:
                            Author singleAuthor = new Author();
                            Console.WriteLine("Enter the first and last name of author");
                            var singleAuthorName = Console.ReadLine();
                            singleAuthor.FirstName = singleAuthorName.Split(' ')[0];
                            singleAuthor.LastName = singleAuthorName.Split(' ')[1];
                            _siteService.Create(singleAuthor);
                            Console.Clear();
                            break;
                        case 3:
                            _siteService.DisplayBooks(dbContext.Books);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            _siteService.DisplayAuthors(dbContext.Authors);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            _siteService.DisplayTitlesAndAuthors(dbContext);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            Console.WriteLine("Closing");
                            shouldContinue = false;
                            break;
                    }
                }
            }
        }
    }
}
