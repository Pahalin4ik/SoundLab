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
        public KeyBindingsVM KeyBindingsVM { get; private set; }
        public SoundTreeVM SoundTreeVM { get; private set; }
        public MainVM()
        {
            Model = new SoundModel();
            KeyBindingsVM = new KeyBindingsVM(this);
            SoundTreeVM = new SoundTreeVM(this);
        }


    }
}
