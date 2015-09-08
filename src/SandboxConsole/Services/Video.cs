using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandboxConsole.Services
{
    public class Video
    {
        public long AddedAt { get; set; }
        public long UpdatedAt { get; set; }
        public long Duration { get; set; }
        public long LibrarySectionId { get; set; }
        public long RatingKey { get; set; }
        public long SessionKey { get; set; }

        public long ViewOffset { get; set; }

        public string Type { get; set; }
        public string Guid { get; set; }
        public string Key { get; set; }
        public string ChapterSource { get; set; }
        public string Title { get; set; }
        public string TitleSort { get; set; }

        public string Summary { get; set; }

        public string GrandParentTitle { get; set; }

        public string Thumb { get; set; }
        public Player Player { get; set; }
    }
}