using SoundLab.Model;
using SoundLab.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundLab.ViewModel
{
    public class KeyBindingsVM: ViewModelBase
    {
        public MainVM Parent { get; set; }
        public SoundModel Model { get => Parent.Model; }
        public KeyBindingsVM(MainVM Parent)
        {
            this.Parent = Parent;
        }
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
        public string StartKey
        {
            get { return Model.StartKey; }
            set { Model.StartKey = value; onPropertyChanged(nameof(StartKey)); }
        }
        public string StopKey
        {
            get { return Model.StopKey; }
            set
            {
                Model.StopKey = value; onPropertyChanged(nameof(StopKey));
            }
        }
    }
}
