namespace MovieStore.Entities
{
    public class MovieCast
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CastId { get; set; }
        public Cast Cast { get; set; }
    }
}