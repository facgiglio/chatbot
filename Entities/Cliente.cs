using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Models.Attributes;

namespace Entities
{
    public class Cliente
    {
        [PrimaryKey]
        public int IdCliente { get; set; }
        [Insertable, Updatable]
        public string RazonSocial { get; set; }
        [Insertable, Updatable]
        public string Direccion { get; set; }
        [Insertable, Updatable]
        public string CodigoPostal { get; set; }
        [Insertable, Updatable]
        public string Telefono { get; set; }
    }
}