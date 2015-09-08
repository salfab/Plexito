using SandboxConsole.Services;

namespace SandboxConsole.Model
{
    public class Track
    {
        public Player Player { get; set; }

        public int AddedAt { get; set; }

        public string Art { get; set; }

        public string ChapterSource { get; set; }
        public int Duration { get; set; }
        public string GrandParentArt { get; set; }
        public string GrandParentKey { get; set; }
        public int GrandParentRatingKey { get; set; }
        public string GrandParentThumb { get; set; }
        public string GrandParentTitle { get; set; }
        public string Guid { get; set; }
        public int Index { get; set; }
        public string Key { get; set; }
        public string LibrarySectionId { get; set; }
        public int ParentIndex { get; set; }
        public string ParentKey { get; set; }
        public int ParentRatingKey { get; set; }
        public string ParentThumb { get; set; }
        public string ParentTitle { get; set; }
        public int RatingCount { get; set; }
        public int RatingKey { get; set; }
        public int SessionKey { get; set; }
        public string Summary { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int UpdatedAt { get; set; }

    }
}