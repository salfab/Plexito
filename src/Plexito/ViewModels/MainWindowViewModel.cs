using Plexito.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Plexito.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _parent;
        private string _title;

        public MainWindowViewModel()
        {
            Title = "Title";
            Parent = "Parent";
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