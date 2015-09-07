using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxConsole
{
    using JimBobBennett.JimLib.Network;
    using JimBobBennett.JimLib.Xamarin.Net45.Images;
    using JimBobBennett.JimLib.Xamarin.Network;
    using JimBobBennett.JimLib.Xamarin.Timers;
    using JimBobBennett.RestAndRelaxForPlex.Caches;
    using JimBobBennett.RestAndRelaxForPlex.Connection;
    using JimBobBennett.RestAndRelaxForPlex.PlexObjects;
    using Jint;
    using SandboxConsole.Model;
    using SandboxConsole.Services;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

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