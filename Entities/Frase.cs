using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Models.Attributes;

namespace Models
{
    public class Frase
    {
        [PrimaryKey]
        public int IdFrase { get; set; }
        [Insertable, Updatable]
        public string Descripcion { get; set; }
        [Insertable, Updatable]
        public string Respuesta { get; set; }
        [Insertable, Updatable]
        public bool Activo { get; set; }
    }
}
