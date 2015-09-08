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
            var statuses =
                this.scripts.SetValue("device", device)
                    .SetValue("plexServers", plexServers.ToArray())
                    .Execute("GetStatusJson_jint(device, plexServers)")
                    .GetCompletionValue()
                    .ToObject();

            if (statuses != null)
            {
                var jsonStatuses = ((object[])statuses).Cast<string>().Select(JObject.Parse);

                // we're supposed to have only one here, except if one device has more than one valid connection URL, in which case the same device will have replied anyways (and that scenario shouldn't happen anyways because one of the two should fail.)
                var deviceStatusJson = jsonStatuses.First(o => o["Video"]["Player"]["@attributes"]["machineIdentifier"].Value<string>() == device.ClientIdentifier);

                var deviceStatus = new DeviceStatus();

                deviceStatus.Video = deviceStatusJson["Video"]["@attributes"].ToObject<Video>();
                deviceStatus.Video.Player = deviceStatusJson["Video"]["Player"]["@attributes"].ToObject<Player>();
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