namespace SkiPass
{
    partial class frmAddCar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.lFullName = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.lShortName = new System.Windows.Forms.Label();
            this.tbShortName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::SkiPass.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(427, 155);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::SkiPass.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(389, 155);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 0;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сотрудник";
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(191, 17);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.ReadOnly = true;
            this.tbFIO.Size = new System.Drawing.Size(264, 20);
            this.tbFIO.TabIndex = 2;
            // 
            // lFullName
            // 
            this.lFullName.Location = new System.Drawing.Point(24, 43);
            this.lFullName.Name = "lFullName";
            this.lFullName.Size = new System.Drawing.Size(156, 41);
            this.lFullName.TabIndex = 1;
            this.lFullName.Text = "Полное наименование марки/номера а/м";
            this.lFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbFullName
            // 
            this.tbFullName.Location = new System.Drawing.Point(191, 43);
            this.tbFullName.Multiline = true;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(264, 41);
            this.tbFullName.TabIndex = 2;
            this.tbFullName.TextChanged += new System.EventHandler(this.tbFullName_TextChanged);
            // 
            // lShortName
            // 
            this.lShortName.Location = new System.Drawing.Point(24, 90);
            this.lShortName.Name = "lShortName";
            this.lShortName.Size = new System.Drawing.Size(156, 41);
            this.lShortName.TabIndex = 1;
            this.lShortName.Text = "Краткое наименование марки/номера а/м";
            this.lShortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbShortName
            // 
            this.tbShortName.Location = new System.Drawing.Point(191, 90);
            this.tbShortName.Multiline = true;
            this.tbShortName.Name = "tbShortName";
            this.tbShortName.Size = new System.Drawing.Size(264, 41);
            this.tbShortName.TabIndex = 2;
            this.tbShortName.TextChanged += new System.EventHandler(this.tbFullName_TextChanged);
            // 
            // frmAddCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 199);
            this.ControlBox = false;
            this.Controls.Add(this.tbShortName);
            this.Controls.Add(this.lShortName);
            this.Controls.Add(this.tbFullName);
            this.Controls.Add(this.lFullName);
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddCar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddCar_FormClosing);
            this.Load += new System.EventHandler(this.frmAddCar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.Label lFullName;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.Label lShortName;
        private System.Windows.Forms.TextBox tbShortName;
    }
}

