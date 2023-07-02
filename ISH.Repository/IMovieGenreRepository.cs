using ISH.Data;

namespace ISH.Repository
{
    public interface IMovieGenreRepository
    {
        MovieGenre GetById(int id);
    }
}
