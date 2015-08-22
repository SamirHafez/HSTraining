using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HST.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public class WorkdayViewModel : CustomViewModelBase
    {
        ObservableCollection<ExerciseWorkday> exercises;
        public ObservableCollection<ExerciseWorkday> Exercises
        {
            get { return exercises; }
            set { Set(ref exercises, value); }
        }

        public bool Done
        {
            get
            {
                return exercises.All(e => e.Done);
            }
        }

        readonly INavigationService NavigationService;
        public WorkdayViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override Task InitAsync(object parameter)
        {
            var workday = (Workday)parameter;

            Exercises = new ObservableCollection<ExerciseWorkday>(workday.Exercises);

            return base.InitAsync(parameter);
        }
    }
}
