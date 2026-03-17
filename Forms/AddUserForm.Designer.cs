namespace BAMS.Forms
{
    partial class AddUserForm
    {
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSave = new Button();
            txtFullName = new TextBox();
            cmbGender = new ComboBox();
            cmbPosition = new ComboBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnCancel2 = new Button();
            label1 = new Label();
            txtEmployeeID = new TextBox();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(343, 320);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 34);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 12F);
            txtFullName.Location = new Point(138, 114);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(293, 29);
            txtFullName.TabIndex = 0;
            txtFullName.Text = "Full Name";
            txtFullName.TextChanged += txtFullName_TextChanged;
            // 
            // cmbGender
            // 
            cmbGender.Font = new Font("Segoe UI", 12F);
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(138, 154);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(293, 29);
            cmbGender.TabIndex = 1;
            cmbGender.Text = "Gender";
            // 
            // cmbPosition
            // 
            cmbPosition.Font = new Font("Segoe UI", 12F);
            cmbPosition.FormattingEnabled = true;
            cmbPosition.Location = new Point(138, 194);
            cmbPosition.Name = "cmbPosition";
            cmbPosition.Size = new Size(293, 29);
            cmbPosition.TabIndex = 2;
            cmbPosition.Text = "Position";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(138, 234);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(293, 29);
            txtUsername.TabIndex = 3;
            txtUsername.Text = "Username";
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(138, 274);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(293, 29);
            txtPassword.TabIndex = 4;
            txtPassword.Text = "Password";
            // 
            // btnCancel2
            // 
            btnCancel2.BackColor = Color.Crimson;
            btnCancel2.FlatStyle = FlatStyle.Popup;
            btnCancel2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel2.Location = new Point(138, 320);
            btnCancel2.Name = "btnCancel2";
            btnCancel2.Size = new Size(88, 34);
            btnCancel2.TabIndex = 6;
            btnCancel2.Text = "Cancel";
            btnCancel2.UseVisualStyleBackColor = false;
            btnCancel2.Click += btnCancel2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(214, 24);
            label1.Name = "label1";
            label1.Size = new Size(150, 37);
            label1.TabIndex = 7;
            label1.Text = "ADD USER";
            // 
            // txtEmployeeID
            // 
            txtEmployeeID.Font = new Font("Segoe UI", 12F);
            txtEmployeeID.Location = new Point(138, 79);
            txtEmployeeID.Name = "txtEmployeeID";
            txtEmployeeID.Size = new Size(293, 29);
            txtEmployeeID.TabIndex = 8;
            txtEmployeeID.Text = "Employee ID";
            txtEmployeeID.TextChanged += txtEmployeeID_TextChanged;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(568, 417);
            Controls.Add(txtEmployeeID);
            Controls.Add(label1);
            Controls.Add(btnCancel2);
            Controls.Add(btnSave);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(cmbPosition);
            Controls.Add(cmbGender);
            Controls.Add(txtFullName);
            Name = "AddUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add User";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSave;
        private TextBox txtFullName;
        private ComboBox cmbGender;
        private ComboBox cmbPosition;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnCancel2;
        private Label label1;
        private TextBox txtEmployeeID;
    }
}