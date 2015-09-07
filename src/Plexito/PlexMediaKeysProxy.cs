namespace Plexito
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    using Ownskit.Utils;

    using SandboxConsole.Model;
    using SandboxConsole.Services;

    public class PlexMediaKeysProxy : IDisposable
    {
        private readonly KeyboardListener kListener;

        private PlexBinding plexApi;

        private PlexDevice device;

        public PlexMediaKeysProxy(PlexBinding plexApi)
        {
            this.plexApi = plexApi;
            this.kListener = new KeyboardListener();
        }

        public void Start()
        {
            this.kListener.KeyDown += this.OnKeyDown;
        }

        public void SetDevice(PlexDevice newDevice)
        {
            this.device = newDevice;
        }

        private void OnKeyDown(object sender, RawKeyEventArgs args)
        {            
            switch (args.Key)
            {                
                case Key.MediaNextTrack:
                    plexApi.PlayBack.SkipNext(this.device);
                    break;
                case Key.MediaPreviousTrack:
                    plexApi.PlayBack.SkipPrevious(this.device);
                    break;
                //case Key.MediaStop:
                //    plexApi.PlayBack.SkipNext(this.device);
                    break;
                case Key.MediaPlayPause:
                    plexApi.PlayBack.Pause(this.device);
                    break;              
            }
        }

        public void Dispose()
        {
            this.kListener.KeyDown -= this.OnKeyDown;
            this.kListener.Dispose();
        }
    }
}