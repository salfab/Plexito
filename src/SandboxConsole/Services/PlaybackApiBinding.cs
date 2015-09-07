namespace SandboxConsole.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Jint;

    using Plexito.JavaScriptLogic.Stubs;

    using SandboxConsole.Model;

    public class PlaybackApiBinding
    {
        private readonly Engine scripts;

        public PlaybackApiBinding(string controlJs)
        {
            scripts = new Engine(cfg => cfg.AllowClr(typeof(XMLHttpRequest).Assembly)).Execute(controlJs);
        }

        public DeviceStatus GetStatus(PlexDevice device, IEnumerable<PlexDevice> plexServers)
        {

            var statuses =
                this.scripts.SetValue("device", device)
                    .SetValue("plexServers", plexServers.ToArray())
                    .Execute("GetStatus_jint(device, plexServers)")
                    .GetCompletionValue()
                    .ToObject();
            
            foreach (var status in ((object[])statuses).Cast<string>())
            {
                var document = XDocument.Parse(status);

                var videoElement = document.Element("MediaContainer").Element("Video");
                if (videoElement.Element("Player").Attribute("machineIdentifier").Value == device.ClientIdentifier)
                {
                    // Making a lightweight version right now, because in the future, we expect statuses to return directly a single JSON object.
                    return new DeviceStatus()
                           {
                               Video = new Video {
                                           Duration = long.Parse(videoElement.Attribute("duration").Value),
                                           ViewOffset = long.Parse(videoElement.Attribute("viewOffset").Value),
                                           Title = videoElement.Attribute("title").Value,
                                           Summary = videoElement.Attribute("summary").Value,
                                           GrandParentTitle = videoElement.Attribute("grandparentTitle").Value,
                                           Thumb = videoElement.Attribute("thumb").Value
                                       },
                                Player = new Player {
                                             Title = videoElement.Element("Player").Attribute("title").Value,
                                             MachineIdentifier = videoElement.Element("Player").Attribute("machineIdentifier").Value,
                                             Platform = videoElement.Element("Player").Attribute("platform").Value,
                                             Product = videoElement.Element("Player").Attribute("product").Value,
                                             State = videoElement.Element("Player").Attribute("state").Value
                                         }
                           };
                }
            }

            return null;
        }

        public void Pause(PlexDevice device)
        {
            scripts
                .SetValue("device", device)
                .Execute("pause_jint(device)");
        }

        public void Resume(PlexDevice device)
        {
            scripts
                .SetValue("device", device)
                .Execute("play_jint(device)");
               
        }

        public void SkipNext(PlexDevice device)
        {
            scripts
                .SetValue("device", device)
                .Execute("skipNext_jint(device)");           
        }

        public void SkipPrevious(PlexDevice device)
        {
            scripts
                .SetValue("device", device)
                .Execute("skipPrevious_jint(device)");                 
        }
    }


}