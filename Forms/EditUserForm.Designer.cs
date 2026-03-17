namespace BAMS.Forms
{
    partial class EditUserForm
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
        private void InitializeComponent()
        {
            txtFullName = new TextBox();
            cmbGender = new ComboBox();
            cmbPosition = new ComboBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnSave = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 12F);
            txtFullName.Location = new Point(139, 124);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(293, 29);
            txtFullName.TabIndex = 0;
            txtFullName.Text = "Full Name";
            // 
            // cmbGender
            // 
            cmbGender.Font = new Font("Segoe UI", 12F);
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(139, 164);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(293, 29);
            cmbGender.TabIndex = 1;
            cmbGender.Text = "Gender";
            // 
            // cmbPosition
            // 
            cmbPosition.Font = new Font("Segoe UI", 12F);
            cmbPosition.FormattingEnabled = true;
            cmbPosition.Location = new Point(139, 204);
            cmbPosition.Name = "cmbPosition";
            cmbPosition.Size = new Size(293, 29);
            cmbPosition.TabIndex = 2;
            cmbPosition.Text = "Position";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(139, 244);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(293, 29);
            txtUsername.TabIndex = 3;
            txtUsername.Text = "Username";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(139, 284);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(293, 29);
            txtPassword.TabIndex = 4;
            txtPassword.Text = "Password";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.RoyalBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(227, 334);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(116, 33);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(222, 51);
            label1.Name = "label1";
            label1.Size = new Size(150, 37);
            label1.TabIndex = 6;
            label1.Text = "EDIT USER";
            // 
            // EditUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(588, 450);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(cmbPosition);
            Controls.Add(cmbGender);
            Controls.Add(txtFullName);
            Name = "EditUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit User";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFullName;
        private ComboBox cmbGender;
        private ComboBox cmbPosition;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnSave;
        private Label label1;
    }
}