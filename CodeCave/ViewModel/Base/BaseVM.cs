using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CodeCave.ViewModel.Base
{
    public class BaseVM : INotifyPropertyChanged
    {
        private bool _IsBusy;
        public bool IsBusy { get => _IsBusy; set => SetProperty(ref _IsBusy, value); }

        public event PropertyChangedEventHandler PropertyChanged;


        #region PropertyChanged
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
