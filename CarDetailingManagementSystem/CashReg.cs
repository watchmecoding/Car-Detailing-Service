using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class CashReg : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Detailing Management System";
        public int customerId = 0, vehicleTypeId = 0;
        public string carno, carmodel;
        MainForm main;
        public CashReg(MainForm mainForm)
        {
            InitializeComponent();
            getTransno();
            loadCash();
            main = mainForm;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CashCustomer(this));
            btnAddService.Enabled = true;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            openChildForm(new CashService(this));
            btnAddCustomer.Enabled = false;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            SettlePayment module = new SettlePayment(this);
            module.txtSale.Text = lblTotal.Text;
            module.ShowDialog();
            main.loadGrossProfit();
        }

        #region method
        // create a function any form to the panelChild on the mainform

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelCash.Height = 200;
            panelCash.Controls.Add(childForm);
            panelCash.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Create a function for transatoin generator depend on date

        public void getTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;


                dbcon.open();
                cm = new SqlCommand("SELECT TOP 1 TransactionNumber FROM tbCashReg WHERE TransactionNumber LIKE '" + sdate + "%' ORDER BY CashId DESC", dbcon.connect());
                dr = cm.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }

                dbcon.close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCash.Columns[e.ColumnIndex].Name;
            if (colName == "Delete") // if you want to delete the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to cancle this service?", "Cancel Services", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbCashReg WHERE CashId LIKE'" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Service has been successfully Canceled!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadCash();
        }

        private void Cash_Load(object sender, EventArgs e)
        {
        }

        public void loadCash()
        {
            int i = 0;
            double total = 0;
            double price = 0;
            dgvCash.Rows.Clear();

            cm = new SqlCommand("SELECT CashId, TransactionNumber, Cu.CustomerName, Cu.CustomerCarNumber, Cu.CustomerCarModel, v.VehicleName, v.VehicleClass, s.ServiceName,Price, CashDate FROM tbCashReg AS Ca " +
                "LEFT JOIN tbCustomer AS Cu ON Ca.CustomerId = Cu.CustomerId LEFT JOIN tbService AS s ON Ca.ServiceId = s.ServiceId LEFT JOIN tbVehicleType AS v ON Ca.VehicleId = v.VehicleId WHERE CashStatus LIKE 'Pending' AND TransactionNumber='" + lblTransno.Text + "'", dbcon.connect());

            dbcon.open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                price = int.Parse(dr[6].ToString()) * double.Parse(dr[8].ToString());
                dgvCash.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), price, dr[9].ToString());
                total += price;
                carno = dr[3].ToString();
                carmodel = dr[4].ToString();
            }

            dr.Close();
            dbcon.close();
            lblTotal.Text = total.ToString("#,##0.00");
        }
        #endregion method
    }

}
