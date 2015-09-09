using Jint;
using Newtonsoft.Json.Linq;
using SandboxConsole.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;

namespace SandboxConsole.Services
{
    using System;
    using System.Configuration;

    using Plexito.JavaScriptLogic;
    using Plexito.JavaScriptLogic.Stubs;

    public class PlexBinding
    {
        public static Lazy<PlexBinding> Instance { get; set; }

        private readonly string _username;
        private readonly string _password;
        private Engine _scripts;

        public IReadOnlyDictionary<string, PlexDevice> GetDevices()
        {
            IDictionary<string, PlexDevice> devices = new Dictionary<string, PlexDevice>();

            var devicesJson = (string)_scripts
                .SetValue("username", _username)
                .SetValue("password", _password)
                .Execute("GetDevices_jint(username, password)")
                .GetCompletionValue()
                .ToObject();

            var devicesJObject = JObject.Parse(devicesJson)["Device"].Select(o =>
            {
                var device = o["@attributes"].ToObject<PlexDevice>();
                var connectionToken = o["Connection"];
                if (connectionToken != null)
                {
                    if (connectionToken.Skip(1).Any())
                    {
                        foreach (var uri in connectionToken.Select(c => c["@attributes"]["uri"].Value<string>()))
                        {
                            device.AddConnectionUri(uri);
                        }
                    }
                    else
                    {
                        device.AddConnectionUri(connectionToken["@attributes"]["uri"].Value<string>());
                    }
                }

                return device;
            }).ToArray();

            //var wc = new WebClient();

            //var userName = ConfigurationManager.AppSettings["username"];
            //var password = ConfigurationManager.AppSettings["password"];

            //var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

            //wc.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            //var devicesXml = wc.DownloadString("https://plex.tv/devices.xml");

            //var document = XDocument.Parse(devicesXml);

            //foreach (var item in document.Element("MediaContainer").Elements("Device"))
            //{
            //    var plexDevice = new PlexDevice
            //                     {
            //                         Name = item.Attribute("name").Value,
            //                         PublicAddress = item.Attribute("publicAddress").Value,
            //                         Product = item.Attribute("product").Value,
            //                         ProductVersion = item.Attribute("productVersion").Value,
            //                         Platform = item.Attribute("platform").Value,
            //                         PlatformVersion = item.Attribute("platformVersion").Value,
            //                         Device = item.Attribute("device").Value,
            //                         Model = item.Attribute("model").Value,
            //                         Vendor = item.Attribute("vendor").Value,
            //                         Provides = item.Attribute("provides").Value,
            //                         ClientIdentifier = item.Attribute("clientIdentifier").Value,
            //                         Version = item.Attribute("version").Value,
            //                         Id = item.Attribute("id").Value,
            //                         Token = item.Attribute("token").Value,
            //                         LastSeenAt = item.Attribute("lastSeenAt").Value,
            //                         ScreenResolution = item.Attribute("screenResolution").Value,
            //                         ScreenDensity = item.Attribute("screenDensity").Value
            //                     };
            //    foreach (var element in item.Elements("Connection"))
            //    {
            //        plexDevice.AddConnectionUri(element.Attribute("uri").Value);
            //    }

            //    if (plexDevice.Name != string.Empty)
            //    {
            //        devices.Add(plexDevice.Name, plexDevice);
            //    }
            //}
            foreach (var device in devicesJObject.Where(device => device.Name != string.Empty))
            {
                devices.Add(device.Name, device);
            }
            return new ReadOnlyDictionary<string, PlexDevice>(devices);
        }

        public PlaybackApiBinding PlayBack { get; set; }

        public PlexBinding(string username, string password)
        {
            _username = username;
            _password = password;
            this.PlayBack = new PlaybackApiBinding(Scripts.PlaybackApi);
            _scripts = new Engine(cfg => cfg.AllowClr(typeof(XMLHttpRequest).Assembly)).Execute(Plexito.JavaScriptLogic.Scripts.PlexApi);            
        }

        static PlexBinding()
        {
            Instance = new Lazy<PlexBinding>(() => new PlexBinding(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]));            
        }
    }
}