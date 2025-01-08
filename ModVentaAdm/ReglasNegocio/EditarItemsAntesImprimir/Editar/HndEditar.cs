using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Editar
{
    public class HndEditar: IEditar
    {
        private string _texto;
        private Utils.Control.Boton.Salir.ISalir _btSalir;
        private Utils.Control.Boton.Procesar.IProcesar _btProcesar;
        private bool _edicionIsOk;
        private string _titulo;
        //
        public Utils.Control.Boton.Salir.ISalir BtSalir { get { return _btSalir; } }
        public Utils.Control.Boton.Procesar.IProcesar BtProcesar { get { return _btProcesar; } }
        public string GetTitulo { get { return _titulo; } }
        public string GetDetalle { get { return _texto; } }
        public bool EdicionIsOk { get { return _edicionIsOk; } }
        //
        public HndEditar() 
        {
            _titulo = "";
            _texto = "";
            _edicionIsOk = false;
            _btSalir = new Utils.Control.Boton.Salir.Imp();
            _btProcesar = new Utils.Control.Boton.Procesar.Imp();
        }
        public void Inicializa()
        {
            _texto = "";
            _edicionIsOk = false;
            _btSalir.Inicializa();
            _btProcesar.Inicializa();
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        //
        private bool CargarData()
        {
            return true;
        }
        public void setItemDescripcion(string text)
        {
            _texto = text;
        }
        public void Guardar()
        {
            _edicionIsOk = false;
            if (_texto.Trim() != "") 
            {
                BtProcesar.Opcion();
                if (BtProcesar.OpcionIsOK) 
                {
                    _edicionIsOk = true;
                }
            }
        }
        public void setTitulo(string text)
        {
            _titulo = text;
        }
    }
}