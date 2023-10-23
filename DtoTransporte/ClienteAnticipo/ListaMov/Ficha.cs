using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.ClienteAnticipo.ListaMov
{
    public class Ficha
    {
        public int idMov { get; set; }
        public string ciRifCliente { get; set; }
        public string nombreCliente { get; set; }
        public decimal montoMonDiv { get; set; }
        public string aplicaRet { get; set; }
        public decimal montoRecMonDiv { get; set; }
        public DateTime fechaReg { get; set; }
        public string estatusAnulado { get; set; }
    }
}