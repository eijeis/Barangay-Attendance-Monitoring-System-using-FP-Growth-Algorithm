using BAMS.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BAMS.Forms
{
    public partial class AddUserForm : Form
    {
        private int userId;

        public AddUserForm(int id = 0)
        {
            InitializeComponent();

            userId = id;

            SetupComboBoxes();
            SetupPlaceholders();

            if (userId != 0)
                LoadUserData();
        }

        private void SetupComboBoxes()
        {
            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[]
            {
                "Select Gender",
                "Male",
                "Female"
            });

            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.SelectedIndex = 0;

            cmbPosition.Items.Clear();
            cmbPosition.Items.AddRange(new object[]
            {
                "Select Position",
                "Admin",
                "Staff",
                "Official"
            });

            cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPosition.SelectedIndex = 0;
        }

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtEmployeeID, "Employee ID");
            SetPlaceholder(txtFullName, "Full Name");
            SetPlaceholder(txtUsername, "Username");
            SetPlaceholder(txtPassword, "Password");
        }

        private void SetPlaceholder(TextBox box, string placeholder)
        {
            box.Text = placeholder;
            box.ForeColor = Color.Gray;

            box.Enter += (s, e) =>
            {
                if (box.Text == placeholder)
                {
                    box.Text = "";
                    box.ForeColor = Color.Black;

                    if (box == txtPassword)
                        box.UseSystemPasswordChar = true;
                }
            };

            box.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    if (box == txtPassword)
                        box.UseSystemPasswordChar = false;

                    box.Text = placeholder;
                    box.ForeColor = Color.Gray;
                }
            };
        }

        private void LoadUserData()
        {
            try
            {
                UserRepository repo = new UserRepository();
                DataTable dt = repo.GetUserById(userId);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("User not found.");
                    return;
                }

                DataRow row = dt.Rows[0];

                txtEmployeeID.ForeColor = Color.Black;
                txtFullName.ForeColor = Color.Black;
                txtUsername.ForeColor = Color.Black;
                txtPassword.ForeColor = Color.Black;

                txtEmployeeID.Text = row["EmployeeID"]?.ToString();
                txtFullName.Text = row["FullName"]?.ToString();
                txtUsername.Text = row["Username"]?.ToString();
                txtPassword.Text = row["Password"]?.ToString();

                cmbGender.SelectedItem = row["Gender"]?.ToString();
                cmbPosition.SelectedItem = row["Position"]?.ToString();

                txtPassword.UseSystemPasswordChar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user:\n" + ex.Message);
            }
        }

        private bool ValidateForm()
        {
            if (txtEmployeeID.ForeColor == Color.Gray ||
                txtFullName.ForeColor == Color.Gray ||
                txtUsername.ForeColor == Color.Gray ||
                txtPassword.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            if (cmbGender.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a gender.");
                return false;
            }

            if (cmbPosition.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a position.");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (!int.TryParse(txtEmployeeID.Text, out int employeeId))
            {
                MessageBox.Show("Employee ID must be a number.");
                return;
            }

            try
            {
                UserRepository repo = new UserRepository();

                if (userId == 0)
                {
                    repo.AddUser(
                        employeeId,
                        txtFullName.Text.Trim(),
                        cmbGender.Text,
                        cmbPosition.Text,
                        txtUsername.Text.Trim(),
                        txtPassword.Text
                    );

                    MessageBox.Show(
                        "User added successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    repo.UpdateUser(
                        userId,
                        txtFullName.Text.Trim(),
                        cmbGender.Text,
                        cmbPosition.Text,
                        txtUsername.Text.Trim(),
                        txtPassword.Text
                    );

                    MessageBox.Show(
                        "User updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error saving user:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}