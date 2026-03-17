namespace museum_artifact_manager.Models
{
    public class Restoration
    {
        public int Id { get; set; }

        public string? Date { get; set; }

        public string? Description { get; set; }

        public int ArtifactId { get; set; }

        public Artifact? Artifact { get; set; }
    }
}