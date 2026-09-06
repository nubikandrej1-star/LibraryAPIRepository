using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .Property(b => b.PurchasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Book>().Property(b => b.Genre).HasConversion<string>();

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Джордж", LastName = "Орвелл", BirthYear = 1903 },
                new Author { Id = 2, FirstName = "Ф. Скотт", LastName = "Фіцджеральд", BirthYear = 1896 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    AuthorId = 1,
                    Genre = BookGenre.Dystopia,
                    PublishedYear = 1949,
                    PurchasePrice = 150.00m,
                    InternalNotes = "Супер популярна книга, замовляти більше привезень."
                },
                new Book
                {
                    Id = 2,
                    Title = "Великий Гетсбі",
                    AuthorId = 2,
                    Genre = BookGenre.Dystopia,
                    PublishedYear = 1932,
                    PurchasePrice = 150.00m,
                    InternalNotes = "Має пошкодження обкладинки на складі №2."
                }
            );
        }

        /*
            AddBook(new Book
            {
                Title = "1984",
                Author = new Author
                {
                    FirstName = "Джордж",
                    LastName = "Орвелл",
                    BirthYear = 1903
                },
                Genre = BookGenre.Dystopia,
                PublishedYear = 1949,
                PurchasePrice = 150.00m,
                InternalNotes = "Супер популярна книга, замовляти більше привезень."
            });
            AddBook(new Book
            {
                Title = "Великий Гетсбі",
                Author = new Author
                {
                    FirstName = "Ф. Скотт",
                    LastName = "Фіцджеральд",
                    BirthYear = 1896
                },
                Genre = BookGenre.Classics,
                PublishedYear = 1925,
                PurchasePrice = 210.50m,
                InternalNotes = "Має пошкодження обкладинки на складі №2."
            });
            */
    }
}