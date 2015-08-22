using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<WorkoutsViewModel>();
            SimpleIoc.Default.Register<WorkoutViewModel>();
            SimpleIoc.Default.Register<NewWorkoutViewModel>();
            SimpleIoc.Default.Register<CycleViewModel>();
            SimpleIoc.Default.Register<WorkdayViewModel>();
        }

        public WorkoutsViewModel Workouts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WorkoutsViewModel>();
            }
        }

        public WorkoutViewModel Workout
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WorkoutViewModel>();
            }
        }

        public NewWorkoutViewModel NewWorkout
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewWorkoutViewModel>();
            }
        }


        public CycleViewModel Cycle
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CycleViewModel>();
            }
        }

        public WorkdayViewModel Workday
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WorkdayViewModel>();
            }
        }
    }
}