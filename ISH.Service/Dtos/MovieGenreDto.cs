namespace ISH.Service.Dtos
{
    public class MovieGenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
