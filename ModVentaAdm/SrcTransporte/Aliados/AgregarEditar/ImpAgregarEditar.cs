using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar
{
    public abstract class ImpAgregarEditar: IAgregarEditar
    {
        private Aliado _aliado;
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;       


        public Aliado Ficha { get { return _aliado; } }


        public ImpAgregarEditar()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _aliado = new Aliado();
        }


        public void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _aliado.Inicializa();
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