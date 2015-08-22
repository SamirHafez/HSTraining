using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using HST.Core.Services;
using HST.Services;
using Microsoft.Practices.ServiceLocation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HST
{
    public sealed partial class App : Application
    {
        //private TransitionCollection transitions;

        public App()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Workouts", typeof(WorkoutsPage));
            navigationService.Configure("Workout", typeof(WorkoutPage));
            navigationService.Configure("NewWorkout", typeof(NewWorkoutPage));
            navigationService.Configure("Cycle", typeof(CyclePage));
            navigationService.Configure("Workday", typeof(WorkdayPage));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            var storageService = new StorageService();
            SimpleIoc.Default.Register<IStorageService>(() => storageService);
            HardwareButtons.BackPressed += (sender, args) =>
            {
                navigationService.GoBack();
                args.Handled = true;
            };

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            Window.Current.Content = new Frame();
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.NavigateTo("Workouts");
            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
    }
}