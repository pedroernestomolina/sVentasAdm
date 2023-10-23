using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltroFecha
{
    public class Imp: IFecha
    {
        private DateTime _fecha;
        private bool _isActiva;


        public DateTime Fecha { get { return _fecha; } }
        public bool IsActiva { get { return _isActiva; } }


        public Imp()
        {
            _fecha = DateTime.Now.Date;
            _isActiva = true;
        }
        public void Inicializa()
        {
            _fecha = DateTime.Now.Date;
            _isActiva = true;
        }
        public void setFecha(DateTime fecha)
        {
            _fecha = fecha;
        }
        public void setActivar(bool modo)
        {
            _isActiva = modo; 
        }

        public void Limpiar()
        {
            _fecha = DateTime.Now.Date;
            _isActiva = true;
        }
    }
}