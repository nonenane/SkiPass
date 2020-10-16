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

                    Task<object> task1 = Config.hCntMain.getSettings("lsnm");
                    task1.Wait();
                    object dtSettings = task1.Result;

                    int maxLength = 41;
                    if (dtSettings != null)
                        maxLength = int.Parse(dtSettings.ToString());

                    Task<DataTable> task = Config.hCntMain.getListKadrVsCarForOEES();
                    task.Wait();

                    if (task.Result == null || task.Result.Rows.Count == 0) return false;

                    using (var MyFile = new StreamWriter(File.Open(Application.StartupPath + "\\Goods.txt",FileMode.Create), Encoding.Default))
                    {

                        foreach (DataRow row in task.Result.Rows)
                        {
                            string fio_Name = $"{((string)row["fio"]).PadLeft(6)} {((string)row["ShortNameCar"]).PadRight(maxLength)}";
                            //MyFile.WriteLine($"{((string)row["Code"]).PadLeft(13)},{"0.00".PadLeft(11)},{(string)row["fio"]} {(string)row["ShortNameCar"]}");
                            //MyFile.WriteLine($"{row["Code"].ToString().PadLeft(13)},{"0.00".PadLeft(11)},{(string)row["fio"]} {((string)row["ShortNameCar"]).PadRight(maxLength)}");
                            MyFile.WriteLine($"{row["Code"].ToString().PadLeft(13)},{"0.00".PadLeft(11)},{fio_Name}");
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
