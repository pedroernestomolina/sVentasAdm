using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar
{
    public abstract class ImpAgregarEditar: IAgregarEditar
    {
        private Servicio _servicio;
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;


        public Servicio Ficha { get { return _servicio; } }


        public ImpAgregarEditar()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _servicio = new Servicio();
        }


        public virtual void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _servicio.Inicializa();
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


        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        abstract public void Procesar();

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        virtual protected bool CargarData()
        {
            return true;
        }
    }
}