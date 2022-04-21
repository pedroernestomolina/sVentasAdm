using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Anular
{
    
    public class Gestion
    {


        private string _motivo;
        private bool _procesarIsOk;
        private bool _abandonarIsOk;


        public string Motivo { get { return _motivo; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool AbandonarIsOK { get { return _abandonarIsOk; } }


        public Gestion()
        {
            _motivo = "";
            _procesarIsOk = false;
            _abandonarIsOk = false;
        }


        AnularFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new AnularFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }

        public void Procesar()
        {
            if (Motivo.Trim() != "") 
            {
                _procesarIsOk = true;
            }
        }

        public void Inicializa()
        {
            _motivo = "";
            _procesarIsOk = false;
            _abandonarIsOk = false;
        }

        public void setMotivo(string p)
        {
            _motivo = p;
        }


        public void Abandonar()
        {
            _abandonarIsOk = true;
        }

    }

}