using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class Service : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Detailing Management System";
        public Service()
        {
            InitializeComponent();
            loadService();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceModule module = new ServiceModule(this);
            module.btnUpdate.Enabled = true;
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadService();
        }

        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvService.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to send vehicle data to the vehicle module 
                ServiceModule module = new ServiceModule(this);
                module.lblSid.Text = dgvService.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvService.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPrice.Text = dgvService.Rows[e.RowIndex].Cells[3].Value.ToString();


                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbService WHERE ServiceId LIKE'" + dgvService.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Service type data has been successfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadService();// to reload the service list after update the record
        }
        #region method
        public void loadService()
        {
            try
            {
                int i = 0; // to show number for service list
                dgvService.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbService WHERE ServiceName LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    // to add data to the datagridview from the database
                    dgvService.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion method


    }
}
