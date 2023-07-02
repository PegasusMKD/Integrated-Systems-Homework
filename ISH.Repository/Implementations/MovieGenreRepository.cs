using ISH.Data;

namespace ISH.Repository.Implementations
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly ApplicationContext _context;

        public MovieGenreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public MovieGenre GetById(int id) =>
            _context.movieGenre.SingleOrDefault(genre => genre.Id == id)!;
    }
}
