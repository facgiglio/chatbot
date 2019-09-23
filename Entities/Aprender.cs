using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Models.Attributes;

namespace Entities
{
    public class Aprender
    {
        [Insertable, Updatable]
        public int IdAprender { get; set; }
        [Insertable, Updatable]
        public int IdCliente { get; set; }
        [Insertable, Updatable]
        public string Frase { get; set; }
        [Insertable, Updatable]
        public bool Aprendido { get; set; }
    }
}
