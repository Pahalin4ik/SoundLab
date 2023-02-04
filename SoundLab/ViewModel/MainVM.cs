using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using SoundLab.Model;
using SoundLab.Utilities;

namespace SoundLab.ViewModel
{
    public class MainVM: ViewModelBase
    {
        public SoundModel Model { get; private set; }
        public MainPageVM MainPageVM { get; private set; }
        public MainVM()
        {
            Model = new SoundModel();
            MainPageVM = new MainPageVM(this);
        }
    }
}
