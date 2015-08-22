using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public interface IViewModel
    {
        Task InitAsync(object parameter);
        void SetState<T>(Action<T> viewModel) where T : class, IViewModel;
    }
}
