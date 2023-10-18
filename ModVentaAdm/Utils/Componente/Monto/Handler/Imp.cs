using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Monto.Handler
{
    public class Imp: Vistas.IMonto
    {
        private decimal _monto;


        public decimal Get_Monto { get { return _monto; } }


        public Imp()
        {
            _monto = 0m;
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }
        public void Inicializa()
        {
            _monto = 0m;
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }
        Vistas.Frm frm;
        public void Inicia()
        {
            if (frm == null) 
            {
                frm = new Vistas.Frm();
                frm.setControlador(this);
            }
            frm.ShowDialog();
        }


        public void setMonto(decimal monto)
        {
            _monto = monto;
        }

        private bool _procesarIsOK;
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = false;
            if (_monto >= 0m) 
            {
                _procesarIsOK = true;
            }
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }
    }
}