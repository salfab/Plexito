using Newtonsoft.Json.Linq;

namespace SandboxConsole.Services
{
    using Jint;
    using Plexito.JavaScriptLogic.Stubs;
    using SandboxConsole.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class PlaybackApiBinding
    {
        private readonly Engine scripts;

        public PlaybackApiBinding(string controlJs)
        {
            scripts = new Engine(cfg => cfg.AllowClr(typeof(XMLHttpRequest).Assembly)).Execute(controlJs);
        }

        public DeviceStatus GetStatus(PlexDevice device, IEnumerable<PlexDevice> plexServers)
        {
            var status =
                this.scripts.SetValue("device", device)
                    .SetValue("plexServers", plexServers.ToArray())
                    .Execute("GetStatusJson_jint(device, plexServers)")
                    .GetCompletionValue()
                    .ToObject();

            if (status != null)
            {
                var deviceStatusJson = JObject.Parse((string)status);

                var videoElement = deviceStatusJson["Video"];
                var photoElement = deviceStatusJson["Photo"];
                var trackElement = deviceStatusJson["Track"];
                if (deviceStatusJson["@attributes"]["size"].Value<int>() > 1)
                {
                    // consider only one.
                    if (videoElement != null && videoElement["Player"]["@attributes"]["state"].Value<string>() == "playing")
                    {
                        photoElement = null;
                        trackElement = null;
                    }
                    else if (photoElement != null && photoElement["Player"]["@attributes"]["state"].Value<string>() == "playing")
                    {
                        videoElement = null;
                        trackElement = null;
                    }
                    else if (trackElement != null && trackElement["Player"]["@attributes"]["state"].Value<string>() == "playing")
                    {
                        videoElement = null;
                        photoElement = null;
                    }
                    //deviceStatusJson = deviceStatusJson.SelectToken()
                }
                // we're supposed to have only one here, except if one device has more than one valid connection URL, in which case the same device will have replied anyways (and that scenario shouldn't happen anyways because one of the two should fail.)

                var deviceStatus = new DeviceStatus();

                if (videoElement != null)
                {
                    deviceStatus.Video = videoElement["@attributes"].ToObject<Video>();
                    deviceStatus.Video.Player = videoElement["Player"]["@attributes"].ToObject<Player>();
                }
                else
                {
                    if (photoElement != null)
                    {
                        deviceStatus.Photo = photoElement["@attributes"].ToObject<Photo>();
                        deviceStatus.Photo.Player = photoElement["Player"]["@attributes"].ToObject<Player>();
                    }
                    else
                    {
                        if (trackElement != null)
                        {
                            deviceStatus.Track = trackElement["@attributes"].ToObject<Track>();
                            deviceStatus.Track.Player = trackElement["Player"]["@attributes"].ToObject<Player>();
                        }
                    }
                }

                return deviceStatus;
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