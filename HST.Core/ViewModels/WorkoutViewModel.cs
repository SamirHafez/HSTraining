using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HST.Core.Models;
using HST.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public class WorkoutViewModel : CustomViewModelBase
    {
        string name;
        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        Cycle cycle1;
        public Cycle Cycle1
        {
            get { return cycle1; }
            set { Set(ref cycle1, value); }
        }

        Cycle cycle2;
        public Cycle Cycle2
        {
            get { return cycle2; }
            set { Set(ref cycle2, value); }
        }

        Cycle cycle3;
        public Cycle Cycle3
        {
            get { return cycle3; }
            set { Set(ref cycle3, value); }
        }

        Cycle cycle4;
        public Cycle Cycle4
        {
            get { return cycle4; }
            set { Set(ref cycle4, value); }
        }

        RelayCommand<Cycle> enterCycleCommand;
        public RelayCommand<Cycle> EnterCycleCommand
        {
            get
            {
                return enterCycleCommand ??
                    (enterCycleCommand = new RelayCommand<Cycle>(cycle => 
                        NavigationService.NavigateTo("Cycle", cycle)));
            }
        }

        RelayCommand newWorkoutCommand;
        public RelayCommand NewWorkoutCommand
        {
            get
            {
                return newWorkoutCommand ??
                    (newWorkoutCommand = new RelayCommand(async () => 
                    {
                        var nextWorkout = workout.Continue($"{workout.Name} - {DateTime.Now.ToString("YYYY/MM/DD")}", .10);
                        await StorageService.InsertWorkoutAsync(nextWorkout);
                        NavigationService.GoBack();
                    }));
            }
        }

        readonly INavigationService NavigationService;
        readonly IStorageService StorageService;
        HSTWorkout workout;

        public WorkoutViewModel(INavigationService navigationService, IStorageService storageService)
        {
            NavigationService = navigationService;
            StorageService = storageService;
        }

        public override Task InitAsync(object parameter)
        {
            workout = (HSTWorkout)parameter;

            Cycle1 = workout.Cycle1;
            Cycle2 = workout.Cycle2;
            Cycle3 = workout.Cycle3;
            Cycle4 = workout.Cycle4;

            return base.InitAsync(parameter);
        }
    }
}