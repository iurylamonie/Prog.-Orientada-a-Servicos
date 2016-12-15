using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsApp.Models
{
    class TUsuario
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1}", Nome, Descrição);
        }
    }
}
