using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Models.Attributes;

namespace Entities
{
    public class Palabra
    {
        [PrimaryKey]
        public int IdPalabra { get; set; }
        [Insertable, Updatable]
        public string Palabra1 { get; set; }
        [Insertable, Updatable]
        public string Palabra2 { get; set; }
        [Insertable, Updatable]
        public string Palabra3 { get; set; }
        [Insertable, Updatable]
        public string Respuesta { get; set; }
    }
}
