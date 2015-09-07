using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandboxConsole.Services
{
    public class Video
    {
        public long Duration { get; set; }

        public long ViewOffset { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string GrandParentTitle { get; set; }

        public string Thumb { get; set; }
    }
}
