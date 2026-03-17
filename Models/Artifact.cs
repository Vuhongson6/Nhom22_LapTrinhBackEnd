namespace museum_artifact_manager.Models
{
    public class Artifact
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public string? Status { get; set; }

        // khóa ngoại
        public int? ExhibitionId { get; set; }

        // navigation property
        public Exhibition? Exhibition { get; set; }
    }
}