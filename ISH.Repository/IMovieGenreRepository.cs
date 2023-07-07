using ISH.Data;

namespace ISH.Repository
{
    public interface IMovieGenreRepository
    {
        MovieGenre GetById(int id);
        List<MovieGenre> GetAll();
        MovieGenre Create(MovieGenre movie);
        MovieGenre Update(MovieGenre movie);
        void SaveChanges();
        MovieGenre GetByName(string viewSlotGenreName);
    }
}
