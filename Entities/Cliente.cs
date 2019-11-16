using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Models.Attributes;

namespace Models
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
        [Insertable, Updatable]
        public string ChatbotName { get; set; }
        [Insertable, Updatable]
        public string HostName { get; set; }
        [Insertable, Updatable]
        public string HashKey { get; set; }

        [EntityMany("ClienteFrase", "Frase", "IdCliente", "IdFrase")]
        public List<Models.Frase> Frases { get; set; }

        [EntityMany("ClientePalabra", "Palabra", "IdCliente", "IdPalabra")]
        public List<Palabra> Palabras { get; set; }

        public Cliente()
        {
            this.Frases = new List<Frase>();
            this.Palabras = new List<Palabra>();
        }

    }
}