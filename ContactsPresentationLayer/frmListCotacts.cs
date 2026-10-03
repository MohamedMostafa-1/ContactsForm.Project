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
    public partial class frmListCotacts : Form
    {
        public frmListCotacts()
        {
            InitializeComponent();
        }

        private void _RefreshContactsList()
        {
            dgvAllContacts.DataSource = clsContact.ListContacts();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddAndEditContacts = new frmAddEditContacts((int)dgvAllContacts.CurrentRow.Cells["ContactID"].Value);
            frmAddAndEditContacts.ShowDialog();

            _RefreshContactsList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvAllContacts.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel ) == DialogResult.OK)
            {
                if (clsContact.DelectContact((int)dgvAllContacts.CurrentRow.Cells["ContactID"].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshContactsList();
                }
                else
                    MessageBox.Show("Contact is not deleted.");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frmAddNewContact = new frmAddEditContacts(-1);
            frmAddNewContact.ShowDialog();


            _RefreshContactsList();
        }
    }
}
