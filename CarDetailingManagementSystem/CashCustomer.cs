using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class CashCustomer : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Detailing Management System";
        CashReg cash;
        public CashCustomer(CashReg cashform)
        {
            InitializeComponent();
            cash = cashform;
            loadCustomer();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                cash.customerId = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString());
                cash.vehicleTypeId = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            else return;
            this.Dispose();
            cash.panelCash.Height = 1;
        }

        #region method
        // create function for load customer
        public void loadCustomer()
        {
            try
            {
                int i = 0; // to show number for customer list
                dgvCustomer.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT (CustomerName,CustomerPhone,CustomerAddress) LIKE '%" + textSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    // to add data to the datagridview from the database
                    dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
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
