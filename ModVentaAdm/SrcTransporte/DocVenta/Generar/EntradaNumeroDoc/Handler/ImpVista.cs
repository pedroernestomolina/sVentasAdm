using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.EntradaNumeroDoc.Handler
{
    public class ImpVista: Vista.IVista
    {
        private Utils.Control.Boton.Abandonar.IAbandonar _btAbandonar;
        private Utils.Control.Boton.Procesar.IProcesar _btAceptar;
        private string _numerorDoc;
        //
        public Utils.Control.Boton.Abandonar.IAbandonar BtAbandonar { get { return _btAbandonar; } }
        public Utils.Control.Boton.Procesar.IProcesar BtAceptar { get { return _btAceptar; } }
        public string Get_NumDocGenerar { get { return _numerorDoc; } }
        //
        public ImpVista()
        {
            _numerorDoc = "";
            _btAbandonar = new Utils.Control.Boton.Abandonar.Imp();
            _btAceptar = new Utils.Control.Boton.Procesar.Imp();
        }
        public void Inicializa()
        {
            _numerorDoc = "";
            _btAbandonar.Inicializa();
            _btAceptar.Inicializa();
        }
        Vista.Frm frm;
        public void Inicia()
        {
            if (cargarDataIsOk()) 
            {
                if (frm == null) 
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setNumeroDoc(string doc)
        {
            _numerorDoc = "";
            if (doc.Trim() != "")
            {
                _numerorDoc = doc.Trim().PadLeft(10, '0');
            }
        }
        //
        private bool cargarDataIsOk()
        {
            return true;
        }
    }
}