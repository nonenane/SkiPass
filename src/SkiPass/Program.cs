﻿using System;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Nwuram.Framework.Settings.User;

namespace SkiPass
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    Task dtTask = get_settings();
                    dtTask.Wait();


                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Logging.StartFirstLevel(1);
                    Logging.Comment("Вход в программу");
                    Logging.StopFirstLevel();

                    if (new List<string>(new string[] { "ОЭЭС" }).Contains(UserSettings.User.StatusCode))
                        Application.Run(new frmUnLoadDataForTxt());
                    else
                        Application.Run(new frmListCar());

                    Logging.StartFirstLevel(2);
                    Logging.Comment("Выход из программы");
                    Logging.StopFirstLevel();

                    Project.clearBufferFiles();
                }
        }

        private static async Task get_settings()
        {
            object dtSettings = await Config.hCntMain.getSettings("lsnm");
            if (dtSettings == null)
                Config.hCntMain.setSettings("lsnm", "40");            
        }
    }
}
