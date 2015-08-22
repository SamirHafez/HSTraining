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
    public class CycleViewModel : CustomViewModelBase
    {
        ObservableCollection<Workday> workdays;
        public ObservableCollection<Workday> Workdays
        {
            get { return workdays; }
            set { Set(ref workdays, value); }
        }

        RelayCommand<Workday> enterWorkdayCommand;
        public RelayCommand<Workday> EnterWorkdayCommand
        {
            get
            {
                return enterWorkdayCommand ??
                    (enterWorkdayCommand = new RelayCommand<Workday>(EnterWorkday));
            }
        }

        readonly INavigationService NavigationService;
        public CycleViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override Task InitAsync(object parameter)
        {
            var cycle = (Cycle)parameter;

            SetState(cycle);

            return base.InitAsync(parameter);
        }

        public void SetState(Cycle viewModel)
        {
            Workdays = new ObservableCollection<Workday>(viewModel.Workdays);
        }

        void EnterWorkday(Workday workday)
        {
            NavigationService.NavigateTo("Workday", workday);
        }
    }
}