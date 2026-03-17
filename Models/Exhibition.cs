using System.Collections.Generic;

namespace museum_artifact_manager.Models
{
    public class Exhibition
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string?       StartDate { get; set; }

        public string?       EndDate { get; set; }

        // Một triển lãm có nhiều hiện vật
        public List<Artifact>? Artifacts { get; set; }
    }
}