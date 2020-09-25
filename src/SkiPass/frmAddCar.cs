using Nwuram.Framework.Settings.Connection;
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
    public partial class frmAddCar : Form
    {        
        public int id_kadr { set; private get; }
        public string nameKadr { set; private get; }
        public string nameShort { set; private get; }
        public string nameFull { set; private get; }

        private bool isEditData = false;
        private string oldName, oldCode;
        
        public frmAddCar()
        {
            InitializeComponent();

            if(Config.hCntMain==null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");

            Task dtTask = get_settings();
            dtTask.Wait();
        }

        private async Task get_settings()
        {
            object dtSettings = await Config.hCntMain.getSettings("lsnm");
            if (dtSettings == null)
                tbShortName.MaxLength = 40;
            else
                tbShortName.MaxLength = int.Parse(dtSettings.ToString());
        }

        private void frmAddCar_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getKadrVsCar(id_kadr);
            task.Wait();
            if (task.Result != null && task.Result.Rows.Count != 0)
            {
                DataRow row = task.Result.Rows[0];
                nameKadr = (string)row["fio"];
                nameFull = row["FullNameCar"] == DBNull.Value ? "" : (string)row["FullNameCar"];
                nameShort = row["ShortNameCar"] == DBNull.Value ? "" : (string)row["ShortNameCar"];
                this.Text = row["FullNameCar"] == DBNull.Value ? this.Text = "Добавить а/м" : "Редактировать а/м";
            }
            else
            { this.Text = "Добавить а/м"; }

            tbFIO.Text = nameKadr;
            tbFullName.Text = nameFull;
            tbShortName.Text = nameShort;

            isEditData = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbFullName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lFullName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbFullName.Focus();
                return;
            }

            if (tbShortName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lShortName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbShortName.Focus();
                return;
            }


            Task<DataTable> task = Config.hCntMain.setUserVsCar(id_kadr, tbFullName.Text, tbShortName.Text, false, 0);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует производитель с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show($"{dtResult.Rows[0]["msg"]}", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void tbFullName_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void frmAddCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}
