using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Framework;
using Framework.WebControls;

namespace ChatBot
{
    public partial class Backup : Page
    {
        const string _seccion = "Backup";
        protected void Page_Load(object sender, EventArgs e)
        {
            grdBackup.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdDirectorio"), ColumnType.Data, "Directorio", "", true, true);
            grdBackup.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdNombre"), ColumnType.Data, "Nombre", "", true, true);
            grdBackup.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdFechaCreacion"), ColumnType.Datetime, "FechaCreacion", "", true, true);
            grdBackup.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdFechaModificacion"), ColumnType.Datetime, "FechaModificacion", "", true, true);

            grdBackup.AddContextMenu("cmnuRestore", MultiLanguage.GetTranslate("cmnuRestore"), "@Restore", "glyphicon glyphicon-import", "#5cb85c", "exampleModal");
            grdBackup.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Restore", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdBackup.DataSource = new Rules.Backup().ObtenerBackup();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            lblBackup.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblBackup");
            lblFileName.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblFileName");
        }

        [WebMethod]
        public static void BackupDatabase(string fileName)
        {
            var br = new Rules.Backup();
            br.BackupDatabase(fileName);
        }

        [WebMethod]
        public static void RestoreDatabase(string fileName)
        {
            var br = new Rules.Backup();
            br.RestoreDatabase(fileName);
        }

        [WebMethod]
        public static void DeleteDatabase(string fileName)
        {
            var br = new Rules.Backup();
            br.DeleteDatabase(fileName);
        }
    }
}