namespace DogsApp.Models.Dog
{
    public class DogDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Breed { get; set; } = null!;
        public string? Picture { get; set; }
    }
}
