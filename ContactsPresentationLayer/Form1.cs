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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void _RefreshContactsList()
        {
            dataGridView1.DataSource = clsContact.ListContacts();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }
    }
}
