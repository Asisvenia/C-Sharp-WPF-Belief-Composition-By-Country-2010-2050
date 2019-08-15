using Religious_Composition_Program_Ver01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Religious_Composition_Program_Ver01.ViewModels
{
   public class CountrySearchViewModel : ObservableObject
    {
        private ReligiousDataVM _ReligiousDataVM;
        private List<ReligiousComposition> ReligiousDataCollection { get; set; }
        private List<ReligiousComposition> _selectedReligiousData;
        public List<ReligiousComposition> SelectedReligiousData
        {
            get { return _selectedReligiousData; }
            set { OnPropertyChanged(ref _selectedReligiousData, value); }
        }
        private string _NoResultsFoundVis;
        public string NoResultsFoundVisibility
        {
            get { return _NoResultsFoundVis; }
            set { OnPropertyChanged(ref _NoResultsFoundVis, value); }
        }
        private string _GridDataView;
        public string GridDataView
        {
            get { return _GridDataView; }
            set { OnPropertyChanged(ref _GridDataView, value); }
        }
        /// Commands
        public ICommand SearchByCountryCommand { get; set; }

        public CountrySearchViewModel(ReligiousDataVM ReligiousDataVM)
        {
            _ReligiousDataVM = ReligiousDataVM;
            ReligiousDataCollection = _ReligiousDataVM.ReligiousDataCollection;

            SearchByCountryCommand = new RelayCommand(SearchByCountry);
            NoResultsFoundVisibility = "Collapsed";
            GridDataView = "Collapsed";
        }
        private void SearchByCountry(object _countryName)
        {
            string countryName = (string)_countryName;
            SelectedReligiousData = ReligiousDataCollection.Where(x => x.CountryName.ToLower() == countryName.ToLower()).
                                     ToList();

            if (SelectedReligiousData.Count == 0)
            {
                NoResultsFoundVisibility = "Visible";
                GridDataView = "Collapsed";
            }
            else
            {
                GridDataView = "Visible";
                NoResultsFoundVisibility = "Collapsed";
            }
        }
    }
}
