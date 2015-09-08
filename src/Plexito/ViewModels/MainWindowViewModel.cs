using Plexito.Annotations;
using SandboxConsole.Model;
using SandboxConsole.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Plexito.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _parent;
        private string _title;
        private PlexBinding _api;
        private Timer playerStateUpdateTimer;
        private IEnumerable<PlexDevice> _servers;
        private PlexDevice _player;

        private string thumbnailLocation;

        public MainWindowViewModel()
        {
            // We should use a service locator or an IoC container, just to make sure we are using the same instance of PlexBinding throughout the app.
            _api = new PlexBinding(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            var devices = _api.GetDevices();
            _player = devices[ConfigurationManager.AppSettings["playerName"]];
            _servers = devices.Values.Where(d => d.Provides.Contains("server"));
            playerStateUpdateTimer = new Timer(UpdatePlayerState, null, 0, 10000);
            Title = "Title";
            Parent = "Parent";
            ThumbnailLocation = "http://siliconangle.com/wp-content/plugins/special-recent-posts-pro/images/no-thumb.png";

            SkipNextCommand = new DelegateCommand(OnSkipNext);
            SkipPreviousCommand = new DelegateCommand(OnSkipPrevious);
            PauseCommand = new DelegateCommand(OnPause);
        }

        private void OnPause()
        {
            // we could have an intermediary service here to basically "manage" the playback, but since it does nothing else than calling the plex api, it seems like a bit of overkill.
            _api.PlayBack.Pause(_player);
        }

        private void OnSkipPrevious()
        {
            _api.PlayBack.SkipPrevious(_player);
        }

        private void OnSkipNext()
        {
            _api.PlayBack.SkipNext(_player);
        }

        private void UpdatePlayerState(object state)
        {
            var status = _api.PlayBack.GetStatus(_player, _servers);
            if (status != null)
            {
                var plexServer = this._servers.Single();
                if (status.Video != null)
                {
                    this.Title = status.Video.Title;
                    this.Parent = status.Video.GrandParentTitle;
                    // We don't support if there's more than one server.
                    this.ThumbnailLocation = plexServer.ConnectionUris.First(s => !s.Contains(plexServer.PublicAddress)).TrimEnd('/') + status.Video.Thumb;
                }
                else if (status.Photo != null)
                {
                    this.Title = status.Photo.Title;
                    this.Parent = status.Photo.OriginallyAvailableAt;
                    // We don't support if there's more than one server.
                    this.ThumbnailLocation = plexServer.ConnectionUris.First(s => !s.Contains(plexServer.PublicAddress)).TrimEnd('/') + status.Photo.Thumb;
                }
                else if (status.Track != null)
                {
                    this.Title = "status.Track.Title";
                    this.Parent = "status.Track.Artist - status.Track.Album";
                    // We don't support if there's more than one server.
                    //this.ThumbnailLocation = _player.ConnectionUris.First().TrimEnd('/') + status.Track.Thumb;
                }
            }
            else
            {
                this.Title = string.Empty;
                this.Parent = string.Empty;
            }
        }

        public string ThumbnailLocation
        {
            get
            {
                return this.thumbnailLocation;
            }
            set
            {
                if (value == this.thumbnailLocation) return;
                this.thumbnailLocation = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (value == _parent) return;
                _parent = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public DelegateCommand PauseCommand { get; set; }
        public DelegateCommand SkipNextCommand { get; set; }
        public DelegateCommand SkipPreviousCommand { get; set; }
    }
}