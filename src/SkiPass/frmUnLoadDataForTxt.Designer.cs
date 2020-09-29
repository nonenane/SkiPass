namespace SkiPass
{
    partial class frmUnLoadDataForTxt
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
            this.btUnLoad = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btUnLoad
            // 
            this.btUnLoad.BackgroundImage = global::SkiPass.Properties.Resources.чума;
            this.btUnLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btUnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btUnLoad.ForeColor = System.Drawing.Color.Red;
            this.btUnLoad.Location = new System.Drawing.Point(12, 12);
            this.btUnLoad.Name = "btUnLoad";
            this.btUnLoad.Size = new System.Drawing.Size(284, 146);
            this.btUnLoad.TabIndex = 8;
            this.btUnLoad.Text = "Выгрузить";
            this.btUnLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btUnLoad.UseVisualStyleBackColor = true;
            this.btUnLoad.Click += new System.EventHandler(this.btUnLoad_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::SkiPass.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(333, 124);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 7;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmUnLoadDataForTxt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 168);
            this.Controls.Add(this.btUnLoad);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUnLoadDataForTxt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выгрузить список сотрудников с бейджиками";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btUnLoad;
    }
}