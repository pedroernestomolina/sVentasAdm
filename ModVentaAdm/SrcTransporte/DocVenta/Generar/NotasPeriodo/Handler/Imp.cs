using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.NotasPeriodo.Handler
{
    public class Imp: Vista.INotas
    {
        private string _notas;


        public string Titulo_Get { get { return "LAPSO / PERIODO"; } }
        public string Notas_Get { get { return _notas; } }


        public Imp()
        {
            _notas = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }


        public void Inicializa()
        {
            _notas = "";
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }
        Vista.Frm frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        public void setNotas(string desc)
        {
            _notas = desc;
        }


        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool _procesarIsOK;
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = true;
        }


        private bool cargarData()
        {
            return true;
        }
    }
}