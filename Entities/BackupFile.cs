using System;

namespace Models
{
    public class BackupFile
    {
        public string Nombre { get; set; }
        public string Directorio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
