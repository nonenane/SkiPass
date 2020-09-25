namespace SkiPass
{
    partial class frmListCar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPost = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOffice = new System.Windows.Forms.RadioButton();
            this.rbUni = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbUnemploy = new System.Windows.Forms.RadioButton();
            this.rbWork = new System.Windows.Forms.RadioButton();
            this.tbPostName = new System.Windows.Forms.TextBox();
            this.tbKadrName = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateUnemploy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDatePrintPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbContainePass = new System.Windows.Forms.CheckBox();
            this.tbEditor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDateEdit = new System.Windows.Forms.TextBox();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btPrintPass = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отдел";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(53, 23);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(198, 21);
            this.cmbDeps.TabIndex = 1;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Должность";
            // 
            // cmbPost
            // 
            this.cmbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPost.FormattingEnabled = true;
            this.cmbPost.Location = new System.Drawing.Point(340, 23);
            this.cmbPost.Name = "cmbPost";
            this.cmbPost.Size = new System.Drawing.Size(198, 21);
            this.cmbPost.TabIndex = 1;
            this.cmbPost.SelectionChangeCommitted += new System.EventHandler(this.cmbPost_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOffice);
            this.groupBox1.Controls.Add(this.rbUni);
            this.groupBox1.Location = new System.Drawing.Point(544, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 45);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Место работы";
            // 
            // rbOffice
            // 
            this.rbOffice.AutoSize = true;
            this.rbOffice.Location = new System.Drawing.Point(104, 19);
            this.rbOffice.Name = "rbOffice";
            this.rbOffice.Size = new System.Drawing.Size(53, 17);
            this.rbOffice.TabIndex = 0;
            this.rbOffice.Text = "Офис";
            this.rbOffice.UseVisualStyleBackColor = true;
            this.rbOffice.Click += new System.EventHandler(this.rbOffice_Click);
            // 
            // rbUni
            // 
            this.rbUni.AutoSize = true;
            this.rbUni.Checked = true;
            this.rbUni.Location = new System.Drawing.Point(15, 19);
            this.rbUni.Name = "rbUni";
            this.rbUni.Size = new System.Drawing.Size(83, 17);
            this.rbUni.TabIndex = 0;
            this.rbUni.TabStop = true;
            this.rbUni.Text = "Универсам";
            this.rbUni.UseVisualStyleBackColor = true;
            this.rbUni.CheckedChanged += new System.EventHandler(this.rbUni_CheckedChanged);
            this.rbUni.Click += new System.EventHandler(this.rbUni_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbUnemploy);
            this.groupBox2.Controls.Add(this.rbWork);
            this.groupBox2.Location = new System.Drawing.Point(736, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 45);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Статус сотрудника";
            // 
            // rbUnemploy
            // 
            this.rbUnemploy.AutoSize = true;
            this.rbUnemploy.Location = new System.Drawing.Point(104, 19);
            this.rbUnemploy.Name = "rbUnemploy";
            this.rbUnemploy.Size = new System.Drawing.Size(83, 17);
            this.rbUnemploy.TabIndex = 1;
            this.rbUnemploy.Text = "Уволенные";
            this.rbUnemploy.UseVisualStyleBackColor = true;
            // 
            // rbWork
            // 
            this.rbWork.AutoSize = true;
            this.rbWork.Checked = true;
            this.rbWork.Location = new System.Drawing.Point(15, 19);
            this.rbWork.Name = "rbWork";
            this.rbWork.Size = new System.Drawing.Size(90, 17);
            this.rbWork.TabIndex = 2;
            this.rbWork.TabStop = true;
            this.rbWork.Text = "Работающие";
            this.rbWork.UseVisualStyleBackColor = true;
            this.rbWork.CheckedChanged += new System.EventHandler(this.rbWork_CheckedChanged);
            // 
            // tbPostName
            // 
            this.tbPostName.Location = new System.Drawing.Point(497, 62);
            this.tbPostName.Name = "tbPostName";
            this.tbPostName.Size = new System.Drawing.Size(138, 20);
            this.tbPostName.TabIndex = 3;
            this.tbPostName.TextChanged += new System.EventHandler(this.tbPostName_TextChanged);
            // 
            // tbKadrName
            // 
            this.tbKadrName.Location = new System.Drawing.Point(641, 62);
            this.tbKadrName.Name = "tbKadrName";
            this.tbKadrName.Size = new System.Drawing.Size(138, 20);
            this.tbKadrName.TabIndex = 3;
            this.tbKadrName.TextChanged += new System.EventHandler(this.tbPostName_TextChanged);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cV,
            this.cDeps,
            this.cFIO,
            this.cPost,
            this.cPass,
            this.cDateUnemploy,
            this.cDatePrintPass,
            this.cPhone});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(15, 88);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(950, 476);
            this.dgvData.TabIndex = 4;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // cV
            // 
            this.cV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cV.DataPropertyName = "isSelect";
            this.cV.HeaderText = "V";
            this.cV.MinimumWidth = 45;
            this.cV.Name = "cV";
            this.cV.Visible = false;
            this.cV.Width = 45;
            // 
            // cDeps
            // 
            this.cDeps.DataPropertyName = "nameDep";
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // cFIO
            // 
            this.cFIO.DataPropertyName = "fio";
            this.cFIO.HeaderText = "ФИО";
            this.cFIO.Name = "cFIO";
            this.cFIO.ReadOnly = true;
            // 
            // cPost
            // 
            this.cPost.DataPropertyName = "namePost";
            this.cPost.HeaderText = "Должность";
            this.cPost.Name = "cPost";
            this.cPost.ReadOnly = true;
            // 
            // cPass
            // 
            this.cPass.DataPropertyName = "FullNameCar";
            this.cPass.HeaderText = "Пропуск";
            this.cPass.Name = "cPass";
            this.cPass.ReadOnly = true;
            // 
            // cDateUnemploy
            // 
            this.cDateUnemploy.DataPropertyName = "dateUnemploy";
            this.cDateUnemploy.HeaderText = "Дата увольнения";
            this.cDateUnemploy.Name = "cDateUnemploy";
            this.cDateUnemploy.ReadOnly = true;
            // 
            // cDatePrintPass
            // 
            this.cDatePrintPass.DataPropertyName = "datePrintPass";
            this.cDatePrintPass.HeaderText = "Дата печати пропуска";
            this.cDatePrintPass.Name = "cDatePrintPass";
            this.cDatePrintPass.ReadOnly = true;
            this.cDatePrintPass.Visible = false;
            // 
            // cPhone
            // 
            this.cPhone.DataPropertyName = "comment";
            this.cPhone.HeaderText = "Телефон";
            this.cPhone.Name = "cPhone";
            this.cPhone.ReadOnly = true;
            this.cPhone.Visible = false;
            // 
            // chbContainePass
            // 
            this.chbContainePass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbContainePass.AutoSize = true;
            this.chbContainePass.Location = new System.Drawing.Point(15, 572);
            this.chbContainePass.Name = "chbContainePass";
            this.chbContainePass.Size = new System.Drawing.Size(155, 17);
            this.chbContainePass.TabIndex = 5;
            this.chbContainePass.Text = "сотрудники без пропуска";
            this.chbContainePass.UseVisualStyleBackColor = true;
            this.chbContainePass.CheckedChanged += new System.EventHandler(this.chbContainePass_CheckedChanged);
            // 
            // tbEditor
            // 
            this.tbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEditor.Location = new System.Drawing.Point(359, 570);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.ReadOnly = true;
            this.tbEditor.Size = new System.Drawing.Size(265, 20);
            this.tbEditor.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 574);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Редактировал";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 600);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Дата редактирования";
            // 
            // tbDateEdit
            // 
            this.tbDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateEdit.Location = new System.Drawing.Point(359, 596);
            this.tbDateEdit.Name = "tbDateEdit";
            this.tbDateEdit.ReadOnly = true;
            this.tbDateEdit.Size = new System.Drawing.Size(265, 20);
            this.tbDateEdit.TabIndex = 3;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::SkiPass.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(940, 22);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(32, 32);
            this.btUpdate.TabIndex = 8;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.Image = global::SkiPass.Properties.Resources.Trash;
            this.btDel.Location = new System.Drawing.Point(895, 584);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 7;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::SkiPass.Properties.Resources.parking_car_park;
            this.btAdd.Location = new System.Drawing.Point(857, 584);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 7;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btPrintPass
            // 
            this.btPrintPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrintPass.Image = global::SkiPass.Properties.Resources.driver_s_license_computer_icons_driving;
            this.btPrintPass.Location = new System.Drawing.Point(819, 584);
            this.btPrintPass.Name = "btPrintPass";
            this.btPrintPass.Size = new System.Drawing.Size(32, 32);
            this.btPrintPass.TabIndex = 6;
            this.btPrintPass.UseVisualStyleBackColor = true;
            this.btPrintPass.Click += new System.EventHandler(this.btPrintPass_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::SkiPass.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(895, 584);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 6;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::SkiPass.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(933, 584);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 6;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmListCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 630);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btPrintPass);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.chbContainePass);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbKadrName);
            this.Controls.Add(this.tbDateEdit);
            this.Controls.Add(this.tbEditor);
            this.Controls.Add(this.tbPostName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListCar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmListCar_FormClosing);
            this.Load += new System.EventHandler(this.frmListCar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOffice;
        private System.Windows.Forms.RadioButton rbUni;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbUnemploy;
        private System.Windows.Forms.RadioButton rbWork;
        private System.Windows.Forms.TextBox tbPostName;
        private System.Windows.Forms.TextBox tbKadrName;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chbContainePass;
        private System.Windows.Forms.TextBox tbEditor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDateEdit;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btPrintPass;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cV;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateUnemploy;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDatePrintPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPhone;
        private System.Windows.Forms.Button btUpdate;
    }
}