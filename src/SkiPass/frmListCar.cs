using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiPass
{
    public partial class frmListCar : Form
    {
        private DataTable dtData;
        private bool preChbPass = false;
        public frmListCar()
        {
            InitializeComponent();
            this.Text = "\"" + Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "\", \"" + Nwuram.Framework.Settings.User.UserSettings.User.Status + "\", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + "";


            tsLabel.Text = Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer() + " " +
              Nwuram.Framework.Settings.Connection.ConnectionSettings.GetDatabase();

            for (int i = 2; i <= 4; i++)
            {
                if (ConnectionSettings.GetServer($"{i}").Length > 0)
                {
                    tsLabel.Text += " | " + Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer($"{i}") + " " +
                  Nwuram.Framework.Settings.Connection.ConnectionSettings.GetDatabase($"{i}");
                }
            }


            dgvData.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
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

                if (!chbContainePass.Checked)
                    //filter += (filter.Length == 0 ? "" : " and ") + $"FullNameCar is null";
                    //else
                    filter += (filter.Length == 0 ? "" : " and ") + $"FullNameCar is not null";

                dtData.DefaultView.RowFilter = filter;
                dtData.DefaultView.Sort = "nameDep asc,fio asc";

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
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dtData.DefaultView.Count<=dgvData.CurrentRow.Index)
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

            EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<bool>("isSelect"));
            if (rowCollect.Count() > 0) 
            {
                Nwuram.Framework.ToExcelNew.ExcelUnLoad rep = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
                rep.SetPageOrientationToLandscape();

                int indexRow = 0;
                int indexCol = 1;
                int cnt = 0;
                foreach (DataRow row in rowCollect)
                {
                    if (row["FullNameCar"] == DBNull.Value) continue;

                    int id_User_vs_Car = (int)row["id_User_vs_Car"];
                    string nameShort = (string)row["ShortNameCar"];
                    string fio = $"{(string)row["lastname"]} {(string)row["firstname"]} {(string)row["secondname"]}";
                    string code = row["Code"].ToString();

                    if (code.Length == 0) continue;

                    if (cnt % 2 != 0)
                        indexCol += 6;
                    else
                    {
                        indexRow += indexRow == 0 ? 1 : 11;
                        indexCol = 1; 
                    }



                    printBlockPass(indexRow, indexCol, rep, nameShort, fio, code);
                    cnt++;
                }
                rep.Show();
                return;
            }
            else
            if (dgvData.SelectedRows.Count > 1)
            {

                Nwuram.Framework.ToExcelNew.ExcelUnLoad rep = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
                rep.SetPageOrientationToLandscape();

                int indexRow = 0;
                int indexCol = 1;
                int cnt = 0;
                foreach (DataGridViewRow rGrid in dgvData.SelectedRows)
                {
                    DataRowView row = dtData.DefaultView[rGrid.Index];

                    if (row["FullNameCar"] == DBNull.Value) continue;

                    int id_User_vs_Car = (int)row["id_User_vs_Car"];
                    string nameShort = (string)row["ShortNameCar"];
                    string fio = $"{(string)row["lastname"]} {(string)row["firstname"]} {(string)row["secondname"]}";
                    string code = row["Code"].ToString();

                    if (code.Length == 0) continue;

                    if (cnt % 2 != 0)
                        indexCol += 6;
                    else
                    {
                        indexRow += indexRow == 0 ? 1 : 11;
                        indexCol = 1;
                    }



                    printBlockPass(indexRow, indexCol, rep, nameShort, fio, code);
                    cnt++;
                }
                rep.Show();

                return;
            }
            else
            {

                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (row["FullNameCar"] == DBNull.Value) return;

                int id_User_vs_Car = (int)row["id_User_vs_Car"];
                string nameShort = (string)row["ShortNameCar"];
                string fio = $"{(string)row["lastname"]} {(string)row["firstname"]} {(string)row["secondname"]}";
                string code = row["Code"].ToString();



                Nwuram.Framework.ToExcelNew.ExcelUnLoad rep = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
                rep.SetPageOrientationToLandscape();

                int indexRow = 1;
                int indexCol = 1;

                printBlockPass(indexRow, indexCol, rep, nameShort, fio, code);             
                rep.Show();
                return;
            }


            //return;


            /*
            string windowsPath = Environment.GetEnvironmentVariable("windir");
            //задаем путь к файлу шрифта
            string path = windowsPath[0].ToString() + ":/Windows/Fonts/ean13.ttf";
            /*if (!File.Exists(path))
            {
                MessageBox.Show("Отсутствует установленный шрифт ean13.ttf", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            */

            /*
            path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Templates\\pass.xls";
            if (!File.Exists(path))
            {
                MessageBox.Show("Отсутствует шаблон выгрузки данных по пути\n" + path, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pathOut = Path.GetDirectoryName(Application.ExecutablePath) + "\\passPrint.xls";

            Nwuram.Framework.ToExcel.Report report = new Nwuram.Framework.ToExcel.Report();
            report.AddSingleValue("FIO", fio.Trim());
            report.AddSingleValue("ShortName", nameShort);
            report.AddSingleValue("code", ConvertToNewEan(code));
            report.CreateTemplate(path.Replace(".xls", ""), pathOut.Replace(".xls", ""), "");
            // report.OpenFile(pathOut);

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

            report.OpenFile(pathOut.Replace(".xls", ""));
            */

            getData();
        }

        private void printBlockPass(int indexRow, int indexCol, Nwuram.Framework.ToExcelNew.ExcelUnLoad rep,string nameShort, string fio, string code)
        {
            int rowStart = indexRow;
            //Размеры колонок
            //rep.SetBorders(indexRow, indexCol, indexRow+8, indexCol+4);
            rep.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, 15);
            rep.SetColumnWidth(indexRow, indexCol + 1, indexRow, indexCol, 8);

            rep.SetColumnWidth(indexRow, indexCol + 2, indexRow, indexCol + 2, 10);
            rep.SetColumnWidth(indexRow, indexCol + 3, indexRow, indexCol + 3, 5);

            rep.SetColumnWidth(indexRow, indexCol + 4, indexRow, indexCol + 4, 12);
            //Начало
            rep.Merge(indexRow, indexCol, indexRow, indexCol + 1);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 1, "Times New Roman");
            rep.AddSingleValue("Пропуск", indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 16);
            rep.SetCellAlignmentToLeft(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToJustify(indexRow, indexCol, indexRow, indexCol);
            rep.SetFontBold(indexRow, indexCol, indexRow, indexCol);

            //+2 колонки
            rep.Merge(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetFontName(indexRow, indexCol + 2, indexRow, indexCol + 3, "Times New Roman");
            rep.AddSingleValue("часы работы", indexRow, indexCol + 2);
            rep.SetFontSize(indexRow, indexCol + 2, indexRow, indexCol + 3, 12);
            rep.SetCellAlignmentToLeft(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetCellAlignmentToJustify(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetFontBold(indexRow, indexCol + 2, indexRow, indexCol + 3);


            //Колонка +4
            rep.SetFontName(indexRow, indexCol + 4, indexRow, indexCol + 4, "Code EAN13");
            rep.Merge(indexRow, indexCol + 4, indexRow + 8, indexCol + 4);
            rep.SetOrientation(indexRow, indexCol + 4, indexRow, indexCol + 4, 90);
            rep.SetFontSize(indexRow, indexCol + 4, indexRow, indexCol + 4, 41);
            rep.AddSingleValue(ConvertToNewEan(code), indexRow, indexCol + 4);
            rep.SetCellAlignmentToLeft(indexRow, indexCol + 4, indexRow, indexCol + 4);
            rep.SetCellAlignmentToJustify(indexRow, indexCol + 4, indexRow, indexCol + 4);


            //Вторая строка
            indexRow++;
            rep.Merge(indexRow, indexCol, indexRow, indexCol + 1);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 1, "Times New Roman");
            rep.AddSingleValue("на служебную парковку", indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 12);
            rep.SetCellAlignmentToLeft(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToJustify(indexRow, indexCol, indexRow, indexCol);
            //rep.SetFontBold(indexRow, indexCol, indexRow, indexCol);

            //+2 колонки
            rep.Merge(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetFontName(indexRow, indexCol + 2, indexRow, indexCol + 3, "Times New Roman");
            rep.AddSingleValue("с 7:30 до 2:30", indexRow, indexCol + 2);
            rep.SetFontSize(indexRow, indexCol + 2, indexRow, indexCol + 3, 12);
            rep.SetCellAlignmentToLeft(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetCellAlignmentToJustify(indexRow, indexCol + 2, indexRow, indexCol + 3);
            rep.SetFontBold(indexRow, indexCol + 2, indexRow, indexCol + 3);

            //3 и 4 строка
            indexRow++;
            indexRow++;
            rep.Merge(indexRow, indexCol, indexRow, indexCol + 3);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 1, "Times New Roman");
            rep.AddSingleValue(fio, indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 12);
            rep.SetCellAlignmentToCenter(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToJustify(indexRow, indexCol, indexRow, indexCol);
            rep.SetFontBold(indexRow, indexCol, indexRow, indexCol);

            //5 строка
            indexRow++;
            rep.Merge(indexRow, indexCol, indexRow, indexCol + 3);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 1, "Times New Roman");
            rep.AddSingleValue("(Фамилия И.О.)", indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 9);
            rep.SetCellAlignmentToCenter(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToTop(indexRow, indexCol, indexRow, indexCol);
            //rep.SetFontBold(indexRow, indexCol, indexRow, indexCol);

            //6 и 7 и 8 строка
            indexRow++;
            indexRow++;
            //rep.Merge(indexRow, indexCol, indexRow, indexCol);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 1, "Times New Roman");
            rep.AddSingleValue("Гос. Номер а/м:", indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 11);
            rep.SetCellAlignmentToLeft(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToJustify(indexRow, indexCol, indexRow, indexCol);
            //rep.SetFontBold(indexRow, indexCol, indexRow, indexCol);

            rep.Merge(indexRow, indexCol + 1, indexRow + 1, indexCol + 3);
            rep.SetWrapText(indexRow, indexCol + 1, indexRow + 1, indexCol + 3);
            rep.SetFontName(indexRow, indexCol + 1, indexRow, indexCol + 3, "Times New Roman");
            rep.AddSingleValue(nameShort, indexRow, indexCol + 1);
            rep.SetFontSize(indexRow, indexCol + 1, indexRow, indexCol + 3, 12);
            rep.SetCellAlignmentToCenter(indexRow, indexCol + 1, indexRow, indexCol + 3);
            rep.SetCellAlignmentToJustify(indexRow, indexCol + 1, indexRow, indexCol + 3);
            rep.SetFontBold(indexRow, indexCol + 1, indexRow, indexCol + 3);
            

            //9 строка
            indexRow++;
            indexRow++;
            rep.Merge(indexRow, indexCol, indexRow, indexCol + 3);
            rep.SetFontName(indexRow, indexCol, indexRow, indexCol + 3, "Times New Roman");
            rep.AddSingleValue("Подпись РДО ___________", indexRow, indexCol);
            rep.SetFontSize(indexRow, indexCol, indexRow, indexCol, 11);
            rep.SetCellAlignmentToLeft(indexRow, indexCol, indexRow, indexCol);
            rep.SetCellAlignmentToJustify(indexRow, indexCol, indexRow, indexCol);

            rep.SetBordersToAll(rowStart, indexCol, rowStart, indexCol + 4, OfficeOpenXml.Style.ExcelBorderStyle.Medium, 1);
            rep.SetBordersToAll(indexRow, indexCol, indexRow, indexCol+4, OfficeOpenXml.Style.ExcelBorderStyle.Medium,2);


            rep.SetBordersToAll(rowStart, indexCol, indexRow, indexCol, OfficeOpenXml.Style.ExcelBorderStyle.Medium, 3);
            
            rep.SetBordersToAll(rowStart, indexCol+4, indexRow, indexCol+4, OfficeOpenXml.Style.ExcelBorderStyle.Medium, 4);

            rep.SetBordersToAll(rowStart, indexCol+3, indexRow, indexCol+3, OfficeOpenXml.Style.ExcelBorderStyle.Medium, 4);

        }


        private string ConvertToNewEan(string str)
        {
            string result = "";
            char[] chaine = str.ToCharArray();

            int first = 0;
            bool fromA; // true начинает отсчет с буквы A, false начинает отсчет с буквы K 

            for (int i = 1; 8 > i; i++)
            {
                fromA = false;

                if (i == 1)
                {
                    //первая цифра добавляется как есть
                    result += chaine[i - 1].ToString();
                    first = int.Parse(chaine[i - 1].ToString());
                }
                else
                {
                    //со 2-й по 7-ю цифры преобразуются в заглавные буквы
                    switch (i)
                    {
                        case 2:
                            {
                                //2-я цифра преобразуется всегда начиная с буквы А
                                //остальные в зависимости от того, какая была 1-я цифра
                                fromA = true;
                                break;
                            }
                        case 3:
                            {
                                if ((first == 0)
                                    || (first == 1)
                                    || (first == 2)
                                    || (first == 3))
                                {
                                    fromA = true;
                                }
                                break;
                            }
                        case 4:
                            {
                                if ((first == 0)
                                    || (first == 4)
                                    || (first == 7)
                                    || (first == 8))
                                {
                                    fromA = true;
                                }
                                break;
                            }
                        case 5:
                            {
                                if ((first == 0)
                                    || (first == 1)
                                    || (first == 4)
                                    || (first == 5)
                                    || (first == 9))
                                {
                                    fromA = true;
                                }
                                break;
                            }
                        case 6:
                            {
                                if ((first == 0)
                                    || (first == 2)
                                    || (first == 5)
                                    || (first == 6)
                                    || (first == 7))
                                {
                                    fromA = true;
                                }
                                break;
                            }
                        case 7:
                            {
                                if ((first == 0)
                                    || (first == 3)
                                    || (first == 6)
                                    || (first == 8)
                                    || (first == 9))
                                {
                                    fromA = true;
                                }
                                break;
                            }
                    }

                    result += ConvertNumberToLetter(chaine[i - 1].ToString(), fromA);
                }
            }

            //срединный разделитель
            result += "*";

            //со 8-й по 13-ю цифры преобразуются в прописные буквы
            for (int i = 8; 14 > i; i++)
            {
                result += ConvertNumberToSmallLetter(chaine[i - 1].ToString());
            }

            //результирующий разделитель
            result += "+";

            return result;
        }

        private string ConvertNumberToSmallLetter(string number)
        {
            byte[] a = new byte[1];

            //получаем ASCII код буквы
            a[0] = Convert.ToByte((int.Parse(number) + 97).ToString());

            //преобразуем ASCII код в букву
            return Encoding.Default.GetString(a);
        }

        private string ConvertNumberToLetter(string number, bool fromA)
        {
            byte[] a = new byte[1];

            //получаем ASCII код буквы
            if (fromA)
            {
                a[0] = Convert.ToByte((int.Parse(number) + 65).ToString());
            }
            else
            {
                a[0] = Convert.ToByte((int.Parse(number) + 75).ToString());
            }

            //преобразуем ASCII код в букву
            return Encoding.Default.GetString(a);
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

        private void rbOffice_Click(object sender, EventArgs e)
        {
            if (rbOffice.Checked && new List<string>(new string[] { "СК2" }).Contains(UserSettings.User.StatusCode))
            {
                preChbPass = chbContainePass.Checked;
                chbContainePass.Visible = false;
                chbContainePass.Checked = false;
                btAdd.Visible = false;
            }
        }

        private void rbUni_Click(object sender, EventArgs e)
        {
            if (rbUni.Checked && new List<string>(new string[] { "СК2" }).Contains(UserSettings.User.StatusCode))
            {
                chbContainePass.Checked = preChbPass;
                chbContainePass.Visible = true;
                btAdd.Visible = true;
            }
        }

        private void frmListCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы хотите закрыть программу?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                e.Cancel = true;
            }
        }
    }
}
