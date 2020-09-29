using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiPass
{
    public partial class frmUnLoadDataForTxt : Form
    {
        public frmUnLoadDataForTxt()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btUnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                btUnLoad.Enabled = false;
                var result = await Task<bool>.Factory.StartNew(() =>
                {

                    Task<DataTable> task = Config.hCntMain.getListKadrVsCarForOEES();
                    task.Wait();

                    if (task.Result == null || task.Result.Rows.Count == 0) return false;

                    using (var MyFile = new StreamWriter(Application.StartupPath + "\\SaveMyFile.txt"))
                    {

                        foreach (DataRow row in task.Result.Rows)
                        {
                            //MyFile.WriteLine($"{((string)row["Code"]).PadLeft(13)},{"0.00".PadLeft(11)},{(string)row["fio"]} {(string)row["ShortNameCar"]}");
                            MyFile.WriteLine($"{row["Code"].ToString().PadLeft(13)},{"0.00".PadLeft(11)},{(string)row["fio"]} {(string)row["ShortNameCar"]}");
                        }


                        //Random T = new Random();
                        //for (int i = 0; i < 1000; i++)
                        //{
                        //MyFile.WriteLine($"{i} {T.Next(-1000, 1000)}");
                        //}
                    }
                    return true;
                });
            }
            catch(Exception ex)
            {                
            }
            finally {
                btUnLoad.Enabled = true;
            }
        }
    }
}
