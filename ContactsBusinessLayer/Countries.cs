using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ContactsDateAccessLayer;


namespace ContactsBusinessLayer
{
    public class clsCountries
    {
        public enum enMode
        {
            AddNew =0,
            Update =1
        }
        private enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string CountryName { get; set; }

        public clsCountriesData.stCountryInfo DALCountryInfo = new clsCountriesData.stCountryInfo();

        public clsCountries()
        {
            ID = -1;
            CountryName = "";
            Mode = enMode.AddNew;
        }
        private clsCountries(clsCountriesData.stCountryInfo DALCountryInfo)
        {
            ID = DALCountryInfo.ID;
            CountryName = DALCountryInfo.Name;
            Mode = enMode.Update;
        }


        public static clsCountries Find(int ID)
        {
            clsCountriesData.stCountryInfo Country = new clsCountriesData.stCountryInfo();

            if(clsCountriesData.GetCountryByID(ID, ref Country))
            {
                return new clsCountries(Country);
            }
            else
            {
                return null;
            }

        }

        public static clsCountries Find(string CountryName)
        {
            clsCountriesData.stCountryInfo Country = new clsCountriesData.stCountryInfo();

            if (clsCountriesData.GetCountryByName(CountryName, ref Country))
                return new clsCountries(Country);
            else
                return null;
        }

        public static bool isExistCountryByID (int ID)
        {
            return clsCountriesData.isExistCountryByID(ID);
        }
        public static bool testIsCountryExistByName(string CountryName)
        {
            return clsCountriesData.testIsCountryExistByName(CountryName);
        }


        //Privet Methods
        private void _FillDALCountryInfoByObject()
        {
            DALCountryInfo.ID = this.ID;
            DALCountryInfo.Name = this.CountryName;
        }
        private bool _AddNewCountry()
        {
            _FillDALCountryInfoByObject();

            this.ID = clsCountriesData.AddNewCountry(DALCountryInfo);
            DALCountryInfo.ID = this.ID;

            return (this.ID != -1) ;
        }
        private bool _UpdateCountry()
        {
            _FillDALCountryInfoByObject();
            return clsCountriesData.UpdateCountry(DALCountryInfo);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();

              
            }
            return false;
        }


        public static bool DeleteCountry(int CountryID)
        {
            return clsCountriesData.DeleteCountry(CountryID);
        }

        public static DataTable ListCountries()
        {
            return clsCountriesData.ListCountries();
        }

    }
}
