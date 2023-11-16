using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.TasaDivisa
{
    public class Imp: ITasa
    {
        private decimal _tasaActual;
        private string _textoPublicar;


        public string Titulo_Get { get { return _textoPublicar; } }
        public decimal TasaActual_Get { get { return _tasaActual; } }


        public Imp()
        {
            _tasaActual = 0m;
            _textoPublicar = "";
            _abandonarIsOK = false;
            _procesarIsOK = false;
        }


        public void Inicializa()
        {
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


        public void setTasaDivisa(decimal tasa)
        {
            _tasaActual = tasa;
        }
        public void setTexto(string texto)
        {
            _textoPublicar = texto;
        }


        private bool _procesarIsOK;
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = false;
            if (_tasaActual > 0m) 
            {
                _procesarIsOK = true;
            }
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; ; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true; 
        }


        private bool CargarData()
        {
            return true;
        }
    }
}