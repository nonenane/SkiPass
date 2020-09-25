using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace SkiPass
{
    public class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        public async Task<DataTable> getDate()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[dbo].[GetDate]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        public async Task<DataTable> getDeps(bool isOffice, bool isAll)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[WorkTime].[GetDepsForSettings]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtResult == null) return null;

            if (isAll)
            {
                dtResult.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });

                DataRow newRow = dtResult.NewRow();
                newRow["id_dep"] = 0;
                newRow["dep_name"] = "Все отделы";
                newRow["isOffice"] = DBNull.Value;
                newRow["isMain"] = true;
                dtResult.Rows.Add(newRow);

                dtResult.DefaultView.RowFilter = $"isOffice = {isOffice} or isOffice is null";
                dtResult.DefaultView.Sort = "isMain desc, dep_name asc";
            }
            else {
                dtResult.DefaultView.RowFilter = $"isOffice = {isOffice}";
                dtResult.DefaultView.Sort = "dep_name asc";
            }
            
            dtResult = dtResult.DefaultView.ToTable().Copy();

            return dtResult;
        }

        public async Task<DataTable> getPosts(int id_dep, bool isAll)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(true);

            DataTable dtResult = executeProcedure("[WorkTime].[getPosts]",
                 new string[2] { "@id_dep", "@isWorkTime" },
                 new DbType[2] { DbType.Int32, DbType.Boolean }, ap);

            if (dtResult == null) return null;

            if (isAll)
            {
                dtResult.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });

                DataRow newRow = dtResult.NewRow();
                newRow["id"] = 0;
                newRow["cName"] = "Все Должности";
                newRow["isMain"] = true;
                dtResult.Rows.Add(newRow);

                dtResult.DefaultView.Sort = "isMain desc, cName asc";
            }
            else
                dtResult.DefaultView.Sort = "cName asc";

            dtResult = dtResult.DefaultView.ToTable().Copy();

            return dtResult;
        }

        public async Task<object>  getSettings(string id_value, int idProg = 0)
        {
            ap.Clear();

            if (idProg == 0)
                ap.Add(ConnectionSettings.GetIdProgram());
            else
                ap.Add(idProg);

            ap.Add(id_value);

            DataTable dt = new DataTable();

            dt = executeProcedure("[WorkTime].[GetSettings]",
                new string[2] { "@id_prog", "@id_value" },
                new DbType[2] { DbType.Int32, DbType.String }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return dt.Rows[0]["value"];
            }
            else
            {
                return null;
            }
        }

        public DataTable setSettings(string id_value, string value)
        {
            ap.Clear();


            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add(value);

            DataTable dtResult = executeProcedure("[WorkTime].[setSettings]",
                 new string[3] { "@id_prog", "@id_value", "@value" },
                 new DbType[3] { DbType.Int32, DbType.String, DbType.String }, ap);

            return dtResult;
        }

        /// <summary>
        /// Запись справочника форм собственности
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setUserVsCar(int id_kadr, string fullName, string shortName, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id_kadr);
            ap.Add(fullName);
            ap.Add(shortName);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[WorkTime].[setUserVsCar]",
                 new string[6] { "@id_kadr", "@fullName", "@shortName", "@id_user", "@result", "@isDel" },
                 new DbType[6] { DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }


        public async Task<DataTable> getListKadrVsCar(int id_workstatus, int id_personnelType)
        {
            ap.Clear();
            ap.Add(id_workstatus);
            ap.Add(id_personnelType);
           
            DataTable dtResult = executeProcedure("[WorkTime].[getListKadrVsCar]",
                 new string[2] { "@id_workstatus", "@id_personnelType" },
                 new DbType[2] { DbType.Int32, DbType.Int32}, ap);

            return dtResult;
        }


        public async Task<DataTable> getKadrVsCar(int id_kadr)
        {
            ap.Clear();
            ap.Add(id_kadr);

            DataTable dtResult = executeProcedure("[WorkTime].[getKadrVsCar]",
                 new string[1] { "@id_kadr" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }


        public async Task<DataTable> setPassCarUnload(int id_User_vs_Car)
        {
            ap.Clear();
            ap.Add(id_User_vs_Car);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dtResult = executeProcedure("[WorkTime].[setPassCarUnload]",
                 new string[2] { "@id_User_vs_Car", "@id_user" },
                 new DbType[2] { DbType.Int32, DbType.Int32 }, ap);

            return dtResult;
        }

    }
}
