using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymManagement.WinForms
{
    public class MainForm : Form
    {
        private Button btnManageMembers;
        private Label lblTitle;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Gym Management System - Dashboard";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Gym Management Dashboard";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(50, 30);
            this.Controls.Add(lblTitle);

            btnManageMembers = new Button();
            btnManageMembers.Text = "Manage Members";
            btnManageMembers.Size = new Size(150, 50);
            btnManageMembers.Location = new Point(120, 100);
            btnManageMembers.Click += BtnManageMembers_Click;
            this.Controls.Add(btnManageMembers);
        }

        private void BtnManageMembers_Click(object? sender, EventArgs e)
        {
            MembersForm membersForm = new MembersForm();
            membersForm.ShowDialog(); // Open members form as a dialog
        }
    }
}
