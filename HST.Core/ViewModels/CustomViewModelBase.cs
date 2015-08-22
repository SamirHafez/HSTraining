using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public abstract class CustomViewModelBase : ViewModelBase, IViewModel
    {
        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action(this as T); 
        }

        public virtual Task InitAsync(object parameter)
        {
            return Task.FromResult<object>(null);
        }
    }
}
