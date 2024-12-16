using System;
using System.Windows.Forms;

namespace CarDetailingManagementSystem
{
    public partial class MainForm : Form
    {
        private User currentUser;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            ConfigureMenuAccess();
            loadGrossProfit();
            openChildForm(new Dashboard());
        }

        private void ConfigureMenuAccess()
        {
            if (currentUser.Role == "Cashier")
            {
                btnEmployer.Enabled = false;
                btnService.Enabled = false;
                btnReport.Enabled = false;
                btnSetting.Enabled = false;
            }
            else if (currentUser.Role == "Manager")
            {
                btnSetting.Enabled = false;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnDashboard.Height;
            panelSlide.Top = btnDashboard.Top;
            openChildForm(new Dashboard());
        }

        private void btnEmployer_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnEmployer.Height;
            panelSlide.Top = btnEmployer.Top;
            openChildForm(new Employee());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnCustomer.Height;
            panelSlide.Top = btnCustomer.Top;
            openChildForm(new Customer());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnService.Height;
            panelSlide.Top = btnService.Top;
            openChildForm(new Service());
        }

        private void btncash_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btncash.Height;
            panelSlide.Top = btncash.Top;
            openChildForm(new CashReg(this));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnReport.Height;
            panelSlide.Top = btnReport.Top;
            openChildForm(new Report());
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnSetting.Height;
            panelSlide.Top = btnSetting.Top;
            openChildForm(new Setting());

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            panelSlide.Height = btnLogout.Height;
            panelSlide.Top = btnLogout.Top;
            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
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
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // to extract data for dashboard
        // to show only seven day
        public void loadGrossProfit()
        {
            Report module = new Report();
            lblRevenus.Text = module.extractData("SELECT ISNULL(SUM(Price),0) AS total FROM tbCashReg WHERE CashDate >'" + DateTime.Now.AddDays(-7) + "' AND CashStatus LIKE 'Sold' ").ToString("#,##0.00");
            lblCostofGood.Text = module.extractData("SELECT ISNULL(SUM(ExpenseCost),0) AS cost FROM tbExpense WHERE ExpenseDate > '" + DateTime.Now.AddDays(-7) + "'").ToString("#,##0.00");
            lblGrossProfit.Text = (double.Parse(lblRevenus.Text) - double.Parse(lblCostofGood.Text)).ToString("#,##0.00");

            double revlast7 = module.extractData("SELECT ISNULL(SUM(Price),0) AS total FROM tbCashReg WHERE CashDate BETWEEN '" + DateTime.Now.AddDays(-14) + "' AND '" + DateTime.Now.AddDays(-7) + "' AND CashStatus LIKE 'Sold' ");
            double coglast7 = module.extractData("SELECT ISNULL(SUM(ExpenseCost),0) AS cost FROM tbExpense WHERE ExpenseDate BETWEEN  '" + DateTime.Now.AddDays(-14) + "' AND '" + DateTime.Now.AddDays(-7) + "'");
            double gplast7 = revlast7 - coglast7;

        }
        #endregion method

    }
}
