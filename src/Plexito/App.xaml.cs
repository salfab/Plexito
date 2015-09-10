using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Plexito
{
    using SandboxConsole.Services;
    using System.Threading;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private PlexMediaKeysProxy plexMediaKeysProxy;

        public App()
        {
            var plexApi = PlexBinding.Instance.Value;
            var device = plexApi.GetDevices()[ConfigurationManager.AppSettings["playerId"]];
            this.plexMediaKeysProxy = new PlexMediaKeysProxy(plexApi);
            this.plexMediaKeysProxy.SetDevice(device);
            this.plexMediaKeysProxy.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.plexMediaKeysProxy.Dispose();
            base.OnExit(e);
        }
    }
}