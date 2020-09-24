using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiPass
{
    public partial class frmListCar : Form
    {
        private DataTable dtData;
        public frmListCar()
        {
            InitializeComponent();
            this.Text = "\"" + Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "\", \"" + Nwuram.Framework.Settings.User.UserSettings.User.Status + "\", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + "";
            dgvData.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose,"Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btPrintPass, "Печать пропуска");
            tp.SetToolTip(btAdd, "Добавить/Редактировать");
            tp.SetToolTip(btDel, "Удалить");

            groupBox2.Visible = btPrint.Visible = new List<string>(new string[] { "КНТ" }).Contains(UserSettings.User.StatusCode);
            cV.Visible = btAdd.Visible = btDel.Visible = new List<string>(new string[] { "СК2" }).Contains(UserSettings.User.StatusCode);
        }

        private void frmListCar_Load(object sender, EventArgs e)
        {
            cDateUnemploy.Visible = rbUnemploy.Checked;
            getDeps();
            getData();
        }

        private void getDeps()
        {
            Task<DataTable> task = Config.hCntMain.getDeps(rbOffice.Checked, true);
            task.Wait();
            DataTable dtDeps = task.Result;
            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "dep_name";
            cmbDeps.ValueMember = "id_dep";

            getPost();
        }

        private void getPost()
        {
            if (cmbDeps.SelectedValue == null) return;

            int id_dep = (Int16)cmbDeps.SelectedValue;

            Task<DataTable> task = Config.hCntMain.getPosts(id_dep, true);
            task.Wait();

            DataTable dtPost = task.Result;
            cmbPost.DataSource = dtPost;
            cmbPost.DisplayMember = "cName";
            cmbPost.ValueMember = "id";

        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getPost();
            setFilter();
        }

        private void rbUni_CheckedChanged(object sender, EventArgs e)
        {
            getDeps();
            getData();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (col.Visible)
                {
                    if (col.Name.Equals(cPost.Name))
                    {
                        tbPostName.Location = new Point(dgvData.Location.X + 1 + width, tbPostName.Location.Y);
                        tbPostName.Size = new Size(cPost.Width, tbPostName.Size.Height);
                    }
                    else
                        if (col.Name.Equals(cFIO.Name))
                    {
                        tbKadrName.Location = new Point(dgvData.Location.X + 1 + width, tbPostName.Location.Y);
                        tbKadrName.Size = new Size(cFIO.Width, tbPostName.Size.Height);
                    }


                    width += col.Width;
                }
            }
        }

        private void rbWork_CheckedChanged(object sender, EventArgs e)
        {
            cDateUnemploy.Visible = rbUnemploy.Checked;
            getData();
        }

        private void getData()
        {
            int id_PersonnelType = rbUni.Checked ? 2 : 1;
            int id_WorkStatus = rbWork.Checked ? 4 : 5;

            Task<DataTable> task = Config.hCntMain.getListKadrVsCar(id_WorkStatus, id_PersonnelType);
            task.Wait();
            dtData = task.Result;
            setFilter();

            dgvData.DataSource = dtData;

        }

        private void setFilter()
        {

            if (dtData == null || dtData.Rows.Count == 0)
            {
                btAdd.Enabled = btDel.Enabled = btPrintPass.Enabled = btPrint.Enabled = false;
                return;
            }

            try
            {
                string filter = "";


                if (cmbDeps.SelectedValue != null && (Int16)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_departments = {cmbDeps.SelectedValue}";

                if (cmbPost.SelectedValue != null && (Int16)cmbPost.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_posts = {cmbPost.SelectedValue}";

                if (tbKadrName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + string.Format("fio like '%{0}%'", tbKadrName.Text.Trim());

                if (tbPostName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + string.Format("namePost like '%{0}%'", tbPostName.Text.Trim());

                if (chbContainePass.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"FullNameCar is null";
                else
                    filter += (filter.Length == 0 ? "" : " and ") + $"FullNameCar is not null";

                dtData.DefaultView.RowFilter = filter;

            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btAdd.Enabled = btDel.Enabled =
                btPrintPass.Enabled = btPrint.Enabled =
                                  dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0)
            {
                tbDateEdit.Text = tbEditor.Text = "";
                //btPrintPass.Enabled = btPrint.Enabled = false;
                return;
            }

            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            tbEditor.Text = row["nameEditor"] != DBNull.Value ? (string)row["nameEditor"] : "";
            tbDateEdit.Text = row["DateEdit"] != DBNull.Value ? ((DateTime)row["DateEdit"]).ToString() : "";

            btDel.Enabled = btPrintPass.Enabled = row["FullNameCar"] != DBNull.Value;
            btPrint.Enabled = true;
        }

        private void tbPostName_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbPost_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void chbContainePass_CheckedChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btPrintPass_Click(object sender, EventArgs e)
        {
            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            if (row["FullNameCar"] == DBNull.Value) return;
            
            int id_User_vs_Car = (int)row["id_User_vs_Car"];
            string nameShort = (string)row["ShortNameCar"];
            string code = row["Code"].ToString();


            Task<DataTable> task = Config.hCntMain.setPassCarUnload(id_User_vs_Car);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show($"{dtResult.Rows[0]["msg"]}", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getData();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            if (row["FullNameCar"] != DBNull.Value)
            {
                if (DialogResult.OK == new frmAddCar() { nameKadr = (string)row["fio"], id_kadr = (int)row["id"], Text = "Редактировать а/м", nameFull = (string)row["FullNameCar"], nameShort = (string)row["ShortNameCar"] }.ShowDialog())
                    getData();
            }
            else
            {
                if (DialogResult.OK == new frmAddCar() { nameKadr = (string)row["fio"], id_kadr = (int)row["id"], Text = "Добавить а/м" }.ShowDialog())
                    getData();
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            if (row["FullNameCar"] == DBNull.Value) return;

            if (DialogResult.Yes == MessageBox.Show("Удалить запись?", "Запрос на действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {                
                int id_kadr = (int)row["id"];
                string nameFull = (string)row["FullNameCar"];
                string nameShort = (string)row["ShortNameCar"];


                Task<DataTable> task = Config.hCntMain.setUserVsCar(id_kadr, nameShort, nameFull, true, 1);
                task.Wait();
                getData();
            }
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible || col.Name.Equals(cDatePrintPass.Name) || col.Name.Equals(cPhone.Name))
                {
                    maxColumns++;
                    if (col.Name.Equals(cDeps.Name)) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals(cPost.Name)) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals(cFIO.Name)) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals(cPass.Name)) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals(cDatePrintPass.Name)) setWidthColumn(indexRow, maxColumns, 17, report);
                    if (col.Name.Equals(cPhone.Name)) setWidthColumn(indexRow, maxColumns, 17, report);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отчёт по пропускам", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отдел: {cmbDeps.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Должность: {cmbPost.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Место работы: {(rbOffice.Checked?rbOffice.Text:rbUni.Text)}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Статус сотрудника: {(rbWork.Checked ? rbWork.Text : rbUnemploy.Text)}", indexRow, 1);
            indexRow++;

            if (tbPostName.Text.Trim().Length != 0 || tbKadrName.Text.Trim().Length != 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтр: {(tbPostName.Text.Trim().Length != 0 ? $"Должность:{tbPostName.Text.Trim()} | ":"")} {(tbKadrName.Text.Trim().Length != 0 ? $"ФИО:{tbKadrName.Text.Trim()}" : "")}", indexRow, 1);
                indexRow++;
            }

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexCol = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible || col.Name.Equals(cDatePrintPass.Name) || col.Name.Equals(cPhone.Name))
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            foreach (DataRowView row in dtData.DefaultView)
            {
                indexCol = 1;
                report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible || col.Name.Equals(cDatePrintPass.Name) || col.Name.Equals(cPhone.Name))
                    {
                        if (row[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                        else
                           if (row[col.DataPropertyName] is decimal)
                        {
                            report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                            report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                        }
                        else
                            report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);

                        indexCol++;
                    }
                }

                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                indexRow++;
            }

            report.Show();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }
    }
}
