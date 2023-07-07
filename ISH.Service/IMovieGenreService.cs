using ISH.Service.Dtos;

namespace ISH.Service
{
    public interface IMovieGenreService
    {
        List<MovieGenreDto> GetAll();
        MovieGenreDto Create(string genreName);
        MovieGenreDto Update(MovieGenreDto genre);
        MovieGenreDto GetByName(string viewSlotGenreName);
    }
}
