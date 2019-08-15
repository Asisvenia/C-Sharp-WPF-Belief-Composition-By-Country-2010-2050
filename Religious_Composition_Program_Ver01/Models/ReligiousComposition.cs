using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Religious_Composition_Program_Ver01.Models
{
    public class ReligiousComposition : ObservableObject
    {
        private string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string[] splitedLines;
        private static List<string> fixedDataCollection = new List<string>();

        public static string selectedStringDesc { get; set; }

        private string _flagPath;
        public string FlagPath
        {
            get
            {
                if(_flagPath.Contains(" "))
                {
                    string[] splittedName = _flagPath.Split(' ');
                    string fixedName = string.Empty;

                    foreach (var item in splittedName)
                    {
                        fixedName += item;
                        if (splittedName[splittedName.Length -1] != item)
                        {
                            fixedName += '_';
                        }
                    }

                    string fixedFlagPath = appFolderPath + @"\Resources\" + fixedName + ".png";
                    return fixedFlagPath;
                } else
                {
                    string fixedFlagPath = appFolderPath + @"\Resources\" + _flagPath + ".png";
                    return fixedFlagPath;
                }
            }
            set
            {
                _flagPath = value;
            }
        }
        private string _countryName;
        public string CountryName
        {
            get
            {
                if (_countryName == "All Countries")
                    return Continent;
                else
                    return _countryName;
            }
            set
            {
                _countryName = value;
            }
        }
        public string Continent { get; set; }
        public int Year { get; set; }
        public string Christians { get; set; }
        public string Muslims { get; set; }
        public string Jews { get; set; }
        public string Unaffiliated { get; set; }
        public string Hindus { get; set; }
        public string Budhists { get; set; }
        public string FolkReligions { get; set; }
        public string Other { get; set; }
        public string All { get; set; }

        public static ReligiousComposition ParseFromCSV(string line)
        {
            splitedLines = line.Split(',');

            string[] fixedLines = FixNumbersWithComa(splitedLines);
            fixedDataCollection.Clear();

            return new ReligiousComposition
            {
                Year = Int32.Parse(fixedLines[3]),
                Continent = fixedLines[4],
                FlagPath = fixedLines[5],
                CountryName = fixedLines[5],
                Christians = fixedLines[6],
                Muslims = fixedLines[7],
                Unaffiliated = fixedLines[8],
                Hindus = fixedLines[9],
                Budhists = fixedLines[10],
                FolkReligions = fixedLines[11],
                Other = fixedLines[12],
                Jews = fixedLines[13],
                All = fixedLines[14],
            };
        }

        private static string[] FixNumbersWithComa(string[] splitedLines)
        {
            bool isQuotationOn = false;
            int quotationCounter = 0;
            string numericalData = string.Empty;

            foreach (string item in splitedLines)
            {
                if (item.Contains('"') || isQuotationOn)
                {
                    isQuotationOn = true;
                    if (quotationCounter == 1)
                        numericalData += ',';
                    foreach (char c in item)
                    {
                        if(c == '"')
                        {
                            quotationCounter++;
                        } else
                        numericalData += c;

                        if(quotationCounter == 2)
                        {
                            quotationCounter = 0;
                            isQuotationOn = false;
                            fixedDataCollection.Add(numericalData.Replace(" ", "")); /// Removes all white spaces
                            numericalData = string.Empty;
                        }
                    }
                } else
                {
                    fixedDataCollection.Add(item.Trim());
                }
            }
            return fixedDataCollection.ToArray();
        }

    }
}
