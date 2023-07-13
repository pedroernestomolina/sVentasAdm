using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Presupuesto
{
    public class FichaFechaServ
    {
        public DateTime  fecha { get; set; }
        public string hora { get; set; }
        public string nota { get; set; }
        public FichaFechaServ()
        {
            fecha = DateTime.Now.Date;
            hora = "";
            nota = "";
        }
    }
}