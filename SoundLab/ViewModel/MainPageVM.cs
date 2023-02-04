using SoundLab.Model;
using SoundLab.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoundLab.ViewModel
{
    public class MainPageVM : PageVmBase
    {
        public int DeviceNumber
        {
            get
            {
                return Model.DeviceNumber;
            }
            set
            {
                Model.DeviceNumber = Convert.ToInt32(value);
                onPropertyChanged(nameof(DeviceNumber));
            }
        }
        public ObservableCollection<System.IO.FileInfo> Files
        {
            get { return Model.Files; }

        }
        public MainPageVM(MainVM Parent) : base(Parent) { }
        private RelayCommand _setFile;
        public RelayCommand SetFile
        {
            get
            {
                return _setFile ??
                  (_setFile = new RelayCommand(obj =>
                  {
                      Model.selectedFile = obj as string;
                  }));
            }
        }
        private RelayCommand _reload;
        public RelayCommand Reload
        {
            get
            {
                return _reload ??
                  (_reload = new RelayCommand(obj =>
                  {
                      onPropertyChanged(nameof(Files));
                  }));
            }
        }
        private RelayCommand _drop;
        public RelayCommand Drop
        {
            get
            {
                return _drop ??
                  (_drop = new RelayCommand(obj =>
                  {
                      var o = obj as IDataObject;
                      var f = o?.GetData(DataFormats.FileDrop);
                      Model.AddFile(f);
                      onPropertyChanged(nameof(Files));
                  }));
            }
        }
        private RelayCommand _start;
        public RelayCommand Start
        {
            get
            {
                return _start ??
                  (_start = new RelayCommand(obj =>
                  {
                      Model.Play();
                  }));
            }
        }
        private RelayCommand _stop;



        public RelayCommand Stop
        {
            get
            {
                return _stop ??
                  (_stop = new RelayCommand(obj =>
                  {
                      Model.Stop();
                  }));
            }
        }
    }
}
