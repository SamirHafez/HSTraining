using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HST.Core.Models;
using HST.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public class WorkoutsViewModel : CustomViewModelBase
    {
        ObservableCollection<HSTWorkout> workouts;
        public ObservableCollection<HSTWorkout> Workouts
        {
            get { return workouts; }
            set { Set(ref workouts, value); }
        }

        RelayCommand<HSTWorkout> enterWorkoutCommand;
        public RelayCommand<HSTWorkout> EnterWorkoutCommand
        {
            get
            {
                return enterWorkoutCommand ??
                    (enterWorkoutCommand = new RelayCommand<HSTWorkout>(EnterWorkout));
            }
        }

        RelayCommand createWorkoutCommand;
        public RelayCommand CreateWorkoutCommand
        {
            get
            {
                return createWorkoutCommand ??
                    (createWorkoutCommand = new RelayCommand(CreateWorkout));
            }
        }


        readonly INavigationService NavigationService;
        readonly IStorageService StorageService;
        public WorkoutsViewModel(INavigationService navigationService, IStorageService storageService)
        {
            NavigationService = navigationService;
            StorageService = storageService;
        }

        public override async Task InitAsync(object parameter)
        {
            var workouts = await StorageService.GetWorkoutsAsync();

            Workouts = new ObservableCollection<HSTWorkout>(workouts);
        }

        void EnterWorkout(HSTWorkout workout)
        {
            NavigationService.NavigateTo("Workout", workout);
        }

        void CreateWorkout()
        {
            NavigationService.NavigateTo("NewWorkout");
        }
    }
}