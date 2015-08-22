using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HST.Core.Models;
using HST.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HST.Core.ViewModels
{
    public class NewWorkoutViewModel : CustomViewModelBase
    {
        ObservableCollection<ExerciseConfiguration> exercises;
        public ObservableCollection<ExerciseConfiguration> Exercises
        {
            get { return exercises; }
            set { Set(ref exercises, value); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { Set(ref name, value); DoneCommand.RaiseCanExecuteChanged(); }
        }

        string exerciseName;
        public string ExerciseName
        {
            get { return exerciseName; }
            set { Set(ref exerciseName, value); AddCommand.RaiseCanExecuteChanged(); }
        }

        string weight;
        public string Weight
        {
            get { return weight; }
            set { Set(ref weight, value); AddCommand.RaiseCanExecuteChanged(); }
        }

        string reps;
        public string Reps
        {
            get { return reps; }
            set { Set(ref reps, value); AddCommand.RaiseCanExecuteChanged(); }
        }

        RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(Add,
                        () => !string.IsNullOrWhiteSpace(ExerciseName) &&
                              !string.IsNullOrWhiteSpace(Weight) &&
                              !string.IsNullOrWhiteSpace(Reps)));
            }
        }

        RelayCommand<ExerciseConfiguration> deleteCommand;
        public RelayCommand<ExerciseConfiguration> DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand<ExerciseConfiguration>(Delete,
                        exercise => exercise != null));
            }
        }

        RelayCommand doneCommand;
        public RelayCommand DoneCommand
        {
            get
            {
                return doneCommand ??
                    (doneCommand = new RelayCommand(Done,
                        () => !string.IsNullOrWhiteSpace(Name)));
            }
        }

        readonly INavigationService NavigationService;
        readonly IStorageService StorageService;
        public NewWorkoutViewModel(INavigationService navigationService, IStorageService storageService)
        {
            NavigationService = navigationService;
            StorageService = storageService;

            Exercises = new ObservableCollection<ExerciseConfiguration>();
        }

        async void Done()
        {
            var workout = new HSTWorkout(Name, Exercises.ToList());
            await StorageService.InsertWorkoutAsync(workout);

            NavigationService.GoBack();
        }

        void Delete(ExerciseConfiguration exercise)
        {
            Exercises.Remove(exercise);
        }

        void Add()
        {
            double rm15 = 0, rm10 = 0, rm5 = 0;
            if (!string.IsNullOrWhiteSpace(Weight) && !string.IsNullOrWhiteSpace(Reps))
                CalculateRMs(double.Parse(Weight), int.Parse(Reps), out rm15, out rm10, out rm5);

            Exercises.Add(new ExerciseConfiguration
            {
                Exercise = new Exercise(ExerciseName),
                RMs = new Dictionary<RepEnum, double>
                {
                    {RepEnum.RM15, rm15},
                    {RepEnum.RM10, rm10},
                    {RepEnum.RM5, rm5},
                }
            });

            ExerciseName = null;
            Reps = Weight = null;
        }

        // Based on exrx.net
        void CalculateRMs(double weight, int reps, out double rm15, out double rm10, out double rm5)
        {
            var rm1 = reps < 10 ? Math.Round(weight / (1.0278 - 0.0278 * reps)) :
                                  Math.Round(weight / 0.75);

            rm15 = rm1 * .60;
            rm10 = rm1 * .70;
            rm5 = rm1 * .85;
        }
    }
}