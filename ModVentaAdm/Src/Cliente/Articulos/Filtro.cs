using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Articulos
{
    
    public class Filtro
    {

        private DateTime _desde;
        private DateTime _hasta;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public DateTime desde { get { return _desde; } }
        public DateTime hasta { get { return _hasta; } }
        public OOB.Maestro.Cliente.Entidad.Ficha cliente { get { return _cliente; } }
        

        public Filtro()
        {
            Limpiar();
        }


        public void Limpiar()
        {
            _desde = DateTime.Now.Date;
            _hasta = DateTime.Now.Date;
            _cliente = null;
        }

        public void setDesde(DateTime fecha)
        {
            _desde = fecha;
        }

        public void setHasta(DateTime fecha)
        {
            _hasta = fecha;
        }

        public void setCliente(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _cliente = ficha;
        }

        public bool IsOk()
        {
            var rt = true;

            if (_desde > _hasta) 
            {
                Helpers.Msg.Error("FECHA INCORRECTAS, VERIFIQUE POR FAVOR");
                return false;
            }
            if (_cliente==null)
            {
                Helpers.Msg.Error("CLIENTE INCORRECTO, VERIFIQUE POR FAVOR");
                return false;
            }

            return rt;
        }

    }

}