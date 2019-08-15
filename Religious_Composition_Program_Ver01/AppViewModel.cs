using Religious_Composition_Program_Ver01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Religious_Composition_Program_Ver01
{
   public class AppViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                OnPropertyChanged(ref _currentView, value);
            }
        }

        private ReligiousDataVM ReligiousDataViewModel;
        private CountrySearchViewModel CountrySearchVM;
        // Commands
        public ICommand LoadAllCountriesViewCommand { get; set; }
        public ICommand LoadCountrySearchViewCommand { get; set; }

        public AppViewModel()
        {
            ReligiousDataViewModel = new ReligiousDataVM();
            CountrySearchVM = new CountrySearchViewModel(ReligiousDataViewModel);

            LoadAllCountriesViewCommand = new RelayCommand(LoadAllCountriesView);
            LoadCountrySearchViewCommand = new RelayCommand(LoadCountrySearchView);

            LoadAllCountriesViewCommand.Execute(null);
        }
        private void LoadAllCountriesView()
        {
            CurrentView = ReligiousDataViewModel;
        }
        private void LoadCountrySearchView()
        {
            CurrentView = CountrySearchVM;
        }
    }
}
