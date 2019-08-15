using Religious_Composition_Program_Ver01.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Religious_Composition_Program_Ver01.ViewModels
{
    public class ReligiousDataVM : ObservableObject
    {
        private string path = "Religious_Composition_by_Country_2010-2050.csv";
        private string continentValue = string.Empty;
        private int yearValue = 2010;
        private int lastCodeCasted;
        private string lastParam;

        public enum DescTitles
        {
            Country = 1,
            Christians,
            Muslims,
            Jews,
            Unaffiliated,
            All
        };
        private DescTitles _selectedDesc;
        public DescTitles selectedDesc
        {
            get { return _selectedDesc; }
            set
            {
                _selectedDesc = value;
                ReligiousComposition.selectedStringDesc = _selectedDesc.ToString();
                selectedStringDesc = _selectedDesc.ToString();
            }
        }

        private string _selectedStringDesc;
        public string selectedStringDesc
        {
            get { return _selectedStringDesc; }
            set { OnPropertyChanged(ref _selectedStringDesc, value); }
        }

        public List<bool> DescBooleans { get; set; }

        public List<ReligiousComposition> ReligiousDataCollection { get; set; }
        private List<ReligiousComposition> _selectedReligiousData;
        public List<ReligiousComposition> SelectedReligiousData
        {
            get { return _selectedReligiousData; }
            set { OnPropertyChanged(ref _selectedReligiousData, value); }
        }
        /// Commands
        public ICommand UpdateYearsCommand { get; set; }
        public ICommand UpdateContinentsCommand { get; set; }
        public ICommand UpdateDescsCommand { get; set; }

        public ReligiousDataVM()
        {
            ReligiousDataCollection = new List<ReligiousComposition>();
            ReligiousDataCollection = ReadFile();
            SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).ToList(); /// Initially, take only data from 2010

            selectedDesc = DescTitles.Country;
            DescBooleans = new List<bool>()
            {
               false, /// null
               true, /// country
               false, /// christians ...
               false, /// ..
               false,
               false,
               false,
            };

            UpdateYearsCommand = new RelayCommand(UpdateYears);
            UpdateContinentsCommand = new RelayCommand(UpdateContinents);
            UpdateDescsCommand = new RelayCommand(UpdateDescriptions);
        }
        private List<ReligiousComposition> ReadFile()
        {
            ReligiousDataCollection = File.ReadAllLines(path).Skip(1).
                                      Select(ReligiousComposition.ParseFromCSV).ToList();
            return ReligiousDataCollection;
        }
        private void UpdateYears(object year)
        {
            int yearCasted = (int)year;
            if(string.IsNullOrEmpty(continentValue))
                SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearCasted).ToList();
            else
                SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearCasted && x.Continent == continentValue).ToList();

            yearValue = yearCasted;
            RepeatLastOrdering(); /// Order by last selected description value
        }
        private void UpdateContinents(object name)
        {
            string nameCasted = (string)name;
            if(nameCasted == "All")
            {
                SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).ToList();
                continentValue = string.Empty;
            }
            else
            {
                SelectedReligiousData = ReligiousDataCollection.Where(x => x.Continent == nameCasted && x.Year == yearValue).ToList();
                continentValue = nameCasted;
            }
            RepeatLastOrdering(); /// Order by last selected description value
        }
        private void DataOrderingString(string param, bool orderByDescending)
        {
            var propertyInfo = typeof(ReligiousComposition).GetProperty(param);

            if(orderByDescending == false)
            {
                if (continentValue == string.Empty)
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).
                                                                    OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                else
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Continent == continentValue && x.Year == yearValue).
                                                                    OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
            } else
            {
                if (continentValue == string.Empty)
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).
                                                                    OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                else
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Continent == continentValue && x.Year == yearValue).
                                                                    OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
            }
        }
        private void DataOrderingNumbers(string param, bool orderByDescending)
        {
            var propertyInfo = typeof(ReligiousComposition).GetProperty(param);

            if (orderByDescending == false)
            {
                if (continentValue == string.Empty)
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).
                                                                    OrderBy(x => double.Parse(Regex.Replace(propertyInfo.GetValue(x, null).ToString(), "[,<]", string.Empty))).ToList();
                else
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Continent == continentValue && x.Year == yearValue).
                                                                    OrderBy(x => double.Parse(Regex.Replace(propertyInfo.GetValue(x, null).ToString(), "[,<]", string.Empty))).ToList();
            }
            else
            {
                if (continentValue == string.Empty)
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Year == yearValue).
                                                                    OrderByDescending(x => double.Parse(Regex.Replace(propertyInfo.GetValue(x, null).ToString(), "[,<]", string.Empty))).ToList();
                else
                    SelectedReligiousData = ReligiousDataCollection.Where(x => x.Continent == continentValue && x.Year == yearValue).
                                                                    OrderByDescending(x => double.Parse(Regex.Replace(propertyInfo.GetValue(x, null).ToString(), "[,<]", string.Empty))).ToList();
            }
        }
        private void RepeatLastOrdering()
        {
            if(lastParam != null && lastCodeCasted != 0)
            {
                if (selectedDesc == DescTitles.Country)
                {
                    if (DescBooleans[lastCodeCasted] == true)
                    {
                        DataOrderingString(lastParam, false);
                    }
                    else
                    {
                        DataOrderingString(lastParam, true);
                    }
                }
                else
                {
                    if (DescBooleans[lastCodeCasted] == true)
                    {
                        DataOrderingNumbers(lastParam, false);
                    }
                    else
                    {
                        DataOrderingNumbers(lastParam, true);
                    }
                }
            }
        }

        private void UpdateDescriptions(object code)
        {
            int codeCasted = (int)code;
            lastCodeCasted = codeCasted;
            selectedDesc = (DescTitles)codeCasted;
            string param = string.Empty;
            switch (selectedDesc)
            {
                case DescTitles.Country:
                    param = "CountryName";
                    break;
                case DescTitles.Christians:
                    param = "Christians";
                    break;
                case DescTitles.Muslims:
                    param = "Muslims";
                    break;
                case DescTitles.Jews:
                    param = "Jews";
                    break;
                case DescTitles.Unaffiliated:
                    param = "Unaffiliated";
                    break;
                case DescTitles.All:
                    param = "All";
                    break;
                default:
                    break;
            }
            lastParam = param;

            if (selectedDesc == DescTitles.Country)
            {
                if (DescBooleans[codeCasted] == true)
                {
                    DataOrderingString(param, false);
                }
                else
                {
                    DataOrderingString(param, true);
                }
            } else
            {
                if (DescBooleans[codeCasted] == true)
                {
                    DataOrderingNumbers(param, false);
                }
                else
                {
                    DataOrderingNumbers(param, true);
                }
            }
        }
    }

    //public static class CollectionUtils
    //{
    //    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> thisCollection)
    //    {
    //        if (thisCollection == null)
    //            return null;

    //        var oc = new ObservableCollection<T>();

    //        foreach (var item in thisCollection)
    //        {
    //            oc.Add(item);
    //        }
    //        return oc;
    //    }
    //}
    //public static class LinqExtensions
    //{
    //    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
    //    {
    //        return new ObservableCollection<T>(_LinqResult);
    //    }
    //}

}
