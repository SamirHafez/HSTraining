using HST.Core.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HST
{
    public abstract class ViewPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var viewModel = (IViewModel)this.DataContext;

            viewModel.InitAsync(e.Parameter);
        }
    }
}
