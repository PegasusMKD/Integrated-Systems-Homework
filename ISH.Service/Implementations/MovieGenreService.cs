using AutoMapper;
using ISH.Data;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos;

namespace ISH.Service.Implementations
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly IMovieGenreRepository _baseMovieGenreRepository;
        private readonly IMapper _mapper;


        public MovieGenreService(IMovieGenreRepository baseMovieGenreRepository, IMapper mapper)
        {
            _baseMovieGenreRepository = baseMovieGenreRepository;
            _mapper = mapper;
        }

        public List<MovieGenreDto> GetAll() =>
            _baseMovieGenreRepository.GetAll().Select(_mapper.Map<MovieGenreDto>).ToList();

        public MovieGenreDto Create(string genreName)
        {
            var genre = _baseMovieGenreRepository.Create(new MovieGenre
            {
                Name = genreName
            });
            _baseMovieGenreRepository.SaveChanges();
            return _mapper.Map<MovieGenreDto>(genre);
        }

        public MovieGenreDto Update(MovieGenreDto genre)
        {
            var movieGenre = _baseMovieGenreRepository.Update(_mapper.Map<MovieGenre>(genre));
            _baseMovieGenreRepository.SaveChanges();
            return _mapper.Map<MovieGenreDto>(movieGenre);
        }

        public MovieGenreDto GetByName(string viewSlotGenreName) =>
            _mapper.Map<MovieGenreDto>(_baseMovieGenreRepository.GetByName(viewSlotGenreName));
    }
}
