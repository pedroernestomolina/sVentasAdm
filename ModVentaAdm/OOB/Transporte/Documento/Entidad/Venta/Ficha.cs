using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Venta
{
    public class Ficha
    {
        public FichaEncabezado encabezado { get; set; }
        public List<FichaDetalle> detalles { get; set; }
        public List<Turno> turnos { get; set; }
        public List<DetTurno> detTurnos { get; set; }
        public List<Fecha> fechas { get; set; }
        public List<Aliado> aliados { get; set; }
        public List<DetDoc> detDoc { get; set; }
        public Ficha()
        {
            encabezado = new FichaEncabezado();
            detalles = new List<FichaDetalle>();
        }
    }
}