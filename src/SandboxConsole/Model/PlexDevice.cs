namespace SandboxConsole.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class PlexDevice
    {
        private IList<string> connectionUris;

        public string Name { get; set; }

        public string PublicAddress { get; set; }

        public string Product { get; set; }

        public string ProductVersion { get; set; }

        public string Platform { get; set; }

        public string PlatformVersion { get; set; }

        public string Device { get; set; }

        public string Model { get; set; }

        public string Vendor { get; set; }

        public string Provides { get; set; }

        public string ClientIdentifier { get; set; }

        public string Version { get; set; }

        public string Id { get; set; }

        public string Token { get; set; }

        public string LastSeenAt { get; set; }

        public string ScreenResolution { get; set; }

        public string ScreenDensity { get; set; }

        public PlexDevice()
        {
            connectionUris = new List<string>();
        }

        public string[] ConnectionUris
        {
            get
            {
                return this.connectionUris.ToArray();
            }
        }

        public void AddConnectionUri(string uri)
        {            
            connectionUris.Add(uri);
        }
    }
}