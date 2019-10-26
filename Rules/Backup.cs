using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Framework.Helpers;

namespace Rules
{
    public class Backup
    {
        string directory = WebConfigurationManager.AppSettings["BackupDirectory"];

        public List<Object> ObtenerBackup()
        {
            var dir = new DirectoryInfo(directory);
            var backpus = new List<Object>();

            foreach (var file in dir.GetFiles("*.bak"))
            {
                backpus.Add(new Models.BackupFile
                {
                    Directorio = file.DirectoryName,
                    Nombre = file.Name,
                    FechaCreacion = file.CreationTime,
                    FechaModificacion = file.LastWriteTime
                });
            }

            return backpus;
        }

        public void BackupDatabase(string fileName)
        {
            //Agrego la extensión del archivo
            fileName += ".bak";

            var store = "BackupDatabase";
            var parameters = new List<SqlParameter>() {
                new SqlParameter("@fileName", fileName),
                new SqlParameter("@directory", directory)
            };

            //Ejecuto el backup.
            SqlHelper.ExecuteQueryMaster(store, parameters.ToArray());
        }

        public void RestoreDatabase(string fileName)
        {
            var store = "RestoreDatabase";
            var parameters = new List<SqlParameter>() {
                new SqlParameter("@fileName", fileName),
                new SqlParameter("@directory", directory)
            };

            //Ejecuto el backup.
            SqlHelper.ExecuteQueryMaster(store, parameters.ToArray());

        }

        public void DeleteDatabase(string fileName)
        {
            var dir = new DirectoryInfo(directory);

            File.Delete(directory + @"\" + fileName);
        }
    }
}
