using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class ManageVehicleType : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Detailing Management System";
        Setting setting;
        public ManageVehicleType(Setting sett)
        {
            InitializeComponent();
            setting = sett;
            cbClass.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Required vehicle type name!", "Warning");
                    return; // return to the data field and form
                }
                if (MessageBox.Show("Are you sure you want to register this vehicle type?", "Vehicle Type Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbVehicleType(VehicleName, VehicleClass) VALUES(@name, @class)", dbcon.connect());
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class", cbClass.Text);

                    dbcon.open();// to open connection
                    cm.ExecuteNonQuery();
                    dbcon.close();// to close connection
                    MessageBox.Show("Vehicle type has been successfully registered!", title);
                    Clear();//to clear data field, after data inserted into the database
                    setting.loadVehicleType();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Required vehicle type name!", "Warning");
                    return; // return to the data field and form
                }
                if (MessageBox.Show("Are you sure you want to edit this vehicle type?", "Vehicle Type Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbVehicleType SET VehicleName=@name, VehicleClass=@class WHERE VehicleId=@id", dbcon.connect());
                    cm.Parameters.AddWithValue("@id", lblVid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class", cbClass.Text);

                    dbcon.open();// to open connection
                    cm.ExecuteNonQuery();
                    dbcon.close();// to close connection
                    MessageBox.Show("Vehicle type has been successfully Edited!", title);
                    Clear();//to clear data field, after data inserted into the database                                           
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region method
        public void Clear()
        {
            txtName.Clear();
            cbClass.SelectedIndex = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
