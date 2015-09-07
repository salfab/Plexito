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

        public MainWindowViewModel()
        {
            _api = new PlexBinding();
            var devices = _api.GetDevices();
            _player = devices[ConfigurationManager.AppSettings["playerName"]];
            _servers = devices.Values.Where(d => d.Provides.Contains("server"));
            playerStateUpdateTimer = new Timer(UpdatePlayerState, null, 0, 1000);
            Title = "Title";
            Parent = "Parent";
        }

        private void UpdatePlayerState(object state)
        {
            var status = _api.PlayBack.GetStatus(_player, _servers);
            if (status != null)
            {
                this.Title = status.Video.Title;
                this.Parent = status.Video.GrandParentTitle;
            }
            else
            {
                this.Title = string.Empty;
                this.Parent = string.Empty;
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
    }
}