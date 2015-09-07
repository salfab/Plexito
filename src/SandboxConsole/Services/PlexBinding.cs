namespace SandboxConsole.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Net;
    using System.Text;
    using System.Xml.Linq;

    using SandboxConsole.Model;

    public class PlexBinding
    {
        public IReadOnlyDictionary<string, PlexDevice> GetDevices()
        {            
            IDictionary<string, PlexDevice> devices = new Dictionary<string, PlexDevice>();

            WebClient wc = new WebClient();

            var userName = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

            wc.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            var devicesXml = wc.DownloadString("https://plex.tv/devices.xml");

            var document = XDocument.Parse(devicesXml);
            
            foreach (var item in document.Element("MediaContainer").Elements("Device"))
            {
                var plexDevice = new PlexDevice
                                 {
                                     Name = item.Attribute("name").Value,
                                     PublicAddress = item.Attribute("publicAddress").Value,
                                     Product = item.Attribute("product").Value,
                                     ProductVersion = item.Attribute("productVersion").Value,
                                     Platform = item.Attribute("platform").Value,
                                     PlatformVersion = item.Attribute("platformVersion").Value,
                                     Device = item.Attribute("device").Value,
                                     Model = item.Attribute("model").Value,
                                     Vendor = item.Attribute("vendor").Value,
                                     Provides = item.Attribute("provides").Value,
                                     ClientIdentifier = item.Attribute("clientIdentifier").Value,
                                     Version = item.Attribute("version").Value,
                                     Id = item.Attribute("id").Value,
                                     Token = item.Attribute("token").Value,
                                     LastSeenAt = item.Attribute("lastSeenAt").Value,
                                     ScreenResolution = item.Attribute("screenResolution").Value,
                                     ScreenDensity = item.Attribute("screenDensity").Value
                                 };
                foreach (var element in item.Elements("Connection"))
                {
                    plexDevice.AddConnectionUri(element.Attribute("uri").Value);
                }


                if (plexDevice.Name != string.Empty)
                {
                    devices.Add(plexDevice.Name, plexDevice);                    
                }

            }

            return new ReadOnlyDictionary<string, PlexDevice>(devices);
        }

        public PlaybackApiBinding PlayBack { get; set; }

        public PlexBinding()
        {
            this.PlayBack = new PlaybackApiBinding(Plexito.JavaScriptLogic.Scripts.PlaybackApi);
        }
    }
}