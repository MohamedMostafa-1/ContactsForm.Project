using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace ContactsPresentationLayer
{
    public partial class frmAddEditContacts : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        int _ContactID;
        clsContact _Contact;


        public frmAddEditContacts(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;

            if (_ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            
        }
        private void _FillCountriesInComoboBox() {

            DataTable dtCountries = clsCountries.ListCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

        }

       
    }
}
