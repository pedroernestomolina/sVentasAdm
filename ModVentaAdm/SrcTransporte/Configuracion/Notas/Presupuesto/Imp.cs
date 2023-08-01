using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModVentaAdm.SrcTransporte.Configuracion.Notas.Presupuesto
{
    public class Imp: INotas
    {
        private string _notas;


        public string Titulo_Get { get { return "NOTAS / OBSERVACIONES: PRESUPUESTO"; } }
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
        Frm frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new Frm();
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
            _procesarIsOK=false;
            if (_notas.Trim() != "")
            {
                if (Helpers.Msg.ProcesarGuardar()) 
                {
                    guardar();
                }
            }
        }

        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteCnf_NotasPresupuesto_Get();
                setNotas(r01.Entidad);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void guardar()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteCnf_NotasPresupuesto_Editar(_notas);
                _procesarIsOK = true;
                Helpers.Msg.EditarOk();
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}