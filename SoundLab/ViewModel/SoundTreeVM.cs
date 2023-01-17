using SoundLab.Model;
using SoundLab.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundLab.ViewModel
{
    public class SoundTreeVM: ViewModelBase
    {
        public MainVM Parent { get; set; }
        public SoundModel Model { get; set; }
        public SoundTreeVM(MainVM Parent)
        {
            this.Parent = Parent;
            Model = Parent.Model;
        }
        public ObservableCollection<FileInfo> Files
        {
            get { return Model.Files; }
        }
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
    }
}
