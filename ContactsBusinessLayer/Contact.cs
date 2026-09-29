using ContactsDateAccessLayer;
using System;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public enum enMode {AddNew =0 , Update =1 };
        public enMode Mode = enMode.AddNew;


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set;}

        public clsContactDataAccess.stInfo DALInfo = new clsContactDataAccess.stInfo();

        private clsContact(clsContactDataAccess.stInfo DALInfo)
        {
            this.ID = DALInfo.ID;
            this.FirstName = DALInfo.FirstName;
            this.LastName = DALInfo.LastName;
            this.Email = DALInfo.Email;
            this.Phone = DALInfo.Phone;
            this.Address = DALInfo.Address;
            this.DateOfBirth = DALInfo.DateOfBirth;
            this.CountryID = DALInfo.CountryID;
            this.ImagePath = DALInfo.ImagePath;

            Mode = enMode.Update;
        }
        private void _FillDALInfoByObject()
        {
            DALInfo.ID = this.ID;
            DALInfo.FirstName = this.FirstName;
            DALInfo.LastName = this.LastName;
            DALInfo.Phone = this.Phone;
            DALInfo.Email = this.Email;
            DALInfo.Address = this.Address;
            DALInfo.DateOfBirth = this.DateOfBirth;
            DALInfo.ImagePath = this.ImagePath;
            DALInfo.CountryID = this.CountryID;
        }
        private bool _AddNewContact() 
        {
            _FillDALInfoByObject();
            
            this.ID = clsContactDataAccess.AddNewContact(DALInfo);
            DALInfo.ID = this.ID;

            return (this.ID != -1);
        }
        private bool _UpdateContact()
        {
            _FillDALInfoByObject();
            return clsContactDataAccess.UpdateContact(DALInfo);
        }

        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.ImagePath = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;

            Mode = enMode.AddNew;

        }

        public static clsContact Find(int ID)
        {
   
            clsContactDataAccess.stInfo DALInfo = new clsContactDataAccess.stInfo();

            if (clsContactDataAccess.GetContactInfoByID(ID, ref DALInfo))
            {
                return new clsContact(DALInfo);
            }
            else
            {
                return null;
            }

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                  

                case enMode.Update:
                    return _UpdateContact();
             
            }

            return false;
        }


        public static bool DelectContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }

        public static DataTable ListContacts()
        {
            return clsContactDataAccess.ListContacts();
        }
        public static bool IsContactExistByID(int ID)
        {
            return clsContactDataAccess.IsContactExistByID(ID);
        }

    }
}
