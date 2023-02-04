using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundLab.Utilities
{
    public class PageVmBase: ViewModelBase 
    {
        public ViewModel.MainVM Parent;
        public Model.SoundModel Model
        {
            get { return Parent.Model; }
        }
        public PageVmBase(ViewModel.MainVM Parent)
        {
            this.Parent = Parent;
        } 
    }
}
