using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class ManageExpenses : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Detailing Management System";
        Setting setting;
        bool check = false;
        public ManageExpenses(Setting sett)
        {
            InitializeComponent();
            setting = sett;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this expense?", "Expenses Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbExpense(ExpenseName, ExpenseCost, ExpenseDate) VALUES(@ExpenseName, @cost, @date)", dbcon.connect());
                        cm.Parameters.AddWithValue("@ExpenseName", txtExpenseName.Text);
                        cm.Parameters.AddWithValue("@cost", txtCost.Text);
                        cm.Parameters.AddWithValue("@date", dtCoG.Value);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("An expense has been successfully registered!", title);
                        Clear();//to clear data field, after data inserted into the database
                        setting.loadCostofGood();
                    }
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
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to edit this expense?", "Expenses Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbExpense SET ExpenseName=@ExpenseName, ExpenseCost=@cost, ExpenseDate=@date WHERE ExpenseId=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblCid.Text);
                        cm.Parameters.AddWithValue("@ExpenseName", txtExpenseName.Text);
                        cm.Parameters.AddWithValue("@cost", txtCost.Text);
                        cm.Parameters.AddWithValue("@date", dtCoG.Value);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("An expense has been successfully Edited!", title);
                        Clear();//to clear data field, after data inserted into the database
                        this.Dispose();
                    }
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

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal 
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        #region method

        public void checkField()
        {
            if (txtExpenseName.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return; // return to the data field and form
            }
            check = true;
        }

        public void Clear()
        {
            txtCost.Clear();
            txtExpenseName.Clear();
            dtCoG.Value = DateTime.Now;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method


    }
}
