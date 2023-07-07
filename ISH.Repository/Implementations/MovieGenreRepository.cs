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

        public List<MovieGenre> GetAll() =>
        _context.movieGenre.ToList();

        public MovieGenre Create(MovieGenre movie) =>
            _context.movieGenre.Add(movie).Entity;

        public MovieGenre Update(MovieGenre movie) =>
            _context.movieGenre.Update(movie).Entity;

        public void SaveChanges() =>
            _context.SaveChanges();

        public MovieGenre GetByName(string viewSlotGenreName) =>
            _context.movieGenre.SingleOrDefault(genre => genre.Name == viewSlotGenreName);
    }
}
