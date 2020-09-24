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
        public frmListCar()
        {
            InitializeComponent();
            this.Text = "\"" + Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "\", \"" + Nwuram.Framework.Settings.User.UserSettings.User.Status + "\", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + "";
        }

        private void frmListCar_Load(object sender, EventArgs e)
        {
            getDeps();
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
        }

        private void rbUni_CheckedChanged(object sender, EventArgs e)
        {
            getDeps();
        }
    }
}
