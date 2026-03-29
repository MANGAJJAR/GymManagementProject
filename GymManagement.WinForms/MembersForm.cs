using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GymManagement.WinForms
{
    public class MembersForm : Form
    {
        private DataGridView dgvMembers;
        private TextBox txtName, txtAge, txtPhone, txtPlanId;
        private Button btnAdd, btnUpdate, btnDelete, btnLoad;
        private Label lblName, lblAge, lblPhone, lblPlanId;
        private int selectedMemberId = 0;

        public MembersForm()
        {
            InitializeComponent();
            LoadData(); // Load data on startup
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Members";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            // DataGridView setup
            dgvMembers = new DataGridView();
            dgvMembers.Location = new Point(20, 20);
            dgvMembers.Size = new Size(740, 250);
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.ReadOnly = true;
            dgvMembers.CellClick += DgvMembers_CellClick;
            this.Controls.Add(dgvMembers);

            // Labels and TextBoxes setup
            int startX = 20, startY = 290, ySpacing = 30;

            lblName = new Label() { Text = "Name:", Location = new Point(startX, startY), AutoSize = true };
            txtName = new TextBox() { Location = new Point(startX + 80, startY), Width = 150 };
            
            lblAge = new Label() { Text = "Age:", Location = new Point(startX, startY + ySpacing), AutoSize = true };
            txtAge = new TextBox() { Location = new Point(startX + 80, startY + ySpacing), Width = 150 };

            lblPhone = new Label() { Text = "Phone:", Location = new Point(startX, startY + ySpacing * 2), AutoSize = true };
            txtPhone = new TextBox() { Location = new Point(startX + 80, startY + ySpacing * 2), Width = 150 };

            lblPlanId = new Label() { Text = "Plan ID:", Location = new Point(startX, startY + ySpacing * 3), AutoSize = true };
            txtPlanId = new TextBox() { Location = new Point(startX + 80, startY + ySpacing * 3), Width = 150 };

            this.Controls.Add(lblName); this.Controls.Add(txtName);
            this.Controls.Add(lblAge); this.Controls.Add(txtAge);
            this.Controls.Add(lblPhone); this.Controls.Add(txtPhone);
            this.Controls.Add(lblPlanId); this.Controls.Add(txtPlanId);

            // Buttons
            int btnX = 300;
            btnAdd = new Button() { Text = "Add", Location = new Point(btnX, startY), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = new Button() { Text = "Update", Location = new Point(btnX + 110, startY), Width = 100 };
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = new Button() { Text = "Delete", Location = new Point(btnX + 220, startY), Width = 100 };
            btnDelete.Click += BtnDelete_Click;

            btnLoad = new Button() { Text = "Refresh", Location = new Point(btnX, startY + ySpacing), Width = 100 };
            btnLoad.Click += (s, e) => LoadData();

            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnLoad);
        }

        private void LoadData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT \"MemberId\", \"Name\", \"Age\", \"Phone\", \"JoinDate\", \"TrainerId\", \"PlanId\" FROM \"Members\" ORDER BY \"MemberId\";";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        var adapter = new NpgsqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgvMembers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvMembers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMembers.Rows[e.RowIndex];
                selectedMemberId = Convert.ToInt32(row.Cells["MemberId"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtPlanId.Text = row.Cells["PlanId"].Value.ToString();
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO \"Members\" (\"Name\", \"Age\", \"Phone\", \"PlanId\", \"JoinDate\") VALUES (@name, @age, @phone, @planId, CURRENT_DATE);";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("name", txtName.Text);
                        cmd.Parameters.AddWithValue("age", int.Parse(txtAge.Text));
                        cmd.Parameters.AddWithValue("phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("planId", int.Parse(txtPlanId.Text));

                        cmd.ExecuteNonQuery(); // Required by syllabus
                    }
                }
                MessageBox.Show("Member Added Successfully!");
                LoadData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Make sure Plan ID exists. " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (selectedMemberId == 0) return;

            try
            {
                using (var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE \"Members\" SET \"Name\"=@name, \"Age\"=@age, \"Phone\"=@phone, \"PlanId\"=@planId WHERE \"MemberId\"=@id;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("name", txtName.Text);
                        cmd.Parameters.AddWithValue("age", int.Parse(txtAge.Text));
                        cmd.Parameters.AddWithValue("phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("planId", int.Parse(txtPlanId.Text));
                        cmd.Parameters.AddWithValue("id", selectedMemberId);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Member Updated Successfully!");
                LoadData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (selectedMemberId == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this member?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM \"Members\" WHERE \"MemberId\"=@id;";
                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("id", selectedMemberId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Member Deleted Successfully!");
                    LoadData();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message);
                }
            }
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtPhone.Text = "";
            txtPlanId.Text = "";
            selectedMemberId = 0;
        }
    }
}
