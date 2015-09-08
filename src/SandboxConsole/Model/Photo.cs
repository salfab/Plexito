using SandboxConsole.Services;

namespace SandboxConsole.Model
{
    public class Photo
    {
        public Player Player { get; set; }

        public long AddedAt { get; set; }
        public string Guid { get; set; }
        public long Index { get; set; }
        public string Key { get; set; }
        public long LibrarySectionId { get; set; }
        public string OriginallyAvailableAt { get; set; }
        public long RatingKey { get; set; }
        public long SessionKey { get; set; }
        public string Summary { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public long UpdatedAt { get; set; }
        public long Year { get; set; }
    }
}