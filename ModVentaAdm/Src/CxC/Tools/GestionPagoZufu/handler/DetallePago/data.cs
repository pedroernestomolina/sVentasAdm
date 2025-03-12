using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.DetallePago
{
    public class data
    {
        private string _notas;
        private DateTime _fechaProceso;
        private DateTime _fechaServidor;
        private LibUtilitis.Opcion.IData _cobrador;
        //
        public string GetNotas { get { return _notas; } }
        public LibUtilitis.Opcion.IData GetCobrador { get { return _cobrador; } }
        public DateTime GetFechaProceso { get { return _fechaProceso; } }
        //
        public data()
        {
            _notas = "";
            _fechaServidor = DateTime.Now.Date;
            _cobrador = null;
        }
        public void Inicializa()
        {
            _notas = "";
            _fechaProceso = DateTime.Now.Date;
        }
        public bool IsOk()
        {
            if (_cobrador == null)
            {
                Helpers.Msg.Error("CAMPO [ COBRADOR ] NO PUEDE ESTAR VACIO");
                return false;
            }
            return true;
        }
        public void setNotas(string p)
        {
            _notas = p;
        }
        public void setFechaProceso(DateTime fechaProceso)
        {
            _fechaProceso = fechaProceso;
        }
        public void setCobrador(LibUtilitis.Opcion.IData _item)
        {
            _cobrador = _item;
        }
    }
}