using SandboxConsole.Services;
using System;
using System.Linq;

namespace SandboxConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var api = new PlexBinding();
            var devices = api.GetDevices();
            var hubertDevice = devices["Hubert"];
            var status = api.PlayBack.GetStatus(hubertDevice, devices.Values.Where(d => d.Provides.Contains("server")));
            api.PlayBack.Pause(hubertDevice);
            api.PlayBack.Resume(hubertDevice);
            api.PlayBack.SkipPrevious(hubertDevice);
            api.PlayBack.SkipNext(hubertDevice);

            Console.ReadKey();
        }
    }
}