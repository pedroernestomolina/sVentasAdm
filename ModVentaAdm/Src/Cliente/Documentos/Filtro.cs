using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Documentos
{
    
    public class Filtro
    {

        private DateTime _desde;
        private DateTime _hasta;
        private string _autoCliente;


        public DateTime desde { get { return _desde; } }
        public DateTime hasta { get { return _hasta; } }
        public string autoCliente { get { return _autoCliente; } }
        

        public Filtro()
        {
            Limpiar();
        }


        public void Limpiar()
        {
            _desde = DateTime.Now.Date;
            _hasta = DateTime.Now.Date;
            _autoCliente = "";
        }

        public void setDesde(DateTime fecha)
        {
            _desde = fecha;
        }

        public void setHasta(DateTime fecha)
        {
            _hasta = fecha;
        }

        public void setCliente(string idCl)
        {
            _autoCliente = idCl;
        }

        public bool IsOk()
        {
            var rt = true;

            if (_desde > _hasta) 
            {
                Helpers.Msg.Error("FECHA INCORRECTAS, VERIFIQUE POR FAVOR");
                return false;
            }
            if (_autoCliente=="")
            {
                Helpers.Msg.Error("CLIENTE INCORRECTO, VERIFIQUE POR FAVOR");
                return false;
            }

            return rt;
        }

    }

}